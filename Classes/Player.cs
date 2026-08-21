using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Classes
{
    public class Player : Character
    {

        public int Gold { get; private set; }
        public int Level { get; private set; }
        public int XP { get; private set; }

        public Player(string name):base(name,100,15)
        {
            Gold = 50;
            Level = 1;
            XP = 0;
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
