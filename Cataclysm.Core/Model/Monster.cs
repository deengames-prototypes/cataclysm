namespace DeenGames.Cataclysm.Core.Model
{
    class Monster
    {
        // unique name given by the player
        public string Name { get; set; }

        // eg. Lion
        public string Type {get; private set; }
        
        public Monster(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }
    }
}