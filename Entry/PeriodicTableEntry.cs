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
                PubChemJSON json = new PubChemJSON();
                json.WriteFromMemoryToFile();
                json.PopulateIsotopeBuckets();
                json.AddIsotopes();
                json.WriteIsotopesAddedList();
            }
            else
            {
                Console.WriteLine("File does NOT exist");
                Console.WriteLine(" - Creating file now...");
                PubChemJSON json = new PubChemJSON();
                json.WriteFromMemoryToFile();
                json.PopulateIsotopeBuckets();
                json.AddIsotopes();
                json.WriteIsotopesAddedList();
            }

        }
    }
}