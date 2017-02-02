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
