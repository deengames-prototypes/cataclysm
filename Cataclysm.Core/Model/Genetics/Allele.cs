using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeenGames.Cataclysm.Core.Model.Genetics
{
    // Base class, not supposed to be use directly
    public abstract class Allele
    {        
    }

    [DebuggerDisplay("{Value}")]
    public class ValueAllele<T> : Allele where T : class
    {
        public T Value { get; set; }
    }

    [DebuggerDisplay("{Name}: {Value}")]
    public class ColourAllele : Allele
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    [DebuggerDisplay("{Name} {Value}V, {Saturation}S")]
    public class ShadeAllele : ColourAllele
    {
        public int Saturation { get; set; }
    }
}
