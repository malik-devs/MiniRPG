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
            player.LevelUpEvent += OnLevelUp;

            Console.WriteLine("==========Player==========");
            player.PrintStats();

            Enemy goblin = new Enemy("goblin", 10, 25, 270, 100);

            Console.WriteLine("==========Battle==========");
            CombatSystem combat = new CombatSystem();
            combat.StartBattle(player, goblin);

            HealthPotion potion = new HealthPotion("Health Potion", 20, "Restores 30 HP", 30);

            player.Inventory.AddItem(potion);
            player.Inventory.AddItem(potion);


            player.Inventory.PrintItems();

            Console.WriteLine("\n=== TEST POTION 1 ===");

            player.TakeDamage(60);

            Console.WriteLine($"HP before potion: {player.HP}");

            player.Inventory.UseItem(potion, player);

            Console.WriteLine($"HP after potion: {player.HP}");

            Console.WriteLine("\n=== INVENTORY AFTER USE ===");

            player.Inventory.PrintItems();

            Console.WriteLine("\n=== FINAL PLAYER ===");

            player.PrintStats();

            




        }
    }
}
