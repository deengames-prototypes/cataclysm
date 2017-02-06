meta-id: 34a724fe0093fd8f1f60d8e1e57afc83aa33fd54

meta-title: Week 0: Prototyping and Micro-Console
meta-publishedOn: 2017-01-27

I created Cataclysm with the goal of working consistently (albeit slowly) on an ASCII roguelike. I chose the theme of monster-collection and the educational theme of genetics, since these two work well together and both interest me. In truth, the idea for Cataclysm came to me years ago, although I never sat down to work on it.

That all changed on January 24th; after a culmination of prototyping experiments in Haxe (HaxeFlixel and Kha), which proved that rendering console-like graphics using text could not work at sustained framerates greather than single-digits, I decided to let go of the goal of being able to play the final product in the web, and C#/RogueSharp won.

During the weeks up to and including the 24th, I created a basic shell roguelike (based on the RogueSharp tutorials), and added experimental full-screen support and gamepad support (Logitech F310). I also tried (hard!) to get the entire toolchain to build on Linux; with some effort, it builds with Visual Studio Code (via `xbuild`) and MonoDevelop (5.1 only; 6.x ships with Flatpak, which runs in a self-enclosed container and can't interact with the graphics DLLs necessary to run the game).

The final game builds and runs on Linux and Windows.

You may know that I experimented with building a small, cheap, [Linux-based gaming console](https://github.com/ashes999/linux-micro-console). Since the first game for that didn't pan out (audio crashes due to OpenAL drivers), this week, I decided to test if my new roguelike *could* work on that console.

This week, I:

- Updated to the latest version of SadConsole
- Integrated and verified gamepad support for Logitech F310
- Tested and verified full-screen support

While I haven't run the game on the console, I proved that I *could* run it, and it should work as expected. (My development box is a Linux VM, and Mono should run identically on both systems.)

With the stage set, next week, I can start to work on the actual core educational part of the game: creating the genetics-related classes and generating randomized monsters.

Until next week!