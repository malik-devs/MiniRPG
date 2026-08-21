using MiniRPG.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
           Player player = new Player("Hero");
            player.PrintStats();
            Enemy goblin = new Enemy("Goblin", 40, 10, 0, 0);
            player.Attack(goblin);
            Console.WriteLine(goblin.HP);
            goblin.Attack(player);
            Console.WriteLine(player.HP);
        }
    }
}
