using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Classes
{
    public class Inventory
    {
        private List<Item> items = new List<Item>();

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

        public void PrintItems()
        {
            if(items.Count == 0)
            {
                Console.WriteLine("Inventory is empty..");
                return;
            }

            foreach (Item item in items)
            {
                Console.WriteLine(
                    $"Name: {item.Name}\n"+
                    $"Price: {item.Price}\n"+
                    $"Description: {item.Description}\n\n"
                );
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
