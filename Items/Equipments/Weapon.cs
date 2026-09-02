using MiniRPG.Characters;
using MiniRPG.Enums;


namespace MiniRPG.Items.Equipments
{
    public class Weapon : Equipment
    {
        public int Damage { get; private set; }

        public override EquipmentType EquipmentType => EquipmentType.Weapon;

        public Weapon(string name, int price, string desc, int damage, int maxDurability) : base(name, price, desc, maxDurability)
        {
            Damage = damage;

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

    

    }
}
