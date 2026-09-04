using MiniRPG.Characters;
using MiniRPG.Enums;


namespace MiniRPG.Items
{
    public class HealthPotion : Item
    {
        public int HealAmount { get; private set; }
        public override bool IsStackable => true;
        public HealthPotion(string name, int price, string desc, int healAmount):base(name, price, desc)
        {
            HealAmount = healAmount;
        }

        public override ItemUseResult Use(Player player)
        {
            player.Heal(HealAmount);
            return ItemUseResult.Success;
        }

        public override Item Clone()
        {
            return new HealthPotion(Name,Price,Description,HealAmount);
        }
    }
}
