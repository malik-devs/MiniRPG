using MiniRPG.Characters;
using MiniRPG.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Items
{
    public abstract class Item
    { 
        public string Name { get; private set; }
        public int Price { get; private set; }
        public string Description { get; private set; }
        public virtual bool IsStackable => false;

        public Item(string  name, int price, string description)
        {
            Name = name;
            Price = price;
            Description = description;
        }

        

        public abstract ItemUseResult Use(Player player);

        public abstract Item Clone();

    }
}
