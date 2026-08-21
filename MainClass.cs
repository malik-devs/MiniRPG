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
        static void OnLevelUp(object? sender, LevelUpEventArgs e)
        {
            Console.WriteLine($"!*!*! Level UP! Level{e.OldLevel} --> Level{e.NewLevel}  !*!*!\n");
        }

        public static void Main(string[] args)
        {
           Player player = new Player("Hero");

            Item potion = new Item("Health Potion",20, "Restores the player's HP.");

            player.inventory.AddItem(potion);
            player.inventory.PrintItems();

            //player.PrintStats();

            //player.LevelUpEvent += OnLevelUp;

            //Enemy goblin = new Enemy("Goblin", 40, 10, 270, 15);

            //CombatSystem combat = new CombatSystem();
            //combat.StartBattle(player, goblin);

            
        }
    }
}
