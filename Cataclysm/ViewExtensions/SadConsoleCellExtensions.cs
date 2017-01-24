using SadConsole.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeenGames.Cataclysm.ConsoleUi.ViewExtensions
{
    static class SadConsoleCellExtensions
    {
        public static void ApplyEffect(this SadConsole.Cell cell, ICellEffect effect)
        {
            cell.ClearEffect();
            cell.Effect = effect;
            cell.Effect.Apply(cell);
        }

        public static void ClearEffect(this SadConsole.Cell cell)
        {
            if (cell.Effect != null)
            {
                cell.Effect.Clear(cell);
            }
            cell.Effect = null;
        }
    }
}
