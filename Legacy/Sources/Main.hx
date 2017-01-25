package;

import kha.System;

class Main {
	public static inline var GAME_WIDTH:Int = 1024;
	public static inline var GAME_HEIGHT:Int = 576;
	
	public static function main() {		
		System.init("Unknown", Main.GAME_WIDTH, Main.GAME_HEIGHT, function () {
			new deengames.cataclysmmonsters.scenes.Unknown();
		});
	}
}
