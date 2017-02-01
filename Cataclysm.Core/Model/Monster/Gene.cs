using System.Collections.Generic;

namespace DeenGames.Cataclysm.Core.Model.Monster
{
    public class Gene
    {
        // eg. colour
        public string Name { get; private set; }

        // All possible values, eg. [red, green, blue]
        public List<string> Alleles { get; private set; }
        // eg. [red]
        public string CurrentAllele { get; private set; }

        public Gene(string name, List<string> alleles)
        {
            this.Name = name;
            this.Alleles = alleles;
        }
    }
}