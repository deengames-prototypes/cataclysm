using DeenGames.Cataclysm.Core.Model.Monster;
using System.Collections.Generic;

namespace DeenGames.Cataclysm.Core.Model.Genetics
{
    public class Genome
    {
        public MonsterPrototype Prototype { get; private set; }
        public List<Gene> Genes { get; private set; }

        public Genome(MonsterPrototype prototype, List<Gene> genes)
        {
            this.Prototype = prototype;
            this.Genes = genes;
        }
    }
}