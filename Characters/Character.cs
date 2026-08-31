
using MiniRPG.Combat;

namespace MiniRPG.Characters
{
    public abstract class Character
    {
        public string Name { get; protected set; }
        public int HP { get; protected set; }
        public int MaxHP { get; protected set; }

        public int Damage { get; protected set; }

        public bool IsDead
        {
            get
            {
                return HP <= 0;
            }
        }

        public int Defense { get; protected set; }
        public int MaxDefense { get; protected set; }

        protected Character(string name, int maxHP, int damage, int maxDefense)
        {
            Name = name;
            HP = maxHP;
            MaxHP = maxHP;
            Damage = damage;
            Defense = maxDefense;
            MaxDefense = maxDefense;
        }

        public DamageResult TakeDamage(int damage)
        {
            int defenseDamage = 0;
            int hpDamage = 0;


            if (damage < 0)
            {
                throw new ArgumentException("Damage cannot be negative.");
            }
            
            if(damage <= Defense)
            {
                defenseDamage = damage;
                Defense -= damage;
            }
            else
            {
                defenseDamage = Defense;
                int remainingDamage = damage - Defense;
                hpDamage = Math.Min(HP, remainingDamage);
                HP -= hpDamage;
                Defense = 0;
            }

            if (HP < 0) 
            { 
                HP = 0; 
            }

            return new DamageResult(damage, defenseDamage, hpDamage);
            
        }

        public virtual DamageResult Attack(Character target)
        {
            return target.TakeDamage(Damage);
        }

        public void Heal(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Heal cannot be negative.");
            }

            HP += amount;

            if(HP > MaxHP)
            {
                HP = MaxHP;
            }
        }

        public void RestoreDefense(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Defense restore amount cannot be negative.");
            }

            Defense += amount;

            if (Defense > MaxDefense)
            {
                Defense = MaxDefense;
            }
        }
        public void RestoreFullDefense()
        {
            Defense = MaxDefense;
        }


    }
}
