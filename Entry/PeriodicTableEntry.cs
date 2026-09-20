using PeriodicTableConsoleApp.Data;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace PeriodicTableConsoleApp.Entry
{
    public class PeriodicTableEntry
    {
        static private string PeriodicTableDir = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Assets\PeriodicTable.json";
        static async Task Main(string[] args)
        {
            EndFHandler handler = new EndFHandler();
            RootSourceToMemory json = new RootSourceToMemory();
            json.allDebug = true;
            json.NuDatBool = true;
            json.PubChemBool = true;
            json.NeedsUpdate = false;
            if (File.Exists(PeriodicTableDir))
            {
                if (json.NeedsUpdate)
                {
                    Console.WriteLine("File Does Exist, Updating.");
                    await json.PutTogether();
                }
                else if (!json.NeedsUpdate)
                {
                    Console.WriteLine("File Does Exist.");
                    await handler.DownloadZips();
                    handler.ExtractZips();
                    //json.DumpAllData();// For debug purposes. only runs if file doesn't need update, and does exist.
                }
            }
            else if(!File.Exists(PeriodicTableDir))
            {
                Console.WriteLine("File Doesn't Exist.");
                await json.PutTogether();
            }
        }
    }
}