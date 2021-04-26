using PaperRulez.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace PaperRulez.Services
{
    public struct StoreResult
    {
        public string Client { get; set; }
        public string DocumentId { get; set; }
        public IEnumerable<string> Keywords { get; set; }

        public StoreResult(string client, string documentId, IEnumerable<string> keywords)
        {
            this.Client = client;
            this.DocumentId = documentId;
            this.Keywords = keywords;
        }
    }

    public class LookupStore : ILookupStore
    {
        private readonly string _resultPath = @"E:\source\repos\Mapper\PaperRulez\Results\result.json";

        public void Record(string client, string documentId, IEnumerable<string> keywords)
        {
            string json = JsonSerializer.Serialize(new StoreResult(client, documentId, keywords));

            if (!File.Exists(_resultPath))
                File.WriteAllText(_resultPath, json);
            else
                File.AppendAllText(_resultPath, $",{Environment.NewLine}{json}");
        }
    }
}