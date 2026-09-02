using MiniRPG.Characters;
using MiniRPG.Enums;
using MiniRPG.Items;

namespace MiniRPG.Inventories
{
    public class Inventory
    {
        private List<InventoryItem> items = new List<InventoryItem>();

        public IReadOnlyList<InventoryItem> Items => items;

        public int Count => items.Count;

        public void AddItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (item.IsStackable)
            {
                InventoryItem? existingItem =
                    items.FirstOrDefault(x => x.Item.Name == item.Name);

                if (existingItem != null)
                {
                    existingItem.AddQuantity(1);
                    return;
                }
            }

            items.Add(new InventoryItem(item));
        }

        public bool RemoveItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            InventoryItem? inventoryItem =
                items.FirstOrDefault(x => x.Item == item);

            if (inventoryItem == null)
                return false;

            if (inventoryItem.Quantity > 1)
            {
                inventoryItem.RemoveQuantity(1);
            }
            else
            {
                items.Remove(inventoryItem);
            }

            return true;
        }

        public bool Contains(Item item)
        {
            if (item == null)
                return false;

            return items.Any(x => x.Item == item);
        }

        public Item? FindItem(string name)
        {
            InventoryItem? inventoryItem =
                items.FirstOrDefault(x => x.Item.Name == name);

            return inventoryItem?.Item;
        }

        public Item? GetItem(int index)
        {
            if (index <= 0 || index > items.Count)
                return null;

            return items[index - 1].Item;
        }

        public ItemUseResult UseItem(Item item, Player player)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (player == null)
                throw new ArgumentNullException(nameof(player));

            InventoryItem? inventoryItem =
                items.FirstOrDefault(x => x.Item == item);

            if (inventoryItem == null)
                return ItemUseResult.Failed;

            ItemUseResult result = item.Use(player);

            if (result == ItemUseResult.Success)
            {
                RemoveItem(item);
            }

            return result;
        }
        public void AddItem(Item item, int quantity)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (quantity <= 0)
                throw new ArgumentException(
                    "Quantity must be greater than zero.",
                    nameof(quantity));

            if (item.IsStackable)
            {
                InventoryItem? existingItem =
                    items.FirstOrDefault(x => x.Item.Name == item.Name);

                if (existingItem != null)
                {
                    existingItem.AddQuantity(quantity);
                    return;
                }
            }

            items.Add(new InventoryItem(item, quantity));
        }
    }
}