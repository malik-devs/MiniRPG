using MiniRPG.Characters;
using MiniRPG.Enums;
using MiniRPG.Items;
using MiniRPG.Items.Equipments;

namespace MiniRPG.Inventories
{
    public class Inventory
    {
        private List<InventoryItem> items = new List<InventoryItem>();
        public int Capacity { get; private set; }
        public bool IsFull => Count >= Capacity;

        public IReadOnlyList<InventoryItem> Items => items;

        public int Count => items.Count;

        public Inventory(int capacity = 20)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));

            Capacity = capacity;
        }

        public InventoryResult AddItem(Item item)
        {
           return AddItem(item, 1);
        }

        public InventoryResult AddItem(Item item, int quantity)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (quantity <= 0)
                throw new ArgumentException(
                    "Quantity must be greater than zero.",
                    nameof(quantity));

            // محاولة الـ Stack أولًا
            if (item.IsStackable)
            {
                InventoryItem? existingItem =
                    items.FirstOrDefault(x =>
                        x.Item.IsStackable &&
                        x.Item.Name == item.Name);

                if (existingItem != null)
                {
                    existingItem.AddQuantity(quantity);
                    return InventoryResult.Success;
                }
            }

            // يحتاج Slot جديد
            if (IsFull)
                return InventoryResult.Full;

            items.Add(new InventoryItem(item, quantity));

            return InventoryResult.Success;
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

            if (result == ItemUseResult.Success && item is not Equipment) 
            {
                RemoveItem(item);
            }

            return result;
        }
        public bool CanAdd(Item item)
        {
            if (item == null)
                return false;

            // إذا كان Stackable ويوجد Stack له، يمكن إضافته
            if (item.IsStackable)
            {
                bool hasExistingStack = items.Any(x =>
                    x.Item.IsStackable &&
                    x.Item.Name == item.Name);

                if (hasExistingStack)
                    return true;
            }

            // يحتاج Slot جديد
            return !IsFull;
        }

    }
}