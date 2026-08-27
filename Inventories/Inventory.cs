using MiniRPG.Characters;
using MiniRPG.Enums;
using MiniRPG.Items;
using MiniRPG.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Inventories
{
    public class Inventory
    {
        private List<Item> items = new List<Item>();
        public int Count => items.Count;
        public void AddItem(Item item)
        {
            if(item !=  null)
                items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            if (item != null)
                items.Remove(item);
        }

        public bool Contains(Item item)
        {
            return items.Contains(item);
        }

        

        public Item? FindItem(string name)
        {
            foreach (Item item in items)
            {
                if (item.Name == name)
                    return item;
            }
            return null;
        }
        public Item? GetItem(int index)
        {
            if (index <= 0 || index > items.Count) return null;
            return items[index - 1];
        }

        public void PrintItems()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("Inventory is empty..");
                return;
            }
            int count = 1;
            foreach (Item item in items)
            {
                Console.WriteLine(
                    $"{count}. Name: {item.Name}\n"+
                    $"   Description: {item.Description}\n"
                );

                if (item is Weapon weapon)
                {
                    Console.WriteLine($"   Damage: +{weapon.Damage}");
                    Console.WriteLine(
                   $"{ConsoleUI.PrintBar(" Durability", weapon.Durability, weapon.MaxDurability)}"
               );
                }
                Console.WriteLine("\n");
                count++;
            }
        }

        public ItemUseResult UseItem(Item item, Player player)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (!items.Contains(item))
                return ItemUseResult.Failed;

            ItemUseResult result = item.Use(player);

            if (result == ItemUseResult.Success)
                items.Remove(item);

            return result;

        }
    }
}
