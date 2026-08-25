using MiniRPG.Characters;
using MiniRPG.Characters.Enemies;
using MiniRPG.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Combat
{
    public class CombatSystem
    {
        public BattleResult StartBattle(Player player, Enemy monster)
        {
            while(!player.IsDead && !monster.IsDead)
            {
                player.Attack(monster);
                PrintHealth(monster);

                if (monster.IsDead)
                {
                    break;
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
                return BattleResult.Win;
            }
            else
                return BattleResult.Dead;

        }
        public void PrintHealth(Character character)
        {
            Console.WriteLine($"{character.Name} --> {character.HP}\n");
        }
    }
}
