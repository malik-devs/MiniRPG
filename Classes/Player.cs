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
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public float Gold { get; set; }
        public int Level { get; set; }
        public int XP { get; set; }

        public Player( string name)
        {
            Name = name;
            HP = 100;
            MaxHP = 100;
            Gold = 50;
            Level = 1;
            XP = 0;
        }

        public void printStats()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"HP: {HP}/{MaxHP}");
            Console.WriteLine($"Gold: {Gold}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"XP: {XP}");
        }

    }
}
