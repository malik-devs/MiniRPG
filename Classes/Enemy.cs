using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Classes
{
    public class Enemy : Character
    {
        public int XPReward {  get; private set; }
        public int GoldReward { get; private set; }

        public Enemy(string name, int maxHP, int damage, int xpReward, int goldReward):base(name,  maxHP, damage)
        {
            XPReward = xpReward;
            GoldReward = goldReward;
        }


    }
}
