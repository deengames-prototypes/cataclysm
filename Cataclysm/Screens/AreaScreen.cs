using Microsoft.Xna.Framework;
using SadConsole.Consoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = SadConsole.Consoles.Console;
using SadConsole.Input;
using DeenGames.Cataclysm.ConsoleUi.Consoles;

namespace DeenGames.Cataclysm.ConsoleUi.Screens
{
    /// <summary>
    /// An area, like a dungeon floor, town, or an outdoor place with enemies/NPCs
    /// </summary>
    class AreaScreen : ConsoleList
    {
        private readonly MessageAndStatusConsole messageAndStatusConsole;
        private readonly AreaViewConsole mainView;

        public AreaScreen()
        {
            // Clear the default console
            SadConsole.Engine.ConsoleRenderStack.Clear();
            
            messageAndStatusConsole = new MessageAndStatusConsole();
            mainView = new AreaViewConsole(Config.GameWidth, Config.GameHeight - messageAndStatusConsole.Height, Config.GameWidth * 2, Config.GameHeight * 2);

            mainView.Position = new Point(0, 0);
            messageAndStatusConsole.Position = new Point(0, mainView.Height);

            this.Add(mainView);
            this.Add(messageAndStatusConsole);

            messageAndStatusConsole.ShowMessage("First message");
            messageAndStatusConsole.ShowMessage("SECOND message");
            messageAndStatusConsole.ShowMessage("3rd message");
        }
    }
}
