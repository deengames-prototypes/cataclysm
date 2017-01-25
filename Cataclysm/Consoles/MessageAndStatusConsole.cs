using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeenGames.Cataclysm.ConsoleUi.Consoles
{
    /// <summary>
    /// Shows game messages to the player (eg. who attacked who). Also shows health.
    /// </summary>
    class MessageAndStatusConsole : SadConsole.Consoles.Console
    {
        private const int MyHeight = 3;

        public MessageAndStatusConsole() : base(Config.GameWidth, MyHeight)
        {
            this.CanUseKeyboard = false;
        }

        public void ShowMessage(string text)
        {
            this.ShiftDown();
            this.Print(1, 0, text);
        }
    }
}
