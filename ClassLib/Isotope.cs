namespace PeriodicTableConsoleApp.ClassLib
{
    public class IsotopeList
    {
        public Dictionary<int, List<Isotope>>? IsotopeBuckets { get; init; }
    }

    public class Isotope
    {
        public required int z { get; init; }//Atomic Number
        public required int n { get; init; }//Neutrons
        public required int a { get; init; }//Isotope
        public required string name { get; init; }//Name of Isotope
        public Levels[]? levels { get; init; }

    }
    public class Levels
    {
        public required Energy energy { get; init; }//Energy container
        public required HalfLife halfLife { get; init; }//Halflife container
    }
    public class Energy
    {
        public double? value { get; init; }//Energy
        public string? unit { get; init; }//Units of Energy, f.e "Kev"
    }
    public class HalfLife
    {
        public string? value { get; init; }//Halflife
    }
}
