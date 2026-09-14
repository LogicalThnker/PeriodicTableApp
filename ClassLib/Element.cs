namespace PeriodicTableConsoleApp.ClassLib
{
    public class Element
    {
        public required string Name { get; init; }
        public required string Symbol { get; init; }
        public required int AtomicNumber { get; init; }
        public double? StandardAtomicWeight { get; init; }
        public int? RepresentativeMassNumber { get; init; }

        // This is in 'Kelvin' not C/F
        // 13.99K = -259.16C or -434.49F
        public double? MeltingPoint { get; init; }
        public Dictionary<int, Isotope> Isotopes { get; init; } = new();
    }
}
