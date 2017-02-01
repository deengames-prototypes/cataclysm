using DeenGames.Cataclysm.Core.Model.Monster;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            
        }
    }
}
