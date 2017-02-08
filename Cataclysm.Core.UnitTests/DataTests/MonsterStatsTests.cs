using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeenGames.Cataclysm.Core.UnitTests
{
    [TestFixture]
    class MonsterStatsTests
    {
        public void MonstersAreInOrderOfStrength()
        {
            var stats = JsonConvert.DeserializeObject<List<MonsterData>>(File.ReadAllText("../../../Cataclysm/Assets/Data/monster_stats.json"));
            MonsterData previousStats = null;
            foreach (var monster in stats)
            {
                if (monster == stats.First())
                {
                    previousStats = monster;
                    continue;
                }

                Assert.That(monster.Total, Is.GreaterThanOrEqualTo(previousStats.Total), $"{monster.Name} ({monster.Total}) has less points than {previousStats.Name} ({previousStats.Total})");
                previousStats = monster;
            }
        }

        // TODO: this class will live in Core, not just in tests.
        private class MonsterData
        {
            public string Name { get; set; }
            public int HealthPoints { get; set; }
            public int SkillPoints { get; set; }
            public int Strength { get; set; }
            public int Defense { get; set; }
            public int Agility { get; set; }
            public int SDefense { get; set; }

            public int Total
            {
                get
                {
                    return this.Strength + this.Defense + this.Agility + this.SDefense;
                }
            }
        }
    }
}
