using System.Collections.Generic;

namespace DeenGames.Cataclysm.Core.Model
{
    class Gene
    {
        // eg. colour
        public string Name { get; private set; }

        // eg. [red, green, blue]
        public List<string> Alleles { get; private set; }

        public Gene(string name, List<string> alleles)
        {
            this.Name = name;
            this.Alleles = alleles;
        }
    }
}