using MiniRPG.Characters;

namespace MiniRPG.UI
{
    public class PlayerUI
    {
        public void PrintStats(Player player)
        {
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║           PLAYER STATS             ║");
            Console.WriteLine("╚════════════════════════════════════╝\n");
            Console.WriteLine($"Name: {player.Name}\n");
            Console.WriteLine($"{ConsoleUI.PrintBar("HP", player.HP, player.MaxHP)}\n");
            if (player.MaxDefense > 0)
            {
                Console.WriteLine(
                    $"{ConsoleUI.PrintBar("Defense", player.Defense, player.MaxDefense)}\n");
            }
            Console.WriteLine($"Gold: {player.Gold}\n");
            Console.WriteLine($"Level: {player.Level}\n");
            Console.WriteLine($"{ConsoleUI.PrintBar("XP", player.XP, player.XpMax)}\n");
            Console.WriteLine("======================================\n");
            Console.WriteLine($"Base Damage: {player.Damage}\n");


            Console.Write("Weapon: ");

            if (player.EquippedWeapon != null)
            {
                Console.WriteLine($"{player.EquippedWeapon.Name}\n");
                Console.WriteLine($"Weapon Damage: +{player.EquippedWeapon.Damage}\n");
                Console.WriteLine(
                    $"{ConsoleUI.PrintBar(" Durability", player.EquippedWeapon.Durability, player.EquippedWeapon.MaxDurability)}\n"
                );
                Console.WriteLine($"Total Damage: {player.TotalDamage}\n");

            }
            else
            {
                Console.WriteLine("No Equipped Weapon");
            }
            Console.WriteLine("==============================\n");

            if (player.EquippedArmor != null)
            {
                Console.WriteLine($"{player.EquippedArmor.Name}\n");
                Console.WriteLine($"Armor Damage Reduction Percentage: %{player.EquippedArmor.DamageReductionPercentage}\n");
                Console.WriteLine(
                    $"{ConsoleUI.PrintBar(" Durability", player.EquippedArmor.Durability, player.EquippedArmor.MaxDurability)}\n"
                );
            }
            else
            {
                Console.WriteLine("No Equipped Armor");
            }

            Console.WriteLine("==============================");
        }

    }
}
