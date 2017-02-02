using DeenGames.Cataclysm.Core.Model.Genetics;
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
                var name = jsonObject.Name.Value;
                var type = jsonObject.Type.Value;
                var alleles = new List<Allele>();

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

                    alleles.Add(allele);
                }

                var gene = new Gene(name, type, alleles);
                toReturn.Add(gene);
            }

            return toReturn;
        }        
    }
}
