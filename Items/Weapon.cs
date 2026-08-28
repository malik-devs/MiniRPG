using MiniRPG.Characters;
using MiniRPG.Enums;
using MiniRPG.Equipment;


namespace MiniRPG.Items
{
    public class Weapon : Item
    {
        public int Damage { get; private set; }

        public int MaxDurability {  get; private set; }
        public int Durability { get; private set; }


        public Weapon(string  name, int price, string desc, int damage, int maxDurability) : base (name, price, desc)
        {
            Damage = damage;
            Durability = maxDurability;
            MaxDurability = maxDurability;

        }

        public void UseDurability()
        {
            if(Durability > 0)
                Durability--;
        }

        public bool IsBroken
        {
            get
            {
                return Durability <= 0;
            }
        }

        public override Item Clone()
        {
            return new Weapon(
            Name,
            Price,
            Description,
            Damage,
            MaxDurability
        );
        }

        public override ItemUseResult Use(Player player)
        {
           EquipmentResult result = player.EquipWeapon( this );

            if (result == EquipmentResult.Success)
                return ItemUseResult.Success;

            return ItemUseResult.Failed;


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

    }
}
