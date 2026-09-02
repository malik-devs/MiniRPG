using MiniRPG.Characters;
using MiniRPG.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Items
{
    public class DefensePotion : Item
    {
        public int DefenseAmount { get; private set; }
        public override bool IsStackable => true;
        public DefensePotion(string name, int price, string description, int defenseAmount) : base(name, price, description)
        {
            DefenseAmount = defenseAmount;
        }

        public override Item Clone()
        {
            return new DefensePotion(Name, Price, Description, DefenseAmount);
        }

        public override ItemUseResult Use(Player player)
        {
            if (player.MaxDefense <= 0)
            {
                return ItemUseResult.CannotUse;
            }
            player.RestoreDefense(DefenseAmount);
            return ItemUseResult.Success;
        }
    }
}
