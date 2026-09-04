using MiniRPG.Characters;
using MiniRPG.Characters.Enemies;
using MiniRPG.Combat;

namespace MiniRPG.UI
{
    public class CombatUI
    {
        private readonly CombatSystem combatSystem;

        public CombatUI(CombatSystem combatSystem)
        {
            this.combatSystem = combatSystem;
        }

        public BattleResult StartBattle(Player player, Enemy monster)
        {
            combatSystem.PrepareBattle(monster);

            Console.Clear();
            PrintBattleStart(player, monster);

            WaitForEnter("Press ENTER to start the battle...");

            while (!player.IsDead && !monster.IsDead)
            {
                // Player Turn
                Console.Clear();

                PrintTurnHeader("YOUR TURN");

                DamageResult damageResult =
                    combatSystem.PlayerAttack(player, monster);

                PrintBattleStatus(player, monster, damageResult);

                WaitForEnter("Press ENTER to continue...");

                if (monster.IsDead)
                    break;

                // Enemy Turn
                Console.Clear();

                PrintTurnHeader("ENEMY TURN");

                damageResult =
                    combatSystem.EnemyAttack(player, monster);

                PrintBattleStatus(monster, player, damageResult);

                WaitForEnter("Press ENTER to continue...");
            }

            BattleResult result =
                combatSystem.GetBattleResult(player, monster);

            if (result == BattleResult.Win)
            {
                combatSystem.GiveReward(player, monster);

                Console.Clear();
                PrintVictory(player, monster);

                return result;
            }

            Console.Clear();
            PrintDefeat(player, monster);

            return result;
        }

        private void PrintBattleStart(Player player, Enemy monster)
        {
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║           BATTLE START             ║");
            Console.WriteLine("╚════════════════════════════════════╝\n");

            Console.WriteLine(
                ConsoleUI.PrintBar(
                    player.Name,
                    player.HP,
                    player.MaxHP));

            Console.WriteLine("              VS");

            Console.WriteLine(
                ConsoleUI.PrintBar(
                    monster.Name,
                    monster.HP,
                    monster.MaxHP));

            Console.WriteLine("\n========================================\n");
        }

        private void PrintTurnHeader(string title)
        {
            Console.WriteLine("========================================");
            Console.WriteLine($"              {title}");
            Console.WriteLine("========================================\n");
        }

        private void PrintBattleStatus(
            Character attacker,
            Character defender,
            DamageResult damageResult)
        {
            Console.WriteLine(
                $"{attacker.Name} attacks =>> {defender.Name}!\n");

            Console.WriteLine(
                $" Total Damage: {damageResult.TotalDamage}");

            if (damageResult.ArmorAbsorbedDamage > 0)
            {
                Console.WriteLine(
                    $" Armor Absorbed: {damageResult.ArmorAbsorbedDamage}");
            }

            Console.WriteLine(
                $" Defense Damage: {damageResult.DefenseDamage}");

            Console.WriteLine(
                $" HP Damage: {damageResult.HPDamage}");

            Console.WriteLine();

            if (defender is Player defenderPlayer)
            {
                if (defenderPlayer.EquippedArmor != null)
                {
                    Console.WriteLine(
                        ConsoleUI.PrintBar(
                            defenderPlayer.EquippedArmor.Name,
                            defenderPlayer.EquippedArmor.Durability,
                            defenderPlayer.EquippedArmor.MaxDurability));
                }
            }
            else if (attacker is Player attackerPlayer)
            {
                if (attackerPlayer.EquippedWeapon != null)
                {
                    Console.WriteLine(
                        ConsoleUI.PrintBar(
                            attackerPlayer.EquippedWeapon.Name,
                            attackerPlayer.EquippedWeapon.Durability,
                            attackerPlayer.EquippedWeapon.MaxDurability));
                }
            }

            Console.WriteLine();

            if (defender.MaxDefense > 0)
            {
                Console.WriteLine(
                    ConsoleUI.PrintBar(
                        "Defense",
                        defender.Defense,
                        defender.MaxDefense));
            }

            Console.WriteLine(
                ConsoleUI.PrintBar(
                    defender.Name,
                    defender.HP,
                    defender.MaxHP));

            Console.WriteLine(
                "\n══════════════════════════════════\n");
        }

        private void PrintVictory(Player player, Enemy monster)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("               VICTORY!");
            Console.WriteLine("========================================\n");

            Console.WriteLine(
                $"You defeated {monster.Name}!\n");

            Console.WriteLine(
                "--------------- REWARDS ----------------\n");

            Console.WriteLine(
                $"XP Earned:   +{monster.XPReward}");

            Console.WriteLine(
                $"Gold Earned: +{monster.GoldReward}");

            Console.WriteLine(
                "----------------------------------------\n");

            Console.WriteLine(
                $"Current Level: {player.Level}");

            Console.WriteLine(
                $"Current XP:    {player.XP}");

            WaitForEnter("Press ENTER to return...");
        }

        private void PrintDefeat(Player player, Enemy monster)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("                DEFEAT");
            Console.WriteLine("========================================\n");

            Console.WriteLine(
                $"You were defeated by {monster.Name}.\n");

            Console.WriteLine(
                $"Final Level: {player.Level}");

            Console.WriteLine(
                $"Gold:        {player.Gold}");

            WaitForEnter("Press ENTER to continue...");
        }

        private void WaitForEnter(string message)
        {
            Console.WriteLine($"\n{message}");
            Console.ReadLine();
        }
    }
}