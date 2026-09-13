namespace PeriodicTableConsoleApp.ClassLib
{
    public class Isotope
    {
        public required int MassNumber { get; init; }
        public required int NeutronCount { get; init; }
        public double? IsotopicMass { get; init; }
        public double? NaturalAbundance { get; init; }
        public bool IsStable { get; init; }
        public double? HalfLifeSeconds { get; init; }
    }
}
