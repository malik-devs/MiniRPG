using MiniRPG.Characters;
using MiniRPG.Enums;
using MiniRPG.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Shops
{
    public class Shop 
    {
        private List<Item> items = new List<Item>();
        public int Count => items.Count;


        public void AddItem(Item item)
        {
            if (item != null)
                items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            if (item != null)
                items.Remove(item);
        }



        public void PrintItems()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("Shop is empty..");
                return;
            }
            int count = 1;
            
            foreach (Item item in items)
            {
                Console.WriteLine(
                    $"{count}. {item.Name}\n" +
                    $"Price: {item.Price}\n" +
                    $"Description: {item.Description}"
                );

                if(item is Weapon weapon)
                {
                    Console.WriteLine($"Damage:  +{weapon.Damage}");
                    Console.WriteLine($"Durability: +{weapon.MaxDurability}");
                }
                Console.WriteLine("\n");

                count++;
            }
        }
        
        public Item? GetItem(int index)
        {
            if(index <= 0 || index > items.Count) return null;
            return items[index-1];
        }

        public PurchaseResult BuyItem(Player player, Item item)
        {
            if (!items.Contains(item))
            {
                return PurchaseResult.ItemNotFound;
            }
            if (!player.SpendGold(item.Price))
            {
                return PurchaseResult.NotEnoughGold;
            }
            player.Inventory.AddItem(item.Clone());
            return PurchaseResult.Success;
        }
    }
}
