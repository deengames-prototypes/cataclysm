using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeenGames.Cataclysm.Core.Model.Genetics
{
    public class Allele
    {
        // Either a plain string value (eg. "fire") or a complex object (eg. {"name": "normal", "saturation": 100, "value": 100 })
        public string Value { get; set; }

        // TODO: break out specific allele types (eg. colour, shade) as sub-types
        public dynamic Object { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(this.Value))
            {
                return this.Object.ToString();
            }
            else
            {
                return this.Value;
            }
        }
    }
}
