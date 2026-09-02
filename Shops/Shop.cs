using MiniRPG.Characters;
using MiniRPG.Enums;
using MiniRPG.Items;

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

        public Item? GetItem(int index)
        {
            if (index <= 0 || index > items.Count)
                return null;

            return items[index - 1];
        }

        public PurchaseResult BuyItem(Player player, Item item)
        {
            if (!items.Contains(item))
                return PurchaseResult.ItemNotFound;

            if (item is DefensePotion && player.Level < 3)
                return PurchaseResult.LevelTooLow;

            if (!player.SpendGold(item.Price))
                return PurchaseResult.NotEnoughGold;

            player.Inventory.AddItem(item.Clone());

            return PurchaseResult.Success;
        }
    }
}