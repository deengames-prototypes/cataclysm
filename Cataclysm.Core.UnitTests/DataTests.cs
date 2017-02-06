using DeenGames.Cataclysm.Core.Model.Genetics;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeenGames.Cataclysm.Core.UnitTests
{
    [TestFixture]
    public class DataTests
    {
        [Test]
        public void ConstructorSetsMonsterNames()
        {
            var expectedNames = new string[] { "Lion", "Tiger", "Centipede " };
            var data = new Data(expectedNames, "{ 'Genes': [] }");
            var actual = data.MonsterNames;
            Assert.That(expectedNames.Length, Is.EqualTo(actual.Count()));

            foreach (var expected in expectedNames)
            {
                Assert.That(actual.Contains(expected));
            }
        }

        [Test]
        public void ConstructorSetsGenes()
        {
            // Size (s/m/l) colour(red=>0, orange=>30, ... purple=>300) and shade(normal/black)
            // Make sure this test covers at least one type of each gene!
            var genesJson = "{'Genes':[{'Name':'size','Alleles':['small','large'],'Type':'dominance'},{'Name':'colour','Alleles':[{'Name':'Red','Value':0},{'Name':'Orange','Value':30},{'Name':'Blue','Value':240},{'Name':'Purple','Value':270}],'Type':'co-dominance'},{'Name':'shade','Alleles':[{'Name':'dark','Saturation':100,'Value':50},{'Name':'black','Saturation':0,'Value':0}],'Type':'custom'}]}";
            var names = new string[0];
            var data = new Data(names, genesJson);
            var actual = data.GenesAndAlleles;

            Assert.That(actual.Count(), Is.EqualTo(3));

            var sizeGene = actual.Single(s => s.Name == "size");
            Assert.That(sizeGene.Type, Is.EqualTo("dominance"));
            Assert.That(sizeGene.Alleles.Count, Is.EqualTo(2));
            IList<ValueAllele<string>> sizeAlleles = sizeGene.Alleles.Select(a => (ValueAllele<string>)a).ToList();

            Assert.That(sizeAlleles.Any(s => s.Value == "small"));
            Assert.That(sizeAlleles.Any(s => s.Value == "large"));

            var colourGene = actual.Single(s => s.Name == "colour");
            Assert.That(colourGene.Type, Is.EqualTo("co-dominance"));
            Assert.That(colourGene.Alleles.Count, Is.EqualTo(4));
            IList<ColourAllele> colourAlleles = colourGene.Alleles.Select(a => (ColourAllele)a).ToList();

            Assert.That(colourAlleles.Single(c => c.Name == "Red").Value == 0);
            Assert.That(colourAlleles.Single(c => c.Name == "Orange").Value == 30);
            Assert.That(colourAlleles.Single(c => c.Name == "Blue").Value == 240);
            Assert.That(colourAlleles.Single(c => c.Name == "Purple").Value == 270);

            var shadeGene = actual.Single(s => s.Name == "shade");
            Assert.That(shadeGene.Type, Is.EqualTo("custom"));
            Assert.That(shadeGene.Alleles.Count, Is.EqualTo(2));
            IList<ShadeAllele> shadeAlleles = shadeGene.Alleles.Select(a => (ShadeAllele)a).ToList();

            // [{'Name':'normal','Saturation':100,'Value':100},{'Name':'black','Saturation':0,'Value':0}],'Type':'custom'}]
            var black = shadeAlleles.Single(s => s.Name == "black");
            Assert.That(black.Saturation, Is.EqualTo(0));
            Assert.That(black.Value, Is.EqualTo(0));

            var dark = shadeAlleles.Single(s => s.Name == "dark");
            Assert.That(dark.Saturation, Is.EqualTo(100));
            Assert.That(dark.Value, Is.EqualTo(50));
        }
    }
}
