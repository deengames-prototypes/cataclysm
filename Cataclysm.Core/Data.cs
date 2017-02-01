using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeenGames.Cataclysm.Core
{
    public static class Data
    {
        public static List<Gene> GenesAndAlleles { get; set; }

        public static void Initialize(string genesAndAllelesJson)
        {
            GenesAndAlleles = ParseGenesAndAlleles(JsonConvert.DeserializeObject<dynamic>(genesAndAllelesJson).Genes);
        }

        private static List<Gene> ParseGenesAndAlleles(dynamic genes)
        {
            var toReturn = new List<Gene>();

            foreach (var jsonObject in genes)
            {
                var gene = new Gene();
                gene.Name = jsonObject.Name;
                gene.Type = jsonObject.Type;
                gene.Alleles = new List<Allele>();

                foreach (var jsonAllele in jsonObject.Alleles)
                {
                    var allele = new Allele();
                    if (jsonAllele.GetType() == typeof(JValue))
                    {
                        allele.Value = jsonAllele;
                    } else
                    {
                        allele.Object = jsonAllele;
                    }

                    gene.Alleles.Add(allele);
                }

                toReturn.Add(gene);
            }

            return toReturn;
        }

        public class Gene
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public List<Allele> Alleles { get; set; }
        }

        public class Allele
        {
            // Either a plain string value (eg. "fire") or a complex object (eg. {"name": "normal", "saturation": 100, "value": 100 })
            public string Value { get; set; }
            public dynamic Object { get; set; }
        }
    }
}
