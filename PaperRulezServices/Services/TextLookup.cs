using PaperRulez.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PaperRulez.Services
{
    public class TextLookup : ITextLookup
    {
        private readonly IFileHelper _fileReader;
        private readonly ILookupStore _lookupStore;
        private readonly string _client;
        private readonly string _documentId;

        // Replace this regex by an external config file.
        private readonly string _lookupRegex = @"(^)(?<keyword>{{parameter}})([^\w])|([^\w])(?<keyword>{{parameter}})($)|([^\w])(?<keyword>{{parameter}})([^\w])";

        public TextLookup(IFileHelper fileReader, ILookupStore lookupStore, string client, string documentId)
        {
            _fileReader = fileReader;
            _lookupStore = lookupStore;
            _client = client;
            _documentId = documentId;
        }

        public void Lookup(string[] parameters)
        {
            var keywordsFound = new List<string>();
            var previousBlock = "";

            foreach (var block in _fileReader.ReadBlock())
            {
                previousBlock += block;

                foreach (var result in DoSearch(previousBlock, parameters))
                {
                    keywordsFound.Add(result);
                }

                parameters = parameters.Except(keywordsFound, StringComparer.OrdinalIgnoreCase).ToArray();

                if (!parameters.Any())
                {
                    break;
                }

                // Keep last 20 characters to overlap on next search so its not breaking a word
                previousBlock = previousBlock[^20..];
            }

            _lookupStore.Record(_client, _documentId, keywordsFound);
        }

        private IEnumerable<string> DoSearch(string content, params string[] parameters)
        {
            foreach (var parameter in parameters)
            {
                string pattern = _lookupRegex.Replace("{{parameter}}", parameter);

                Match match = Regex.Match(content, pattern, RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

                if (match.Success)
                {
                    // Return named group
                    yield return match.Groups["keyword"].Value;
                }
            }
        }
    }
}