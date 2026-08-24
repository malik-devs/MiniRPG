using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Classes
{
    /**
     * It's a Class who play 
     **/
    
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
        public Player(string name):base(name,100,15)
        {
            Gold = 500;
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
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"HP: {HP}/{MaxHP}");
            Console.WriteLine($"Gold: {Gold}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"XP: {XP}/{XpMax}");
            Console.WriteLine();
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

                LevelUpEvent?.Invoke(this, new LevelUpEventArgs(Level,Level-1));
            }
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

        public override void Attack(Character target)
        {
            target.TakeDamage(TotalDamage);
        }

        public EquipResult EquipWeapon(Weapon weapon)
        {
            if(weapon == null) 
                throw new ArgumentNullException(nameof(weapon));

            if (!Inventory.Contains(weapon))
                return EquipResult.WeaponNotFound;
            

            if(EquippedWeapon != null)
                Inventory.AddItem(EquippedWeapon);
            
            EquippedWeapon = weapon;

            return EquipResult.Success;
        }

    }
}
