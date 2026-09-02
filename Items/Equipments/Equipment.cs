using MiniRPG.Characters;
using MiniRPG.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Items.Equipments
{
    public abstract class Equipment : Item
    {
        public int MaxDurability { get; private set; }
        public int Durability { get; private set; }

        public abstract EquipmentType EquipmentType { get; }

        protected Equipment(string name, int price, string description, int maxdurabilty) : base(name, price, description)
        {
            Durability = maxdurabilty;
            MaxDurability = maxdurabilty;
        }

        

        public void UseDurability()
        {
            if (Durability > 0)
                Durability--;
        }

        public bool IsBroken
        {
            get
            {
                return Durability <= 0;
            }
        }

        public void RestoreDurability(int durability)
        {
            if (Durability < 0)
                Durability = 0;
            else if (Durability > MaxDurability)
                Durability = MaxDurability;
            else
                Durability = durability;
        }

        public override ItemUseResult Use(Player player)
        {
            EquipmentResult result = player.Equip(this);

            return result == EquipmentResult.Success
                ? ItemUseResult.Success
                : ItemUseResult.Failed;
        }
    }
}
