using MiniRPG.Characters;
using MiniRPG.Characters.Enemies;
using MiniRPG.Enums;
using MiniRPG.UI;
using System.Threading;


namespace MiniRPG.Combat
{
    public class CombatSystem
    {
        public BattleResult StartBattle(Player player, Enemy monster)
        {
            monster.RestoreFullHP();

            Console.Clear();
            PrintBattleStart(player, monster);

            WaitForEnter("Press ENTER to start the battel ...");
            int damage=0;

            while (!player.IsDead && !monster.IsDead)
            {
                //player turn
                
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("              YOUR TURN");
                Console.WriteLine("========================================\n");
                damage = player.Attack(monster);
                PrintBattleStatus(player, monster, damage);

                WaitForEnter("Press ENTER to continue...");

                if (monster.IsDead)
                {
                    break;
                }

                //Enemy Turn
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("              ENEMY TURN");
                Console.WriteLine("========================================\n");
                damage = monster.Attack(player);
                PrintBattleStatus(monster, player, damage);

                WaitForEnter("Press ENTER to continue...");
            }

            if (!player.IsDead && monster.IsDead)
            {
                player.ReceiveReward(monster.XPReward, monster.GoldReward);

                Console.Clear();
                PrintVictory(player, monster);

                return BattleResult.Win;
            }

            Console.Clear();
            PrintDefeat(player, monster);
            return BattleResult.Dead;

        }

        private void PrintBattleStart(Player player, Enemy monster)
        {
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║           BATTLE START             ║");
            Console.WriteLine("╚════════════════════════════════════╝\n");

            Console.WriteLine(ConsoleUI.PrintBar(player.Name, player.HP, player.MaxHP));
            Console.WriteLine("              VS");
            Console.WriteLine(ConsoleUI.PrintBar(monster.Name, monster.HP, monster.MaxHP));
            Console.WriteLine("\n========================================\n");
 
        }

        private void PrintBattleStatus(Character attacker, Character defender, int damage)
        {
            Console.WriteLine($"{attacker.Name} attacks =>> {defender.Name}!\n");
            Console.WriteLine($"Damage dealt: {attacker.Damage}\n");

            Console.WriteLine(ConsoleUI.PrintBar(defender.Name, defender.HP, defender.MaxHP));

            Console.WriteLine("\n══════════════════════════════════\n");
        }

        private void WaitForEnter(string message)
        {
            Console.WriteLine($"\n{message}");
            Console.ReadLine();
        }

        private void PrintVictory(Player player, Enemy monster)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("               VICTORY!");
            Console.WriteLine("========================================\n");

            Console.WriteLine($"You defeated {monster.Name}!\n");

            Console.WriteLine("--------------- REWARDS ----------------\n");

            Console.WriteLine($"XP Earned:   +{monster.XPReward}");
            Console.WriteLine($"Gold Earned: +{monster.GoldReward}");

            Console.WriteLine("----------------------------------------\n");

            Console.WriteLine($"Current Level: {player.Level}");
            Console.WriteLine($"Current XP:    {player.XP}");

            WaitForEnter("Press ENTER to return...");
        }

        private void PrintDefeat(Player player, Enemy monster)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("                DEFEAT");
            Console.WriteLine("========================================\n");

            Console.WriteLine($"You were defeated by {monster.Name}.\n");

            Console.WriteLine($"Final Level: {player.Level}");
            Console.WriteLine($"Gold:        {player.Gold}");

            WaitForEnter("Press ENTER to continue...");
        }

        

        

    }
}
