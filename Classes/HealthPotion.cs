using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Classes
{
    public class HealthPotion : Item
    {
        public int HealAmount { get; private set; }
        public HealthPotion(string name, int price, string desc, int healAmount):base(name, price, desc)
        {
            HealAmount = healAmount;
        }

        public override void Use(Player player)
        {
            player.Heal(HealAmount);
        }
    }
}
