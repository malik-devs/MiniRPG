using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.Combat
{
    public class DamageResult
    {
        public int TotalDamage { get; }
        public int DefenseDamage { get; }
        public int HPDamage { get; }

        public DamageResult(int totalDamage, int defenseDamage, int hpDamage)
        {
            TotalDamage = totalDamage;
            DefenseDamage = defenseDamage;
            HPDamage = hpDamage;
        }
    }
}
