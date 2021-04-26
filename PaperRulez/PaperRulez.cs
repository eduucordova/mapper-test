using PaperRulez.Interfaces;
using PaperRulez.Services;
using System.IO;

namespace PaperRulez
{
    public class PaperRulez
    {
        private readonly IFileHelper _fileReader;
        private readonly ILookupStore _lookupStore;
        private readonly string _client;

        public PaperRulez(string client, IFileHelper fileReader, ILookupStore lookupStore)
        {
            _client = client;
            _fileReader = fileReader;
            _lookupStore = lookupStore;
        }

        public void Start()
        {
            // also need to get the file extension
            var process = GetProcessingType();

            var fileName = _fileReader.FileName();
            var documentId = fileName.Substring(0, fileName.IndexOf('_'));
            var fileType = Path.GetExtension(fileName);

            switch (process.Type)
            {
                case ProcessingType.LOOKUP:
                    {
                        if (fileType == ".text")
                        {
                            var textLookup = new TextLookup(_fileReader, _lookupStore, _client, documentId);
                            textLookup.Lookup(process.Parameters);
                        }
                        break;
                    }
                default:
                    break;
            }

            _fileReader.Remove();
        }

        public Process GetProcessingType()
        {
            var firstLine = _fileReader.ReadFirstLine();

            var pipeIndex = firstLine.IndexOf('|');

            var type = firstLine.Substring(0, pipeIndex).ToUpper();
            var parameters = firstLine[++pipeIndex..].Split(',');

            return type switch
            {
                "LOOKUP" => new Process(ProcessingType.LOOKUP, parameters),
                _ => new Process(ProcessingType.OTHER, parameters),
            };
        }
    }
}
