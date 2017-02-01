namespace DeenGames.Cataclysm.Core.Model.Monster
{
    /// <summary>
    /// A prototype monster: the genome for a monster, with all genes (eg eyes can be red or blue).
    /// </summary>
    public class MonsterPrototype
    {
        // The list of genes this monster actualizes, and their genome
        public Genome Genome { get; private set; }

        // eg. Lion
        public string Type { get; private set; }

        public MonsterPrototype(string type)
        {
            this.Type = type;
        }
    }
}

