using MiniRPG.Characters;
using MiniRPG.Characters.Enemies;

namespace MiniRPG.Combat
{
    public class CombatSystem
    {
        public void PrepareBattle(Enemy monster)
        {
            monster.RestoreFullHP();
        }

        public DamageResult PlayerAttack(Player player, Enemy monster)
        {
            return player.Attack(monster);
        }

        public DamageResult EnemyAttack(Player player, Enemy monster)
        {
            return monster.Attack(player);
        }

        public BattleResult GetBattleResult(Player player, Enemy monster)
        {
            if (!player.IsDead && monster.IsDead)
                return BattleResult.Win;

            if (player.IsDead)
                return BattleResult.Dead;

            return BattleResult.InProgress;
        }

        public void GiveReward(Player player, Enemy monster)
        {
            player.ReceiveReward(
                monster.XPReward,
                monster.GoldReward
            );
        }
    }
}