using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Classes
{
    public class Player
    {
        public string Name { get; set; }
        public int HP { get; private set; }
        public int MaxHP { get; private set; }
        public int Gold { get; private set; }
        public int Level { get; private set; }
        public int XP { get; private set; }

        public Player( string name)
        {
            Name = name;
            HP = 100;
            MaxHP = 100;
            Gold = 50;
            Level = 1;
            XP = 0;
        }

        public void TakeDamage(int damage)
        {
           if(damage < 0)
            {
                throw new ArgumentException("Damage cannot be negative.");
            }
           else
            {
               HP -= damage;
                if (HP < 0) HP = 0;
            }
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
            Console.WriteLine($"XP: {XP}");
        }

    }
}
