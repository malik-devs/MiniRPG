using MiniRPG.Equipment;
using MiniRPG.Events;
using MiniRPG.Items;
using MiniRPG.Inventories;
using MiniRPG.UI;
using MiniRPG.SaveSystem;
using MiniRPG.Combat;

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
        public Player(string name):base(name,100,15,0)
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
            Console.WriteLine($"{ConsoleUI.PrintBar("HP",HP,MaxHP)}\n");
            if (MaxDefense > 0)
            {
                Console.WriteLine(
                    $"{ConsoleUI.PrintBar("Defense", Defense, MaxDefense)}\n");
            }
            Console.WriteLine($"Gold: {Gold}\n");
            Console.WriteLine($"Level: {Level}\n");
            Console.WriteLine($"{ConsoleUI.PrintBar("XP", XP, XpMax)}\n");
            Console.WriteLine($"Base Damage: {Damage}\n");
            Console.WriteLine($"Total Damage: {TotalDamage}\n");

            Console.Write("Weapon: ");

            if (EquippedWeapon != null)
            {
                Console.WriteLine($"{EquippedWeapon.Name}\n");
                Console.WriteLine($"Weapon Damage: +{EquippedWeapon.Damage}\n");
                Console.WriteLine(
                    $"{ConsoleUI.PrintBar(" Durability", EquippedWeapon.Durability, EquippedWeapon.MaxDurability)}\n"
                );
            }
            else
            {
                Console.WriteLine("No Equipped Weapon");
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
            while ( XP >= XpMax )
            {
                XP -= XpMax;
                Level++;
                XpMax += 50;

                if (Level % 2 == 0)
                    MaxHP += 50;

                if(Level == 3)
                {
                    MaxDefense = 50;
                }
                else if(Level > 3)
                {
                    MaxDefense += 25;
                }
                    
                
                Heal();
                RestoreFullDefense();
                LevelUpEvent?.Invoke(this, new LevelUpEventArgs(Level,Level-1));
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

        private void BreakWeapon()
        {
            EquippedWeapon = null;
        }

        public EquipmentResult EquipWeapon(Weapon weapon)
        {
            if(weapon == null) 
                throw new ArgumentNullException(nameof(weapon));

            if (!Inventory.Contains(weapon))
                return EquipmentResult.WeaponNotFound;
            

            if(EquippedWeapon != null)
                Inventory.AddItem(EquippedWeapon);
            
            EquippedWeapon = weapon;

            return EquipmentResult.Success;
        }

        public EquipmentResult RestoreEquippedWeapon(Weapon weapon)
        {
            if (weapon == null)
                throw new ArgumentNullException(nameof(weapon));

            EquippedWeapon = weapon;

            return EquipmentResult.Success;
        }

        public EquipmentResult UnequipWeapon()
        {
            if (EquippedWeapon == null)
                return EquipmentResult.NotEquippedWeapon;

            Inventory.AddItem(EquippedWeapon);
            EquippedWeapon = null;
            return EquipmentResult.Success;

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

            
        }
        
    }
}
