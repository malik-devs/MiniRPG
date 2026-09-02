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
        public int ArmorAbsorbedDamage { get; }
        public int DefenseDamage { get; }
        public int HPDamage { get; }

        public DamageResult(int totalDamage, int armorAbsorbedDamage, int defenseDamage, int hpDamage)
        {
            TotalDamage = totalDamage;
            ArmorAbsorbedDamage = armorAbsorbedDamage;
            DefenseDamage = defenseDamage;
            HPDamage = hpDamage;
        }
    }
}
