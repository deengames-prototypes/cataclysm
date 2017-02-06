using DeenGames.Cataclysm.Core.Model.Genetics;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeenGames.Cataclysm.Core.UnitTests.Model.Genetics
{
    [TestFixture]
    public class GenomeTest
    {
        public void ContructorSetsGenes()
        {
            IList<Allele> alleles = new List<Allele>() { 
                new ValueAllele<string>() { Value  = "Red" },
                new ValueAllele<string>() { Value  = "Blue" },
            };

            var expectedGenes = new List<Gene>()
            {
                new Gene("Colour", "Dominance", alleles)
            };

            var actual = new Genome(expectedGenes);

            Assert.That(actual.Genes, Is.EqualTo(expectedGenes));
        }
    }
}
