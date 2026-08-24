using MiniRPG.Characters;
using MiniRPG.Combat;
using MiniRPG.Enums;
using MiniRPG.Items;
using MiniRPG.Shops;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MiniRPG.GameLunch
{
    public class Game
    {
        public void Start()
        {
            Console.WriteLine("======== Welcome To MiniRPG ========\n\n");

            Console.WriteLine("Enter a name to your player....");
            string namePlayer = Console.ReadLine();
            Player player = new Player(namePlayer);
            Console.WriteLine($"Your Player name: {player.Name}\n\n");
            Shop shop = CreateShop();

            MainMenu(player, shop);


        }

        private void MainMenu(Player player, Shop shop)
        {
            while (true)
            {
                Console.WriteLine("\n\n================ MINI RPG ================\n\n");

                Console.WriteLine(
                    "1.Shop\n" +
                    "2.Inventory\n" +
                    "3.Player Stats\n" +
                    "4.Battle\n" +
                    "0.Exit" +
                    "\n\n"
                    );
                Console.WriteLine("Enter your choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("\n\n============ Good Bye ============\n\n");
                        return;
                    case 1:
                        PrintShop(shop, player);
                        break;
                    case 2:
                        PrintInventory(player);
                        break;
                    case 3:
                        PlayerStats(player);
                        break;
                    case 4:
                        Battle(player);
                        break;

                }


            }

        }

        private Shop CreateShop()
        {
            Shop shop = new Shop();
            shop.AddItem(new HealthPotion("Health Potion", 20, "Restores 30 HP", 30));
            shop.AddItem(new HealthPotion("Greater Health Potion", 40, "Restores 60 HP", 60));
            shop.AddItem(new Weapon("Iron Sword", 50, "A simple iron sword.", 10));
            shop.AddItem(new Weapon("Steel Sword", 80, "A stronger sword.", 20));

            return shop;
        }

        private void PlayerStats(Player player)
        {
            player.PrintStats();
            int choice;
            do
            {
                Console.WriteLine("Enter 0 to back..");
                choice = Convert.ToInt32(Console.ReadLine());
            } while (choice != 0);

        }

        private void PrintShop(Shop shop, Player player)
        {
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
                else
                {
                    Item? item = shop.GetItem(choice);
                    if (item == null)
                    {
                        Console.WriteLine("Invalid choice.");
                        continue;
                    }
                    PurchaseResult result = shop.BuyItem(player, item);
                    switch (result)
                    {
                        case PurchaseResult.Success:
                            Console.WriteLine($"You bought {item.Name}");
                            break;

                        case PurchaseResult.ItemNotFound:
                            Console.WriteLine("Item is not available in the shop.");
                            break;

                        case PurchaseResult.NotEnoughGold:
                            Console.WriteLine("Not enough Gold.");
                            break;
                    }
                }
            }
        }

        private void PrintInventory(Player player)
        {
            while (true)
            {
                Console.WriteLine("================ INVENTORY ================");
                player.Inventory.PrintItems();
                Console.WriteLine("0. Exit\n");
                Console.WriteLine("Enter your Choice to Use: ");
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 0)
                    break;
                else
                {
                    Item? item = player.Inventory.GetItem(choice);
                    if (item == null)
                    {
                        Console.WriteLine("Invalid choice.");
                        continue;
                    }
                    ItemUseResult result = player.Inventory.UseItem(item, player);
                    switch (result)
                    {
                        case ItemUseResult.Success:
                            Console.WriteLine($"{item.Name} used seccessfully");
                            break;
                        case ItemUseResult.Failed:
                            Console.WriteLine($"{item.Name} Faild to use");
                            break;
                    }
                }
            }
        }

        private void Battle(Player player)
        {
            Enemy Goblin = new Enemy("Goblin", 20, 5, 5, 10);
            CombatSystem combat = new CombatSystem();
            BattleResult result = combat.StartBattle(player, Goblin);
            switch (result)
            {
                case BattleResult.Win:
                    Console.WriteLine($"{Goblin.Name} defeated\n");
                    break;
                case BattleResult.Dead:
                    Console.WriteLine("YOU DEAD");
                    break;
            }


        }
    }
}
