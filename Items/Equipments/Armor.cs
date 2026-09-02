using MiniRPG.Characters;
using MiniRPG.Enums;

namespace MiniRPG.Items.Equipments
{
    public class Armor : Equipment
    {
        public int DamageReductionPercentage { get; private set; }

        public override EquipmentType EquipmentType => EquipmentType.Armor;

        public Armor(string name, int price, string description, int maxdurabilty, int damageReductionPercentage) : base(name, price, description, maxdurabilty)
        {
            if(damageReductionPercentage < 0 || damageReductionPercentage > 100)
            {
                throw new ArgumentException(nameof(damageReductionPercentage), "Damage reduction percentage must be between 0 and 100.");
            }
            DamageReductionPercentage = damageReductionPercentage;
            
        }

        public override Item Clone()
        {
            return new Armor(
            Name,
            Price,
            Description,
            MaxDurability,
            DamageReductionPercentage
                );
        }

 
    }
}
