using PaperRulez;
using PaperRulez.Services;
using PaperRulezTests.Mocks;
using System.IO;
using Xunit;

namespace PaperRulezTests
{
    public class PaperRulezTests
    {
        private readonly string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;

        [Fact]
        public void CanGetProcessingTypeFromFile()
        {
            var paperRulez = new PaperRulez.PaperRulez("client", new FileReader(Path.Combine(projectDirectory, @"Files\DOCE4878_largeTestFile.text")), new MockLookupStore());

            var expectedProcessType = ProcessingType.LOOKUP;
            var expectedParams = new string[3] { "Cat", "owner", "box" };

            var actualProcess = paperRulez.GetProcessingType();

            Assert.Equal(expectedProcessType, actualProcess.Type);
            Assert.Equal(expectedParams, actualProcess.Parameters);
        }
    }
}
