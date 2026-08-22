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

        public void PrintItems()
        {
            if(items.Count < 0)
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

        public void UseItem(Item item, Player player)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            if (items.Contains(item))
            {
                item.Use(player);
                items.Remove(item);
            }
        }
    }
}
