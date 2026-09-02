using MiniRPG.Inventories;
using MiniRPG.Items;
using MiniRPG.Items.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.UI
{
    public class InventoryUI
    {
        public void PrintItems(Inventory inventory)
        {
            if (inventory.Count == 0)
            {
                Console.WriteLine("Inventory is empty..");
                return;
            }
            int count = 1;
            foreach (Item item in inventory.Items)
            {
                Console.WriteLine(
                    $"{count}. Name: {item.Name}\n" +
                    $"   Description: {item.Description}\n"
                );

                if (item is Weapon weapon)
                {
                    Console.WriteLine($"   Damage: +{weapon.Damage}");
                    Console.WriteLine(
                   $"{ConsoleUI.PrintBar(" Durability", weapon.Durability, weapon.MaxDurability)}"
                    );
                }
                if (item is Armor armor)
                {
                    Console.WriteLine($"   Damage Reduction Percentage: %{armor.DamageReductionPercentage}");
                    Console.WriteLine(
                   $"{ConsoleUI.PrintBar(" Durability", armor.Durability, armor.MaxDurability)}"
                    );
                }
                Console.WriteLine("\n");
                count++;
            }
        }
    }
}
