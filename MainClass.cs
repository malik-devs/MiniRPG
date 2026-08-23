using MiniRPG.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            Console.WriteLine("before");
            player.Inventory.PrintItems();
            player.PrintStats();

            Shop shop = new Shop();
            shop.AddItem(new HealthPotion("Health Potion", 20, "Restores 30 HP", 30));
            shop.AddItem(new HealthPotion("Greater Health Potion", 40, "Restores 60 HP", 60));

            while (true)
            {
                Console.WriteLine("================  SHOP  ================");
                shop.PrintItems();
                Console.WriteLine("0. Exit\n");
                Console.WriteLine($"Gold: {player.Gold}\n");
                Console.WriteLine("Enter Your Choice...");
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 0)
                {
                    Console.WriteLine("Thank you for shoping....");
                    break;
                }
                else if (choice < 0 || choice > shop.Count)
                {
                    Console.WriteLine("Invalid choice");
                }
                else
                {
                    Item? item = shop.GetItem(choice);
                    if (item == null)
                    {
                        Console.WriteLine("Invalid choice.");
                        continue;
                    }
                    bool bought = shop.BuyItem(player, item);
                    if (bought)
                    {
                        Console.WriteLine($"You bought {item.Name}!\n");
                    }
                }
            }



                Console.WriteLine("After");
            player.Inventory.PrintItems();
            player.PrintStats();
           
        }
    }
}
