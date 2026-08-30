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

        public List<ItemSaveData> InventoryItems { get; set; } = new List<ItemSaveData>();

        public ItemSaveData? EquippedWeapon { get; set; }

    }
}
