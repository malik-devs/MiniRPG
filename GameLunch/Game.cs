using MiniRPG.Characters;
using MiniRPG.Combat;
using MiniRPG.Enums;
using MiniRPG.Events;
using MiniRPG.Items;
using MiniRPG.Shops;


namespace MiniRPG.GameLunch
{
    public class Game
    {
        public void Start()
        {
            while (true)
            {
                Console.WriteLine("======== Welcome To MiniRPG ========\n\n");

                Console.WriteLine("Enter a name to your player....");
                string playerName = Console.ReadLine();
                Player player = new Player(playerName);
                Console.WriteLine($"Your Player name: {player.Name}\n\n");
                Shop shop = CreateShop();
                player.LevelUpEvent += OnLevelUp;

                GameResult result = MainMenu(player, shop);
                if (result != GameResult.Restart)
                    break;
            }
            
        }

        private GameResult MainMenu(Player player, Shop shop)
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

                int choice = ReadChoice(0,4);
                BattleResult result;
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("\n\n============ Good Bye ============\n\n");
                        return GameResult.Exit;
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
                        result = Battle(player);
                        if (result != BattleResult.Win)
                            return GameOver();
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
            Console.WriteLine("Enter 0 to back..");
            ReadChoice(0,0);
            
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
                int choice = ReadChoice(0,shop.Count);
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
                int choice = ReadChoice(0,player.Inventory.Count);
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

        private BattleResult Battle(Player player)
        {
            Enemy goblin = new Enemy("Goblin", 20, 10, 5, 10);
            CombatSystem combat = new CombatSystem();
            BattleResult result = combat.StartBattle(player, goblin);
            if (result == BattleResult.Win)
            {
                Console.WriteLine($"{goblin.Name} defeated\n");
            }

            return result;

        }

        private void OnLevelUp(object? sender, LevelUpEventArgs e)
        {
            Console.WriteLine($"!*!*! Level UP! Level{e.OldLevel} --> Level{e.NewLevel}  !*!*!\n");
        }

        private int ReadChoice(int min, int max)
        {
            while (true)
            {
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int choice) && choice >= min && choice <= max)
                {
                    return choice;
                }

                Console.WriteLine($"Please enter a valid number, between {min} and {max}.");
            }
        }

        private GameResult GameOver()
        {
            Console.WriteLine("============ GAME OVER ============\n");
            Console.WriteLine("1. Restart Game ");
            Console.WriteLine("0. Exit \n");

            int choice = ReadChoice(0,1);

            if(choice == 0)
                return GameResult.Exit;
            return GameResult.Restart;
        }

    }
}
