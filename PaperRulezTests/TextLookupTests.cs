using PaperRulez.Services;
using PaperRulezTests.Mocks;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace PaperRulezTests
{
    public class TextLookupTests
    {
        private readonly string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

        [Fact]
        public void LookupSmallFile()
        {
            var mockupStore = new MockLookupStore();
            var expectedClient = "Test1";
            var expectedDocumentId = "DOCE4878";
            var expectedKeywords = new List<string> { "Cat", "owner", "box" };
            var fileReader = new FileReader(Path.Combine(projectDirectory, @"Files\DOCE4878_smallTestFile.text"));
            var textLookup = new TextLookup(fileReader, mockupStore, expectedClient, expectedDocumentId);


            textLookup.Lookup(new string[3] { "Cat", "owner", "box" });

            Assert.Equal(expectedClient, mockupStore.Results[0].client);
            Assert.Equal(expectedDocumentId, mockupStore.Results[0].documentId);
            Assert.Equal(expectedKeywords, mockupStore.Results[0].keywords);
        }

        [Fact]
        public void LookupLargeFile()
        {
            var mockupStore = new MockLookupStore();
            var expectedClient = "Test1";
            var expectedDocumentId = "DOCE4878";
            var expectedKeywords = new List<string> { "Cat", "owner", "box" };
            var fileReader = new FileReader(Path.Combine(projectDirectory, @"Files\DOCE4878_largeTestFile.text"));
            var textLookup = new TextLookup(fileReader, mockupStore, expectedClient, expectedDocumentId);

            textLookup.Lookup(new string[3] { "Cat", "owner", "box" });

            Assert.Equal(expectedClient, mockupStore.Results[0].client);
            Assert.Equal(expectedDocumentId, mockupStore.Results[0].documentId);
            Assert.Equal(expectedKeywords, mockupStore.Results[0].keywords);
        }

        [Fact]
        public void LookupEmptyFile()
        {
            var mockupStore = new MockLookupStore();
            var expectedClient = "Test1";
            var expectedDocumentId = "TXT123";
            var expectedKeywords = new List<string> { };
            var fileReader = new FileReader(Path.Combine(projectDirectory, @"Files\TXT123_empty.text"));
            var textLookup = new TextLookup(fileReader, mockupStore, expectedClient, expectedDocumentId);

            textLookup.Lookup(new string[0] { });

            Assert.Equal(expectedClient, mockupStore.Results[0].client);
            Assert.Equal(expectedDocumentId, mockupStore.Results[0].documentId);
            Assert.Equal(expectedKeywords, mockupStore.Results[0].keywords);
        }

        [Fact]
        public void LookupUnexistintWords()
        {
            var mockupStore = new MockLookupStore();
            var expectedClient = "Test1";
            var expectedDocumentId = "DOCE4878";
            var expectedKeywords = new List<string> { };

            var fileReader = new FileReader(Path.Combine(projectDirectory, @"Files\DOCE4878_largeTestFile.text"));
            var textLookup = new TextLookup(fileReader, mockupStore, expectedClient, expectedDocumentId);

            textLookup.Lookup(new string[3] { "Dog", "golf", "drawer" });

            Assert.Equal(expectedClient, mockupStore.Results[0].client);
            Assert.Equal(expectedDocumentId, mockupStore.Results[0].documentId);
            Assert.Equal(expectedKeywords, mockupStore.Results[0].keywords);
        }

        [Fact]
        public void LookupMixOfUnexistintAndExistingWords()
        {
            var mockupStore = new MockLookupStore();
            var expectedClient = "Test1";
            var expectedDocumentId = "DOCE4878";
            var expectedKeywords = new List<string> { "owner", "Cat" };

            var fileReader = new FileReader(Path.Combine(projectDirectory, @"Files\DOCE4878_largeTestFile.text"));
            var textLookup = new TextLookup(fileReader, mockupStore, expectedClient, expectedDocumentId);

            textLookup.Lookup(new string[5] { "DoG", "oWnEr", "golf", "drAWer", "cAt" });

            Assert.Equal(expectedClient, mockupStore.Results[0].client);
            Assert.Equal(expectedDocumentId, mockupStore.Results[0].documentId);
            Assert.Equal(expectedKeywords, mockupStore.Results[0].keywords);
        }

        [Fact]
        public void LookupRegexTest()
        {
            var mockupStore = new MockLookupStore();

            var expectedKeywords = new List<string> { "dog", "hamburguer" };

            var fileReader = new FileReader(Path.Combine(projectDirectory, @"Files\REGEXTester01_TestRegex.text"));
            var textLookup = new TextLookup(fileReader, mockupStore, "", "");

            textLookup.Lookup(new string[3] { "dog", "alcohol", "hamburguer" });

            Assert.Equal(expectedKeywords, mockupStore.Results[0].keywords);
        }
    }
}