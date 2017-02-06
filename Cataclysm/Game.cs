using DeenGames.Cataclysm.ConsoleUi.Screens;
using DeenGames.Cataclysm.Core;
using DeenGames.Cataclysm.Core.Model;
using Ninject;
using RogueSharp.Random;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeenGames.Cataclysm.ConsoleUi
{
    class Game
    {
        // Methodology: never use .Resolve. Let DI wire up objects.
        internal static readonly IKernel Kernel = new StandardKernel();
        private int UniverseSeed = 1024768;

        public Game()
        {
            // TODO: read class name from config
            // TODO: wiring up core classes shouldn't be done in the UI
            Kernel.Bind<IRandom>().To<RogueSharp.Random.DotNetRandom>();
        }

        public void Run()
        {
            // Generate our persistent world. The Data class stores a static instance, so we don't need to keep the result of this.
            new Data(File.ReadAllLines("Assets/data/monster_names.txt"), File.ReadAllText("Assets/data/genes_and_alleles.json"));
            var universe = new Universe(UniverseSeed);

            // Setup the engine and creat the main window.
            SadConsole.Engine.Initialize("Assets/Fonts/IBM.font", Config.GameWidth, Config.GameHeight);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Engine.EngineStart += (sender, eventArgs) =>
            {
                // Clear the default console
                SadConsole.Engine.ConsoleRenderStack.Clear();
                SadConsole.Engine.ActiveConsole = null;

                //SadConsole.Engine.ToggleFullScreen();            

                SadConsole.Engine.ConsoleRenderStack.Add(new AreaScreen());
            };

            // Hook the update event that happens each frame so we can trap keys and respond.
            SadConsole.Engine.EngineUpdated += (sender, eventArgs) =>
            {
            };

            // Start the game.
            SadConsole.Engine.Run();
        }
    }
}
