using MiniRPG.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.UI
{
    public static class ConsoleUI
    {
        public static string PrintBar(string label, int currentValue, int maxValue)
        {
            int barLength = 20;

            double percentage = (double)currentValue / maxValue;

            int filledLength = (int)(percentage * barLength);
            int emptyLength = barLength - filledLength;

            string bar = new string('#', filledLength) + new string('-', emptyLength);

            return $"{label}: [{bar}] {currentValue}/{maxValue}";
        }
    }
}
