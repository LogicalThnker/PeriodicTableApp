using Newtonsoft.Json;

namespace PeriodicTableConsoleApp.ClassLib
{
    public class IsotopeList
    {
        public Dictionary<int, List<Isotope>>? IsotopeBuckets { get; init; }
    }

    public class NuDatData
    {
        public List<Isotope>? nuclides { get; init; }
    }

    public class Isotope
    {
        public required string name { get; init; }//Name of Isotope
        public required int z { get; init; }//Atomic Number
        public required int n { get; init; }//Neutrons
        public required int a { get; init; }//Isotope
        public Levels[]? levels { get; init; }
        public Extras? bindingEnergy { get; init; }
        public SeparationEnergies? separationEnergies { get; init; }
        public QValues? qValues { get; init; }
        public Extras? pairingGap { get; init; }
        public FissionYields? fissionYields { get; init; }
    }
    public class QValues
    {
        public Extras? alpha { get; init; }
        public Extras? betaMinus { get; init; }
        public Extras? electronCapture { get; init; }
        public Extras? positronEmission { get; init; }
    }
    public class SeparationEnergies
    {
        public Extras? neutronSeparationEnergy { get; init; }
        public Extras? protonSeparationEnergy { get; init; }
        public Extras? twoNeutronSeparationEnergy { get; init; }
        public Extras? twoProtonSeparationEnergy { get; init; }
    }
    public class FissionYields
    {
        public Extras? FY235U { get; init; }
        public Extras? FY239Pu { get; init; }
        public Extras? FY252Cf { get; init; }
        public Extras? FY238U { get; init; }
        public Extras? cFY235U { get; init; }
        public Extras? cFY239Pu { get; init; }
        public Extras? cFY252Cf { get; init; }
        public Extras? cFY238U { get; init; }
    }
    public class Levels
    {
        public required Energy energy { get; init; }//Energy container
        public required HalfLife halfLife { get; init; }//Halflife container
    }
    public class Extras
    {
        public double? value { get; init; }
        public string? unit { get; init; }
        public double? uncertainty { get; init; }
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
