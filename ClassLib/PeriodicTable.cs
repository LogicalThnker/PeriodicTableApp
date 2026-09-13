using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    AtomicMass = 1.008,
                    MeltingPoint = 13.99
                },
                ["He"] = new Element
                {
                    Name = "Helium",
                    Symbol = "He",
                    AtomicNumber = 2,
                    AtomicMass = 4.0026
                }
            };
        public static Element Get(string symbol) => elements[symbol];
    }
}
