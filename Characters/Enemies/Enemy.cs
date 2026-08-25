using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Characters.Enemies
{
    public class Enemy : Character
    {
        public int XPReward {  get; private set; }
        public int GoldReward { get; private set; }
        public int MinLevel {  get; private set; }
        public int MaxLevel { get; private set; }

        public Enemy(string name, int maxHP, int damage, int xpReward, int goldReward, int minLevel, int maxLevel):base(name,  maxHP, damage)
        {
            XPReward = xpReward;
            GoldReward = goldReward;
            MinLevel = minLevel;
            MaxLevel = maxLevel;
        }


    }
}
