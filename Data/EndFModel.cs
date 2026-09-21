using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicTableConsoleApp.Data
{
    public class EndFModel
    {
        public required int ZA { get; init; }//3006
        public int Z => ZA / 1000;//3
        public int A => ZA % 1000;//6
    }
}
