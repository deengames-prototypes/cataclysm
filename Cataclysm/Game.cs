using DeenGames.Cataclysm.ConsoleUi.Screens;
using Ninject;
using RogueSharp.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeenGames.Cataclysm.ConsoleUi
{
    class Game
    {
        // Methodology: never use .Resolve. Let DI wire up objects.
        internal static readonly IKernel Kernel = new StandardKernel();

        public Game()
        {
            // TODO: read class name from config
            // TODO: wiring up core classes shouldn't be done in the UI
            Kernel.Bind<IRandom>().To<RogueSharp.Random.DotNetRandom>();
        }

        public void Run()
        {
            // Setup the engine and creat the main window.
            SadConsole.Engine.Initialize("Assets/Fonts/IBM.font", Config.GameWidth, Config.GameHeight);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Engine.EngineStart += (sender, eventArgs) =>
            {
                // Clear the default console
                SadConsole.Engine.ConsoleRenderStack.Clear();
                SadConsole.Engine.ActiveConsole = null;

                SadConsole.Engine.ConsoleRenderStack.Add(new AreaScreen());
            };

            // Hook the update event that happens each frame so we can trap keys and respond.
            SadConsole.Engine.EngineUpdated += (sender, eventArgs) =>
            {
            };

            SadConsole.Engine.ToggleFullScreen();

            // Start the game.
            SadConsole.Engine.Run();
        }
    }
}
