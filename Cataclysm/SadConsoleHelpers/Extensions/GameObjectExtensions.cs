using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Consoles;
using SadConsole.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeenGames.Cataclysm.ConsoleUi.SadConsoleHelpers.Extensions
{
    static class GameObjectExtensions
    {
        /// <summary>
        /// Pass in a character, eg. '@'
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="character"></param>
        /// <param name="color"></param>
        public static void DrawAs(this GameObject obj, int character, Color color)
        {
            var animation = new AnimatedTextSurface("default", 1, 1, Engine.DefaultFont);
            animation.CreateFrame();
            animation.CurrentFrame[0].Foreground = color;
            animation.CurrentFrame[0].GlyphIndex = character;
            obj.Animation = animation;
        }

        public static void Move(this GameObject obj, int x, int y)
        {
            obj.Position = new Point(x, y);
        }
    }
}
