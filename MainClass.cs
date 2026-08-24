using MiniRPG.Characters;
using MiniRPG.Enums;
using MiniRPG.Events;
using MiniRPG.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniRPG.Shops;
using MiniRPG.GameLunch;

namespace MiniRPG
{
    public class MainClass
    {
        static void OnLevelUp(object? sender, LevelUpEventArgs e)
        {
            Console.WriteLine($"!*!*! Level UP! Level{e.OldLevel} --> Level{e.NewLevel}  !*!*!\n");
        }

        public static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}
