namespace PeriodicTableConsoleApp.ClassLib
{
    public class Isotope
    {
        public string? Name { get; set; }
        public int? MassNumber { get; init; }
        public int? NeutronCount { get; init; }
        public double? IsotopicMass { get; init; }
        public double? NaturalAbundance { get; init; }
        public bool IsStable { get; init; }
        public double? HalfLifeSeconds { get; init; }
        public double? BoilingPoint {  get; init; }
        public double? LambdaPoint { get; init; }
    }
}
