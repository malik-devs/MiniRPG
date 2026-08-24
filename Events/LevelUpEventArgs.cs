using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Events
{
    public class LevelUpEventArgs : EventArgs
    {
        public int NewLevel { get; }
        public int OldLevel { get; }
        public LevelUpEventArgs(int newLevel, int oldLevel)
        {
            NewLevel = newLevel;
            OldLevel = oldLevel;
        }
    }
}
