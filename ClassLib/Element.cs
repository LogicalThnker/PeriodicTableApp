namespace PeriodicTableConsoleApp.ClassLib
{
    public class PeriodicTableList
    {
        public Element[]? elements;
    }
    public class Element
    {
        public required int AtomicNumber { get; init; }
        public required string Symbol { get; init; }
        public required string Name { get; init; }
        public required double AtomicMass { get; init; }
        public string? CPKHexColor { get; init; }
        public string? ElectronConfiguration { get; init; }
        public double? Electronegativity { get; init; }
        public double? AtomicRadius { get; init; }
        public double? IonizationEnergy { get; init; }
        public double? ElectronAffinity { get; init; }
        public string? StandardState { get; init; }
        public double? MeltingPoint { get; init; }
        public double? BoilingPoint { get; init; }
        public double? Density { get; init; }
        public string? GroupBlock { get; init; }//ElementType
        public string? YearDiscovered { get; init; }
        public int[]? OxidationStates { get; init; }
        public Isotope[]? Isotopes { get; set; }
    }
}
