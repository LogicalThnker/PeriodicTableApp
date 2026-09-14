namespace PeriodicTableConsoleApp.ClassLib
{
    public class Element
    {
        public required string Name { get; init; }
        public required string Symbol { get; init; }
        public required string ElementType { get; init; }
        public required int AtomicNumber { get; init; }
        public double? StandardAtomicWeight { get; init; }
        public int? RepresentativeMassNumber { get; init; }

        // This is in 'Kelvin' not C/F
        // 13.99K = -259.16C or -434.49F
        public double? MeltingPoint { get; init; }
        public double? BoilingPoint { get; init; }
        public Dictionary<int, Isotope> Isotopes { get; init; } = new();
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
