using MiniRPG.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Inventories
{
    public class InventoryItem
    {
        public Item Item { get; }
        public int Quantity { get; private set; }

        public InventoryItem(Item item, int quantity = 1)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));

            if (quantity <= 0)
                throw new ArgumentException(
                    "Quantity must be greater than zero.",
                    nameof(quantity));

            Item = item;
            Quantity = quantity;
        }

        public void AddQuantity(int amount)
        {
            if (amount <= 0)
                throw new ArgumentException(
                    "Amount must be greater than zero.",
                    nameof(amount));

            Quantity += amount;
        }

        public bool RemoveQuantity(int amount)
        {
            if (amount <= 0)
                return false;

            if (amount > Quantity)
                return false;

            Quantity -= amount;
            return true;
        }
    }
}
