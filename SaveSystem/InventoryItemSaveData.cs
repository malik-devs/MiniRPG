using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniRPG.SaveSystem
{
    public class InventoryItemSaveData
    {
        public ItemSaveData Item { get; set; } = new ItemSaveData();

        public int Quantity { get; set; }
    }
}
