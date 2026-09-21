using PeriodicTableConsoleApp.Data;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace PeriodicTableConsoleApp.Entry
{
    public class PeriodicTableEntry
    {
        static private readonly string PeriodicTableDir = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Assets\PeriodicTable.json";
        static private readonly string DeuteronEndFPath = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Data\EndF\Deuteron\";
        static private readonly string NeutronEndFPath = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Data\EndF\Neutron\";
        static private readonly string ProtonEndFPath = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Data\EndF\Proton\";
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
                    //await handler.DownloadZips();
                    //handler.ExtractZips();
                    if (handler.EndfFFileExists(DeuteronEndFPath) && handler.EndfFFileExists(NeutronEndFPath) && handler.EndfFFileExists(ProtonEndFPath))
                    {
                        handler.ConvertInputToOutputFormat(DeuteronEndFPath + @"deuteron\endf\");
                        handler.ConvertInputToOutputFormat(NeutronEndFPath + @"neutron\endf\");
                        handler.ConvertInputToOutputFormat(ProtonEndFPath + @"proton\endf\");
                        Console.WriteLine("Number Of Files : " + handler.NumberOfFiles);
                        Console.WriteLine("Number Of Lines : " + handler.NumberOfLines);
                        Console.WriteLine("Number Of Chars : " + handler.NumberOfChars);
                    }
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