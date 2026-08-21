using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Classes
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

        public void Attack(Character target)
        {
            target.TakeDamage(Damage);
        }

       
    }
}
