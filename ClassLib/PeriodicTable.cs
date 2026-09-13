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
                    MeltingPoint = 13.99
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
