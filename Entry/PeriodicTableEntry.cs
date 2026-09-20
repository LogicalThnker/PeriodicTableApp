using PeriodicTableConsoleApp.ClassLib;
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
            PubChemJSON json = new PubChemJSON();
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