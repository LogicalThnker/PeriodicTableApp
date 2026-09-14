namespace PeriodicTableConsoleApp.ClassLib
{
    public static class PeriodicTable
    {
        private static readonly Dictionary<string, Element> elements =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["H"] = new Element
                {
                    Name = "Hydrogen",
                    Symbol = "H",
                    AtomicNumber = 1,
                    StandardAtomicWeight = 1.008,
                    MeltingPoint = 13.99,
                    Isotopes = new Dictionary<int, Isotope>
                    {
                        [1] = new Isotope
                        {
                            MassNumber = 1,
                            NeutronCount = 0,
                            IsotopicMass = 1.007825,
                            NaturalAbundance = 99.9885,
                            IsStable = true
                        },
                        [2] = new Isotope
                        {
                            MassNumber = 2,
                            NeutronCount = 1,
                            IsotopicMass = 2.014102,
                            NaturalAbundance = 0.0115,
                            IsStable = true
                        },
                        [3] = new Isotope
                        {
                            MassNumber = 3,
                            NeutronCount = 2,
                            IsotopicMass = 3.016049,
                            IsStable = false
                        }
                    }
                },
                ["He"] = new Element
                {
                    Name = "Helium",
                    Symbol = "He",
                    AtomicNumber = 2,
                    StandardAtomicWeight = 4.0026
                }
            };
        public static Element Get(string symbol) => elements[symbol];
    }
}
