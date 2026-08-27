
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

        protected Character(string name, int maxHP, int damage)
        {
            Name = name;
            HP = maxHP;
            MaxHP = maxHP;
            Damage = damage;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                throw new ArgumentException("Damage cannot be negative.");
            }
            HP -= damage;

            if (HP < 0) 
            { 
                HP = 0; 
            }
            
        }

        public virtual int Attack(Character target)
        {
            int damage = Damage;
            target.TakeDamage(damage);
            return damage;
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

       
    }
}
