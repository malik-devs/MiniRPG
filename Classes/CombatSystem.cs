using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Classes
{
    public class CombatSystem
    {
        public void StartBattle(Player player, Enemy monster)
        {
            while(!player.IsDead && !monster.IsDead)
            {
                player.Attack(monster);
                PrintHealth(monster);

                if (monster.IsDead)
                {
                    Console.WriteLine($"{monster.Name} defeated\n");
                }
                else
                {
                    monster.Attack(player);
                    PrintHealth(player);
                }
            }
            if (!player.IsDead && monster.IsDead)
            {
                player.ReceiveReward(monster.XPReward, monster.GoldReward);
                player.PrintStats();
            }
            else
                Console.WriteLine("YOU DEAD");

        }
        public void PrintHealth(Character character)
        {
            Console.WriteLine($"{character.Name} --> {character.HP}\n");
        }
    }
}
