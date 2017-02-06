using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DeenGames.Cataclysm.Core.Model.Genetics
{
    public class Gene
    {
        // eg. colour
        public string Name { get; private set; }

        // dominance, co-dominance, or pair-with-inhibitor
        public string Type { get; private set; }

        // All possible values, eg. [red, green, blue]
        public IList<Allele> Alleles { get; set; }
        
        // eg. [red]. Doesn't apply to prototypes.
        public Allele CurrentAllele { get; set; }

        public Gene(string name, string type, IList<Allele> alleles)
        {
            this.Name = name;
            this.Alleles = alleles;
            this.Type = type;
        }

        public override string ToString()
        {
            return $"{this.Name} ({this.Type}): ({this.Alleles.Count()}) {string.Join("/", this.Alleles)}";
        }
    }
}