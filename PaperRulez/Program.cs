using PaperRulez.Services;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PaperRulez
{
    public class Program
    {
        static void Main(string[] args)
        {
            LoopClientFoldersAsync();
        }

        static void LoopClientFoldersAsync()
        {
#if DEBUG
            var clientFolders = Directory.EnumerateDirectories(@"E:\source\repos\Mapper\PaperRulez\Clients");
#else  
            var clientFolders = Directory.EnumerateDirectories(Path.Combine(Directory.GetCurrentDirectory(), "Clients"));
#endif
            var lookupStore = new LookupStore();

            while (true)
            {
                foreach (var clientFolder in clientFolders)
                {
                    var fileList = Directory.EnumerateFiles(clientFolder).ToList();
                    var client = Path.GetFileName(clientFolder);

                    // Change for any log library, like serilog, NLog, log4net...
                    Console.WriteLine($"Processing client {client}");

                    foreach (var filePath in fileList)
                    {
                        Console.WriteLine($"Processing file in {filePath}..");

                        var fileReader = new FileReader(filePath);
                        var paperRulez = new PaperRulez(client, fileReader, lookupStore);

                        paperRulez.Start();

                        Console.WriteLine($"Processing finished");
                    }
                }

                Task.Delay(5000).Wait();
            }
        }
    }
}
