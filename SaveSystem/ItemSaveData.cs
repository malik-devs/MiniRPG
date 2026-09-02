namespace MiniRPG.SaveSystem
{
    public class ItemSaveData
    {
        public string Type { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public string Description { get; set; } = string.Empty;
        

        public int HealAmount {  get; set; }

        public int Damage { get; set; }
        public int MaxDurability {  get; set; }
        public int Durability { get; set; }
        public int DamageReductionPercentage { get; set; }
        public int DefenseAmount { get; set; }




    }
}
