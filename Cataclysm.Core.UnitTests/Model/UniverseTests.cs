using DeenGames.Cataclysm.Core.Model;
using DeenGames.Cataclysm.Core.Model.Genetics;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeenGames.Cataclysm.Core.UnitTests.Model
{
    [TestFixture]
    public class UniverseTests
    {            
        private const string GeneDataJson = "{'Genes':[{'Name':'size','Alleles':['small','large'],'Type':'dominance'},{'Name':'colour','Alleles':[{'Name':'Red','Value':0},{'Name':'Orange','Value':30},{'Name':'Blue','Value':240},{'Name':'Purple','Value':270}],'Type':'co-dominance'},{'Name':'shade','Alleles':[{'Name':'dark','Saturation':100,'Value':50},{'Name':'black','Saturation':0,'Value':0}],'Type':'custom'}]}";
        private readonly string[] MonsterNames = new string[] { "Cow", "Gull", "Goose", "Mouse" };

        public void ConstructorCreatesMonstersWithDifferentGenomes()
        {
            new Data(MonsterNames, GeneDataJson);

            var u = new Universe();
            var monsters = u.MonsterPrototypes;
            // Look consecutively through monsters. If they're ALL the same, fail.
            for (var i = 0; i < monsters.Count; i++)
            {
                for (var j = i + 1; j < monsters.Count; j++)
                {
                    var m1 = monsters[i].Genome.Genes;
                    var m2 = monsters[j].Genome.Genes;

                    // Should have equal number of genes
                    Assert.That(m1.Count(), Is.EqualTo(m2.Count()));

                    foreach (var m1gene in m1)
                    {
                        var m2gene = m2.Single(g => g.Name == m1gene.Name);
                        if (m1gene.Alleles.Count == m2gene.Alleles.Count())
                        {
                            bool areAllSame = m1gene.Alleles.All(a => m2gene.Alleles.Contains(a));
                            if (!areAllSame)
                            {
                                Assert.IsTrue(true);
                                return;
                            }
                        } else {
                            Assert.IsTrue(true);
                            return;
                        }
                    }
                }
            }

            Assert.Fail($"Generated {monsters.Count} monsters and they all gave the same genome.");
        }

        [Test]
        public void ConstructorUsesAllAllelesInGenome()
        {
            var genome = new Data(MonsterNames, GeneDataJson).GenesAndAlleles;
            var u = new Universe();
            var monsters = u.MonsterPrototypes;

            foreach (var monster in monsters)
            {
                foreach (var gene in monster.Genome.Genes)
                {
                    foreach (var allele in gene.Alleles)
                    {
                        genome.Single(g => g.Name == gene.Name).Alleles.Remove(allele);
                    }
                }
            }

            var unused = genome.Where(g => g.Alleles.Count > 0);
            Assert.That(unused.Count(), Is.EqualTo(0));
        }

        [Test]
        public void MonstersGetRandomCurrentAlleles()
        {
            var gene = "colour";
            new Data(MonsterNames, GeneDataJson);
            var m1 = new Universe(1234).MonsterPrototypes.First();
            var m1Current = (ColourAllele)m1.Genome.Genes.Single(g => g.Name == gene).CurrentAllele;

            var m2 = new Universe(4321).MonsterPrototypes.Single(m => m.Type == m1.Type);
            var m2Current = (ColourAllele)m2.Genome.Genes.Single(g => g.Name == gene).CurrentAllele;

            Assert.That(m1Current.Name != m2Current.Name || m1Current.Value != m2Current.Value, $"{m1Current.Name} {m1Current.Value} vs {m2Current.Name} {m2Current.Value}");
        }

        [Test]
        public void ConstructorWithSeedGeneratesSamePrototypes()
        {
            new Data(MonsterNames, GeneDataJson);
            var u1 = new Universe(12345);
            var u2 = new Universe(12345);

            Assert.That(u1.MonsterPrototypes.Count, Is.EqualTo(u2.MonsterPrototypes.Count));
            for (var i = 0; i < u1.MonsterPrototypes.Count; i++)
            {
                var m1 = u1.MonsterPrototypes[i];
                var m2 = u2.MonsterPrototypes[i];

                Assert.That(m1.Genome.Genes.Count(), Is.EqualTo(m2.Genome.Genes.Count()));
                for (var j = 0; j < m1.Genome.Genes.Count(); j++)
                {
                    var m1Gene = m1.Genome.Genes.ElementAt(j);
                    var m2Gene = m2.Genome.Genes.ElementAt(j);

                    Assert.That(m1Gene.CurrentAllele, Is.EqualTo(m2Gene.CurrentAllele));

                    for (var k = 0; k < m1Gene.Alleles.Count; k++)
                    {
                        var m1Allele = m1Gene.Alleles[k];
                        var m2Allele = m2Gene.Alleles[k];
                        // Rely on ToString to compare alleles
                        Assert.That(m1Allele.ToString(), Is.EqualTo(m2Allele.ToString()));
                    }
                }
            }
        }
    }
}
