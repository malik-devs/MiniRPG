using System.Collections.Generic;

namespace MiniRPG.SaveSystem
{
    public class PlayerSaveData
    {
        public string Name { get; set; } = string.Empty;

        public int HP { get; set; }

        public int MaxHP { get; set; }

        public int Gold { get; set; }

        public int Level { get; set; }

        public int XP { get; set; }

        public int XpMax { get; set; }

        public int Defense { get; set; }
        public int MaxDefense { get; set; }

        public List<InventoryItemSaveData> InventoryItems { get; set; } = new List<InventoryItemSaveData>();
        public ItemSaveData? EquippedWeapon { get; set; }
        public ItemSaveData? EquippedArmor { get; set; }


    }
}
