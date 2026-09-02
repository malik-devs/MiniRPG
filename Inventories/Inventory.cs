using MiniRPG.Characters;
using MiniRPG.Enums;
using MiniRPG.Items;

namespace MiniRPG.Inventories
{
    public class Inventory
    {
        private List<Item> items = new List<Item>();

        public IReadOnlyList<Item> Items => items;

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
            if (index <= 0 || index > items.Count)
                return null;

            return items[index - 1];
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