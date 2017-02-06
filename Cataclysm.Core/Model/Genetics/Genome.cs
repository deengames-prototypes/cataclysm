using DeenGames.Cataclysm.Core.Model.Monster;
using System.Collections.Generic;

namespace DeenGames.Cataclysm.Core.Model.Genetics
{
    public class Genome
    {
        public IEnumerable<Gene> Genes { get; private set; }

        public Genome(IEnumerable<Gene> genes)
        {
            this.Genes = genes;
        }

        public override string ToString()
        {
            return $"Genome: {string.Join(",", this.Genes)}";
        }
    }
}