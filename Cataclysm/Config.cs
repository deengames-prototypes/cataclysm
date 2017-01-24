using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeenGames.Cataclysm.ConsoleUi
{
    /// <summary>
    /// Gets global configuration. Yes, it's evil, because it's used everywhere.
    /// Checks config.json and returns that value (if present), otherwise returns the constant value.
    /// </summary>
    public static class Config
    {
        public static int GameWidth { get; } = 80;
        public static int GameHeight { get; } = 25;
    }
}
