package deengames.cataclysmmonsters.scenes;

import kha.Assets;
import kha.Color;
import kha.Framebuffer;
import kha.Image;
import kha.Scaler;
import kha.Scheduler;
import kha.System;

import Main;

class Unknown {
	private var backbuffer:Image;
	private var isInitialized:Bool = false;

	public function new() {
		System.notifyOnRender(render);
		Scheduler.addTimeTask(update, 0, 1 / 60);
		Assets.loadEverything(loadingFinished);
	}

	public function update(): Void {
		trace("Update!");
	}

	public function render(framebuffer: Framebuffer): Void {
		var g = backbuffer.g2;
		g.begin();
		g.color = Color.White;
		g.clear(Color.Black);

		// Draw everything to the back buffer here

		g.end();

		// Draw back buffer to screen
		framebuffer.g2.begin();
		Scaler.scale(backbuffer, framebuffer, System.screenRotation);
		framebuffer.g2.end();
	}

	private function loadingFinished(): Void {
		this.isInitialized = true;
		this.backbuffer = Image.createRenderTarget(Main.GAME_WIDTH, Main.GAME_HEIGHT);
	}
}
