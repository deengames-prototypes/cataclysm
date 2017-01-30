using System.Collections.Generic;

namespace DeenGames.Cataclysm.Core.Model
{
    class Genome
    {
        // Type we're for
        public string MonsterType { get; private set; }

        // List of genes and valid alleles
        // eg. Colour(red, green, blue) and Size(medium, large).
        public List<Gene> Genes { get; private set; }

        public Genome(string monsterType, List<Gene> genes)
        {
            this.MonsterType = monsterType;
            this.Genes = genes;
        }
    }
}