using MiniRPG.Events;
using MiniRPG.Inventories;
using MiniRPG.UI;
using MiniRPG.SaveSystem;
using MiniRPG.Combat;
using MiniRPG.Enums;
using MiniRPG.Items.Equipments;

namespace MiniRPG.Characters
{

    public class Player : Character
    {
        //Propreties
        public int Gold { get; private set; }
        public int Level { get; private set; }
        public int XP { get; private set; }
        public int XpMax { get; private set; }
        public Weapon? EquippedWeapon { get; private set; }
        public Armor? EquippedArmor { get; private set; }
        public int TotalDamage
        {
            get
            {
                if (EquippedWeapon == null) return Damage;

                return Damage + EquippedWeapon.Damage;
            }
        }
        public Inventory Inventory { get; private set; } = new Inventory();
        public event EventHandler<LevelUpEventArgs> LevelUpEvent;

        //Methods
        public Player(string name) : base(name, 100, 15, 0)
        {
            Gold = 100;
            Level = 1;
            XP = 0;
            XpMax = 100;
        }

        public void Heal()
        {
            HP = MaxHP;
        }

        public void PrintStats()
        {
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║           PLAYER STATS             ║");
            Console.WriteLine("╚════════════════════════════════════╝\n");
            Console.WriteLine($"Name: {Name}\n");
            Console.WriteLine($"{ConsoleUI.PrintBar("HP", HP, MaxHP)}\n");
            if (MaxDefense > 0)
            {
                Console.WriteLine(
                    $"{ConsoleUI.PrintBar("Defense", Defense, MaxDefense)}\n");
            }
            Console.WriteLine($"Gold: {Gold}\n");
            Console.WriteLine($"Level: {Level}\n");
            Console.WriteLine($"{ConsoleUI.PrintBar("XP", XP, XpMax)}\n");
            Console.WriteLine("======================================\n");
            Console.WriteLine($"Base Damage: {Damage}\n");
            

            Console.Write("Weapon: ");

            if (EquippedWeapon != null)
            {
                Console.WriteLine($"{EquippedWeapon.Name}\n");
                Console.WriteLine($"Weapon Damage: +{EquippedWeapon.Damage}\n");
                Console.WriteLine(
                    $"{ConsoleUI.PrintBar(" Durability", EquippedWeapon.Durability, EquippedWeapon.MaxDurability)}\n"
                );
                Console.WriteLine($"Total Damage: {TotalDamage}\n");

            }
            else
            {
                Console.WriteLine("No Equipped Weapon");
            }
                Console.WriteLine("==============================\n");

            if (EquippedArmor != null)
            {
                Console.WriteLine($"{EquippedArmor.Name}\n");
                Console.WriteLine($"Armor Damage Reduction Percentage: %{EquippedArmor.DamageReductionPercentage}\n");
                Console.WriteLine(
                    $"{ConsoleUI.PrintBar(" Durability", EquippedArmor.Durability, EquippedArmor.MaxDurability)}\n"
                );
            }
            else
            {
                Console.WriteLine("No Equipped Armor");
            }

            Console.WriteLine("==============================");
        }

        public void ReceiveReward(int xpReward, int goldReward)
        {
            Gold += goldReward;
            XP += xpReward;
            LevelUp();

        }

        public void LevelUp()
        {
            while (XP >= XpMax)
            {
                XP -= XpMax;
                Level++;
                XpMax += 50;

                if (Level % 2 == 0)
                    MaxHP += 50;

                if (Level == 3)
                {
                    MaxDefense = 50;
                }
                else if (Level > 3)
                {
                    MaxDefense += 25;
                }


                Heal();
                RestoreFullDefense();
                LevelUpEvent?.Invoke(this, new LevelUpEventArgs(Level, Level - 1));
            }
        }

        public bool IsMaxLevel()
        {
            return Level > 10;
        }

        public bool SpendGold(int amount)
        {
            if (amount < 0) throw new ArgumentException("Amount cannot be negative");
            if (Gold >= amount)
            {
                Gold -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override DamageResult Attack(Character target)
        {
            int damage = TotalDamage;
            DamageResult result = target.TakeDamage(damage);

            if (EquippedWeapon != null)
            {
                EquippedWeapon.UseDurability();
                if (EquippedWeapon.IsBroken)
                {
                    Console.WriteLine($"\nYour {EquippedWeapon.Name} broke!");
                    BreakWeapon();
                }
            }
            return result;
        }

        public override DamageResult TakeDamage(int damage)
        {
            if (damage < 0)
            {
                throw new ArgumentException("Damage cannot be negative.");
            }

            int armorAbsorbedDamage = 0;
            int remainingDamage = damage;

            if (EquippedArmor != null && !EquippedArmor.IsBroken)
            {
                armorAbsorbedDamage =
                    (int)(damage * EquippedArmor.DamageReductionPercentage / 100.0);//معادلة حساب ضرر الدروع

                remainingDamage = damage - armorAbsorbedDamage;

                EquippedArmor.UseDurability();

                if (EquippedArmor.IsBroken)
                {
                    Console.WriteLine(
                        $"\nYour {EquippedArmor.Name} broke!");

                    EquippedArmor = null;
                }
            }
            DamageResult baseResult =
            base.TakeDamage(remainingDamage);

            return new DamageResult(
                damage,
                armorAbsorbedDamage,
                baseResult.DefenseDamage,
                baseResult.HPDamage
            );
        }

        private void BreakWeapon()
        {
            EquippedWeapon = null;
        }

        //دالة لاسترجاع البيانات المحفوظة
        public void RestoreState(PlayerSaveData data)
        {
            HP = data.HP;
            MaxHP = data.MaxHP;
            Gold = data.Gold;
            Level = data.Level;
            XP = data.XP;
            XpMax = data.XpMax;
            Defense = data.Defense;
            MaxDefense = data.MaxDefense;


        }

        public EquipmentResult Equip(Equipment equipment)
        {
            if (equipment == null)
                throw new ArgumentNullException(nameof(equipment));
            if (!Inventory.Contains(equipment))
                return EquipmentResult.NotFound;

            switch (equipment.EquipmentType)
            {
                case EquipmentType.Weapon:

                    if (EquippedWeapon != null)
                        Inventory.AddItem(EquippedWeapon);

                    EquippedWeapon = (Weapon)equipment;
                    break;

                case EquipmentType.Armor:

                    if (EquippedArmor != null)
                        Inventory.AddItem(EquippedArmor);

                    EquippedArmor = (Armor)equipment;
                    break;
            }

            return EquipmentResult.Success;

        }
        public EquipmentResult RestoreEquippedEquipment(Equipment equipment)
        {
            if (equipment == null)
                throw new ArgumentNullException(nameof(equipment));

            switch (equipment.EquipmentType)
            {
                case EquipmentType.Weapon:
                    EquippedWeapon = (Weapon)equipment;
                    break;

                case EquipmentType.Armor:
                    EquippedArmor = (Armor)equipment;
                    break;
            }

            return EquipmentResult.Success;
        }
        public EquipmentResult Unequip(EquipmentType equipmentType)
        {
            switch (equipmentType)
            {
                case EquipmentType.Weapon:

                    if (EquippedWeapon == null)
                        return EquipmentResult.NotEquipped;

                    Inventory.AddItem(EquippedWeapon);
                    EquippedWeapon = null;

                    break;

                case EquipmentType.Armor:

                    if (EquippedArmor == null)
                        return EquipmentResult.NotEquipped;

                    Inventory.AddItem(EquippedArmor);
                    EquippedArmor = null;

                    break;
            }

            return EquipmentResult.Success;
        }


    }

}
