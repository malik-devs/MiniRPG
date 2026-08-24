using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Classes
{
    public class Weapon : Item
    {
        public int Damage { get; private set; }


        public Weapon(string  name, int price, string desc, int damage) : base (name, price, desc)
        {
            Damage = damage;
        }

        public override Item Clone()
        {
            return new Weapon(
            Name,
            Price,
            Description,
            Damage
        );
        }

        public override ItemUseResult Use(Player player)
        {
           EquipResult result = player.EquipWeapon( this );

            if (result == EquipResult.Success)
                return ItemUseResult.Success;

            return ItemUseResult.Failed;


        }
    }
}
