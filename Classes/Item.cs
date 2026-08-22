using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Classes
{
    public abstract class Item
    { 
        public string Name { get; private set; }
        public int Price { get; private set; }
        public string Description { get; private set; }

        public Item(string  name, int price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }

        public abstract void Use(Player player);

    }
}
