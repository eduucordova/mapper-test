using PaperRulez.Interfaces;
using System.Collections.Generic;

namespace PaperRulezTests.Mocks
{
    /// <summary>
    /// Mock of the lookup store, so we can test the 'Lookup' function in 'TextLookup'
    /// </summary>
    class MockLookupStore : ILookupStore
    {
        public struct StoreResult
        {
            public string client;
            public string documentId;
            public IEnumerable<string> keywords;

            public StoreResult(string client, string documentId, IEnumerable<string> keywords)
            {
                this.client = client;
                this.documentId = documentId;
                this.keywords = keywords;
            }
        }

        public List<StoreResult> Results { get; set; }

        public MockLookupStore()
        {
            Results = new List<StoreResult>();
        }

        public void Record(string client, string documentId, IEnumerable<string> keywords)
        {
            Results.Add(new StoreResult(client, documentId, keywords));
        }
    }
}
