package Sources.deengames.cataclysmmonsters.model;

class Genome
{
    // Type we're for
    public var monsterType(default, null):String;
    // List of genes and valid alleles
    public var genes(default, null):Array<Gene> = new Array<Gene>();
}