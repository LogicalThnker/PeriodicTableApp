using PeriodicTableConsoleApp.ClassLib;
using PeriodicTableConsoleApp.Data;

namespace PeriodicTableConsoleApp.Entry
{
    public class PeriodicTableEntry
    {
        static void Main(string[] args)
        {
            JSON json = new JSON();
            json.ReadJsonData();
            json.OutputAllData();
        }
    }
}