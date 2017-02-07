using DeenGames.Cataclysm.Core.Model.Genetics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeenGames.Cataclysm.Core
{
    // Should be static. Singleton is a bit easier to test (reset). Consider this pseudo-singleton.
    public class Data
    {
        /// <summary>
        /// Returns the last-created instance of data. In production code, there should only be one of these.
        /// </summary>
        public static Data Instance { get; private set; }

        public IEnumerable<Gene> GenesAndAlleles { get; set; }
        public IEnumerable<string> MonsterNames { get; set; }

        public Data(IEnumerable<string> monsterNames, string genesAndAllelesJson)
        {
            Data.Instance = this;
            this.MonsterNames = monsterNames.Where(s => !s.StartsWith("#") && !string.IsNullOrWhiteSpace(s));
            this.GenesAndAlleles = ParseGenesAndAlleles(JsonConvert.DeserializeObject<dynamic>(genesAndAllelesJson).Genes);
        }

        private List<Gene> ParseGenesAndAlleles(dynamic genes)
        {
            var toReturn = new List<Gene>();

            foreach (var jsonObject in genes)
            {
                string name = jsonObject.Name.Value;
                string type = jsonObject.Type.Value;
                var alleles = new List<Allele>();

                foreach (var jsonAllele in jsonObject.Alleles)
                {
                    Allele allele;
                    if (jsonAllele.GetType() == typeof(JValue))
                    {
                        allele = new ValueAllele<string>() { Value = jsonAllele };
                    }
                    else
                    {
                        if (name.ToUpper() == "COLOUR")
                        {
                            allele = new ColourAllele() { Name = jsonAllele.Name.ToString(), Value = jsonAllele.Value };
                        }
                        else if (name.ToUpper() == "SHADE")
                        {
                            allele = new ShadeAllele() { Name = jsonAllele.Name.ToString(), Saturation = (int)jsonAllele.Saturation.Value, Value = (int)jsonAllele.Value.Value };
                        }
                        else
                        {
                            throw new InvalidDataException($"Not sure how to parse allele: {jsonObject}");
                        }
                    }

                    alleles.Add(allele);
                }

                var gene = new Gene(name, type, alleles);
                toReturn.Add(gene);
            }

            return toReturn;
        }        
    }
}
