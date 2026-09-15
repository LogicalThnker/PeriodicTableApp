namespace PeriodicTableConsoleApp.ClassLib
{
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
        public int[]? OxidationStates { get; init; }
        public string? StandardState { get; init; }
        public double? MeltingPoint { get; init; }
        public double? BoilingPoint { get; init; }
        public double? Density { get; init; }
        public string? GroupBlock { get; init; }//ElementType
        public int? YearDiscovered { get; init; }
    }
    public class ElementTypes
    {
        public const string ReactiveNonMetal = "Reactive Nonmetal";
        public const string NobleGas = "Noble Gases";
        public const string Metalloids = "Metalloids";
        public const string PostTransitionMetals = "Post-Transition Metals";
        public const string TransitionMetals = "Transition Metals";
        public const string AlkalineEarthMetals = "Alkaline Earth Metals";
        public const string AlkaliMetals = "Alkali Metals";
        public const string Lanthanides = "Lanthanides";
        public const string Actinides = "Actinides";
        public const string UnkownProp = "Unknown Properties";
    }
}
