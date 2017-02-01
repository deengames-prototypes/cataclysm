namespace DeenGames.Cataclysm.Core.Model.Monster
{
    /// <summary>
    /// An actual, real monster that the player fights or owns. With its genes (eg. red eyes).
    /// </summary>
    class Monster : MonsterPrototype
    {
        // unique name given by the player
        public string Name { get; set; }

        public Monster(string name, string type) : base(type)
        {
            this.Name = name;
        }
    }
}