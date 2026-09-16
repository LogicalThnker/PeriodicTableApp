using PeriodicTableConsoleApp.ClassLib;
using PeriodicTableConsoleApp.Data;

namespace PeriodicTableConsoleApp.Entry
{
    public class PeriodicTableEntry
    {
        static private string dir = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Data\PeriodicTable.json";
        static void Main(string[] args)
        {
            if (File.Exists(dir))
            {
                Console.WriteLine("File exists");
            }
            else
            {
                PubChemJSON json = new PubChemJSON();
                json.WriteFromMemoryToFile();
                Console.WriteLine("File does NOT exist");
            }
            
        }
    }
}