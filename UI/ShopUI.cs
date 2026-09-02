using MiniRPG.Items;
using MiniRPG.Items.Equipments;
using MiniRPG.Shops;

namespace MiniRPG.UI
{
    public class ShopUI
    {
        public void PrintItems(Shop shop)
        {
            if (shop.Count == 0)
            {
                Console.WriteLine("Shop is empty..");
                return;
            }
            int count = 1;

            while (count <= shop.Count) 
            {
                Item? item = shop.GetItem(count);

                Console.WriteLine(
                    $"{count}. {item.Name}\n" +
                    $"Price: {item.Price}\n" +
                    $"Description: {item.Description}"
                );

                if (item is Weapon weapon)
                {
                    Console.WriteLine($"Damage:  +{weapon.Damage}");
                    Console.WriteLine($"Durability: +{weapon.MaxDurability}");
                }
                if (item is Armor armor)
                {
                    Console.WriteLine(
                        $"Damage Reduction: %{armor.DamageReductionPercentage}");

                    Console.WriteLine(
                        $"Durability: +{armor.MaxDurability}");
                }
                Console.WriteLine("\n");

                count++;
            }
        }
    }
}
