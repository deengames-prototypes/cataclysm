package deengames.cataclysmmonsters.model;

class Monster {

    // unique name given by the player
    public var name(default, null):String;

    // eg. Lion
    public var type(default, null):String;
    
    public function new(name:String, type:String)
    {
        this.type = type;
    }
}
