package deengames.cataclysmmonsters.model;

class Gene
{
    public var name(default, null):String; // eg. color
    // eg. [red, green, blue]
    public var alleles(default, null):Array<String> = new Array<String>();

    public function new(name:String, alleles:Array<String>)
    {
        this.name = name;
        this.alleles = alleles;
    }
}