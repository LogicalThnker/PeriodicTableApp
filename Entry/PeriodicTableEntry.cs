using PeriodicTableConsoleApp.ClassLib;

namespace PeriodicTableConsoleApp.Entry
{
    public class PeriodicTableEntry
    {
        static void Main(string[] args)
        {
            Element hydrogen = PeriodicTable.Get("H");
            Console.WriteLine(hydrogen.Name);
        }
    }
}