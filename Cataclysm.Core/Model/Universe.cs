using DeenGames.Cataclysm.Core.Model.Genetics;
using DeenGames.Cataclysm.Core.Model.Monster;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeenGames.Cataclysm.Core.Extensions;

namespace DeenGames.Cataclysm.Core.Model
{
    /// <summary>
    /// Our in-game universe. Includes a set of monster prototypes with randomized genes.
    /// You can seed this to always get the same universe back later.
    /// </summary>
    public class Universe
    {
        private readonly int seed;
        public List<MonsterPrototype> MonsterPrototypes { get; private set; } = new List<MonsterPrototype>();

        public Universe() : this(new Random().Next()) { }

        public Universe(int seed)
        {
            this.seed = seed;
            this.CreateMonsterPrototypes();
        }

        private void CreateMonsterPrototypes()
        {
            var names = Data.MonsterNames;
            var genePool = Data.GenesAndAlleles;
            var random = new Random(this.seed);

            if (names.Any() && genePool.Any())
            {
                this.MonsterPrototypes.Clear();

                // Assign a random portion of alleles to every monster
                CreateUniqueGenomes(names, genePool, random);

                // Find any alleles that didn't show up and add them randomly
                AssignUnusedAlleles(names, genePool, random);
            }
            else
            {
                throw new InvalidOperationException("Can't generate universe without loading data first.");
            }
        }

        private void AssignUnusedAlleles(IEnumerable<string> names, IEnumerable<Gene> genePool, Random random)
        {
            IList<Gene> unused = new List<Gene>();
            foreach (var gene in genePool)
            {
                unused.Add(gene);
            }

            foreach (var monster in this.MonsterPrototypes)
            {
                foreach (var currentGene in monster.Genome.Genes)
                {
                    var usedAlleles = currentGene.Alleles;
                    var gene = unused.Single(g => g.Name == currentGene.Name);
                    foreach (var a in usedAlleles)
                    {
                        //gene.Alleles.ToList().Remove(a);
                        gene.Alleles = gene.Alleles.Where(b => b != a).ToList();
                        if (a.ToString().ToLower().Contains("fire"))
                        {
                            System.Diagnostics.Debugger.Break();
                        }
                    }
                }
            }

            unused = unused.Where(g => g.Alleles.Any()).ToList();
        }

        private void CreateUniqueGenomes(IEnumerable<string> names, IEnumerable<Gene> genePool, Random random)
        {
            foreach (var name in names)
            {
                var genome = new Genome(genePool);
                var genes = new List<Gene>();

                foreach (var gene in genome.Genes)
                {
                    // Don't be predictable (eg. all monsters have two alleles for size). Be unique
                    // (eg. all monsters have different sets of alleles). To balance this, for each
                    // gene, we pick between 1/4 and 1/2 of all possible alleles.
                    int total = gene.Alleles.Count();
                    int min = (int)Math.Ceiling(total / 4.0d);
                    int max = (int)Math.Ceiling(total / 2.0d);
                    var count = random.Next(min, max + 1);

                    var alleles = gene.Alleles.Shuffle(random).Take(count);
                    // Pick a random allele as the dominant one
                    var pick = random.Next(gene.Alleles.Count());
                    var currentAllele = gene.Alleles.Skip(pick).First();

                    genes.Add(new Genetics.Gene(gene.Name, gene.Type, alleles.ToList()) { CurrentAllele = currentAllele });
                }

                var prototype = new MonsterPrototype(name, new Genome(genes));
                this.MonsterPrototypes.Add(prototype);
            }

            File.WriteAllText("Prototypes.txt", string.Join(",", this.MonsterPrototypes));
        }
    }
}
