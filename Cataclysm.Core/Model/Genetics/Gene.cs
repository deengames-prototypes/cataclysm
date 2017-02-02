using System.Collections.Generic;
using System.Diagnostics;

namespace DeenGames.Cataclysm.Core.Model.Genetics
{
    [DebuggerDisplay("{Name} ({Type}): {Alleles.Count} alleles")]
    public class Gene
    {
        // eg. colour
        public string Name { get; private set; }

        // dominance, co-dominance, or pair-with-inhibitor
        public string Type { get; private set; }

        // All possible values, eg. [red, green, blue]
        public List<Allele> Alleles { get; private set; }
        // eg. [red]
        public string CurrentAllele { get; private set; }

        public Gene(string name, string type, List<Allele> alleles)
        {
            this.Name = name;
            this.Alleles = alleles;
            this.Type = type;
        }
    }
}