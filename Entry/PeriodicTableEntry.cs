using PeriodicTableConsoleApp.ClassLib;

namespace PeriodicTableConsoleApp.Entry
{
    public class PeriodicTableEntry
    {
        static void Main(string[] args)
        {
            ExampleOutput();
        }
        public static void ExampleOutput()
        {
            Element hydrogen = PeriodicTable.Get("H");
            Console.WriteLine("Name: " + hydrogen.Name);
            Console.WriteLine("Symbol: " + hydrogen.Symbol);
            Console.WriteLine("AtomicNumber: " + hydrogen.AtomicNumber);
            Console.WriteLine("StandardAtomicWeight: " + hydrogen.StandardAtomicWeight);
            Console.WriteLine("MeltingPoint: " + hydrogen.MeltingPoint);

            // Unused in the case of H
            Console.WriteLine("RepresentativeMassNumber: " + hydrogen.RepresentativeMassNumber);

            Console.WriteLine();
            Console.WriteLine();

            // 1 being Hydrogen-1,
            // 2 being Deuterium 'Hydrogen-2',
            // 3 being Tritium 'Hydrogen-3'
            Console.WriteLine("Natural Abundance: " + hydrogen.Isotopes[1].NaturalAbundance);
            Console.WriteLine("NeutronCount: " + hydrogen.Isotopes[1].NeutronCount);
            Console.WriteLine("MassNumber: " + hydrogen.Isotopes[1].MassNumber);
            string x = hydrogen.Isotopes[1].HalfLifeSeconds?.ToString() ?? "N/A";
            Console.WriteLine("HalfLife-Seconds: " + x);
            Console.WriteLine("IsotopicMass: " + hydrogen.Isotopes[1].IsotopicMass);
        }
    }
}