using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Characters.Enemies
{
    public class EnemyFactory
    {
        private List<Enemy> enemies = new List<Enemy>();
        private Random _random = new Random();
        public EnemyFactory()
        {
           CreateEnemies();
        }
        private void CreateEnemies()
        {
            enemies.Add(new Enemy("Goblin Scout", 35, 8, 5, 15, 10, 1, 3));
            enemies.Add(new Enemy("Skeleton Warrior", 60, 14, 10, 30, 22, 2, 5));
            enemies.Add(new Enemy("Wild Orc", 110, 22, 15, 65, 45, 4, 7));
            enemies.Add(new Enemy("Shadow Assassin", 150, 40, 20, 90, 70, 6, 9));
            enemies.Add(new Enemy("Stone Golem", 220, 45, 30, 150, 140, 8, 10));
        }

        private List<Enemy> GetReadyEnemies(int playerLevel)
        {
           List<Enemy> readyEnemies = new List<Enemy>();

            foreach (Enemy enemy in enemies)
            {
                if(enemy.MinLevel <= playerLevel && playerLevel <= enemy.MaxLevel)
                {
                    readyEnemies.Add(enemy);
                }
            }
            return readyEnemies;
        }

        public Enemy? GetEnemy(int playerLevel)
        {
            List<Enemy> readyEnemies = GetReadyEnemies(playerLevel);
            if (readyEnemies.Count == 0)
                return null;

            int randomIndex = _random.Next(0, readyEnemies.Count);

            return readyEnemies[randomIndex];
        }

    }
}
