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
        // Used for debugging and fast equality-checking in tests. Please override
        // appropriately when subclassing.
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class ValueAllele<T> : Allele where T : class
    {
        public T Value { get; set; }
        public override string ToString()
        {
            return this.Value.ToString();
        }
    }

    public class ColourAllele : Allele
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public override string ToString()
        {
            return $"{this.Name}: {this.Value}";
        }
    }

    public class ShadeAllele : ColourAllele
    {
        public int Saturation { get; set; }
        public override string ToString()
        {
            return $"{this.Name} v={this.Value} s={this.Saturation}";
        }
    }
}
