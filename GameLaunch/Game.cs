using MiniRPG.Characters;
using MiniRPG.Characters.Enemies;
using MiniRPG.Combat;
using MiniRPG.Enums;
using MiniRPG.Events;
using MiniRPG.Items;
using MiniRPG.Items.Equipments;
using MiniRPG.SaveSystem;
using MiniRPG.Shops;
using MiniRPG.UI;


namespace MiniRPG.GameLaunch
{
    public class Game
    {
        private Player? StartGame()
        {            
            SaveManager saveManager = new SaveManager();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("======== Welcome To MiniRPG ========\n\n");

                Console.WriteLine(
                    "1. New Game\n" +
                    "2. Load Game\n" +
                    "0. Exit\n"
                );
                Console.WriteLine("Enter your choice:");

                int choice = ReadChoice(0, 2);


                switch (choice)
                {
                    case 1:
                        Console.WriteLine("\nEnter a name to your player....");
                        string? playerName = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(playerName))
                        {
                            Console.WriteLine("Name cannot be empty!");
                            WaitForEnter();
                            continue;
                        }
                        Console.WriteLine($"Your Player name: {playerName}\n\n");
                        return new Player(playerName);

                    case 2:
                        Player? loadedPlayer = saveManager.Load();
                        if (loadedPlayer == null)
                        {
                            Console.WriteLine("\nNo save game found!");
                            WaitForEnter();

                            continue;
                        }
                        Console.WriteLine("\nGame loaded successfully!");
                        WaitForEnter();

                        return loadedPlayer;

                    case 0:
                        return null;

                    

                }
            }
        }

        public void Start()
        {
            
            while (true)
            {
                
                Player? player = StartGame();

                if (player == null) break;

               
                Shop shop = CreateShop();
                player.LevelUpEvent += OnLevelUp;
                EnemyFactory enemyFactory = new EnemyFactory();


                GameResult result = MainMenu(player, shop, enemyFactory);
                if (result == GameResult.Victory)
                {
                    Console.WriteLine("\n=====================================");
                    Console.WriteLine("        YOU COMPLETED THE GAME!      ");
                    Console.WriteLine("=====================================\n");
                    WaitForEnter();
                    break;
                }
                if (result == GameResult.Exit)
                    break;
            }

        }

        private GameResult MainMenu(Player player, Shop shop, EnemyFactory enemyFactory)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("================ MINI RPG ================\n\n");

                Console.WriteLine(
                    "1. Shop\n" +
                    "2. Inventory\n" +
                    "3. Player Stats\n" +
                    "4. Battle\n" +
                    "5. Save Game\n" +
                    "0. Exit" +
                    "\n\n"
                    );
                Console.WriteLine("Enter your choice: ");

                int choice = ReadChoice(0,5);
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
                        result = Battle(player, enemyFactory);
                        
                        if(result == BattleResult.Win)
                        {
                            if (player.IsMaxLevel())
                            {
                                return GameResult.Victory;
                            }
                            break;
                        }
                        return GameOver();
                    case 5:
                        SaveManager saveManager = new SaveManager();
                        saveManager.Save(player);

                        Console.WriteLine("\nGame saved successfully!");
                        WaitForEnter();
                        break;

                }
                
            }

        }

        private Shop CreateShop()
        {
            Shop shop = new Shop();
            shop.AddItem(new HealthPotion("Health Potion", 20, "Restores 50 HP", 50));
            shop.AddItem(new HealthPotion("Greater Health Potion", 40, "Restores 90 HP", 90));
            shop.AddItem(new Weapon("Iron Sword", 30, "A simple iron sword.", 10, 10));
            shop.AddItem(new Weapon("Steel Sword", 50, "A stronger sword.", 20, 20));
            shop.AddItem(new DefensePotion("Defense Potion", 25, "restores 5 Defense", 5));
            shop.AddItem(new Armor("Iron Armor", 30, "A stronger armor.", 20, 10));


            return shop;
        }

        private void PlayerStats(Player player)
        {
            Console.Clear();
            PlayerUI playerUI = new PlayerUI();
            playerUI.PrintStats(player);
            WaitForEnter();

        }

        private void PrintShop(Shop shop, Player player)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("================  SHOP  ================");
                ShopUI shopUI = new ShopUI();
                shopUI.PrintItems(shop);
                Console.WriteLine("0. Exit\n");
                Console.WriteLine($"Gold: {player.Gold}\n");
                Console.WriteLine("Enter Your Choice...");
                int choice = ReadChoice(0,shop.Count);
                if (choice == 0)
                {
                    Console.WriteLine("Thank you for shoping....");
                    WaitForEnter();
                    break;
                }
                else
                {
                    Item? item = shop.GetItem(choice);
                    if (item == null)
                    {
                        Console.WriteLine("Invalid choice.");
                        WaitForEnter();
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

                        case PurchaseResult.LevelTooLow:
                            Console.WriteLine("You need to be level 3 to buy this item!!.");
                            break;
                    }
                }
                WaitForEnter();
            }
        }

        private void PrintInventory(Player player)
        {
            int avilable = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("================ INVENTORY ================");
                InventoryUI inventoryUI = new InventoryUI();
                inventoryUI.PrintItems(player.Inventory); 
                
                if (player.EquippedWeapon != null)
                {
                    Console.WriteLine("-1. Unequip Weapon.");
                    avilable--;
                }
                if (player.EquippedArmor != null)
                {
                    Console.WriteLine("-2. Unequip Armor.");
                    avilable--;
                }
                Console.WriteLine("0. Exit\n");
                Console.WriteLine("Enter your Choice to Use: ");
                int choice = ReadChoice(avilable,player.Inventory.Count);
                if (choice == 0)
                    break;
                else if (choice == -1 && player.EquippedWeapon != null)
                {
                    player.Unequip(EquipmentType.Weapon);
                    Console.WriteLine($"Unequipped Weapon");
                }
                else if (choice == -2 && player.EquippedArmor != null)
                {
                    player.Unequip(EquipmentType.Armor);
                    Console.WriteLine($"Unequipped Armor");
                }
                else
                {
                    Item? item = player.Inventory.GetItem(choice);
                    if (item == null)
                    {
                        Console.WriteLine("Invalid choice.");
                        WaitForEnter();
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
                        case ItemUseResult.CannotUse:
                            Console.WriteLine($"{item.Name} need to be level 3 to use it.");
                            break;
                    }
                }
                WaitForEnter();
            }
        }

        private BattleResult Battle(Player player, EnemyFactory enemyFactory)
        {
            Enemy? enemy = enemyFactory.GetEnemy(player.Level);

            if (enemy == null)
                return BattleResult.NoEnemy;

            CombatSystem combatSystem = new CombatSystem();
            CombatUI combatUI = new CombatUI(combatSystem);

            BattleResult result = combatUI.StartBattle(player, enemy);


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

        public void WaitForEnter()
        {
            Console.WriteLine("\nPress ENTER to continue...");
            Console.ReadLine();
        }

        


        

    }
}
