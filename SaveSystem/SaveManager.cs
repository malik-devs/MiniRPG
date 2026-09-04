using MiniRPG.Characters;
using MiniRPG.Inventories;
using MiniRPG.Items;
using MiniRPG.Items.Equipments;
using System.Text.Json;

namespace MiniRPG.SaveSystem
{
    public class SaveManager
    {
        private const string SaveFolder = "DataSave";
        private const string SaveFileName = "savegame.json";

        private readonly string SaveFilePath;

        public SaveManager()
        {
            SaveFilePath = Path.Combine(SaveFolder, SaveFileName);
        }

        public void Save(Player player)
        {
            //لانشاء نسخه من بيانات اللاعب
            PlayerSaveData data = new PlayerSaveData();
            //البيانات الحالية 
            data.Name = player.Name;
            data.HP = player.HP;
            data.MaxHP = player.MaxHP;
            data.Gold = player.Gold;
            data.Level = player.Level;
            data.XP = player.XP;
            data.XpMax = player.XpMax;
            data.Defense = player.Defense;
            data.MaxDefense = player.MaxDefense;

            // حفظ عناصر الـ Inventory
            foreach (InventoryItem inventoryItem in player.Inventory.Items)
            {
                InventoryItemSaveData inventoryItemData = new InventoryItemSaveData();

                inventoryItemData.Item = CreateItemSaveData(inventoryItem.Item);
                inventoryItemData.Quantity = inventoryItem.Quantity;

                data.InventoryItems.Add(inventoryItemData);
            }
            if (player.EquippedWeapon != null)
            {
                data.EquippedWeapon = CreateItemSaveData(player.EquippedWeapon);
            }
            if (player.EquippedArmor != null)
            {
                data.EquippedArmor = CreateItemSaveData(player.EquippedArmor);
            }

            //لتحويل البيانات objects الى JSON
            string json = JsonSerializer.Serialize(data);

            //لانشاء المسار اذا لم يكن موجود
            Directory.CreateDirectory(SaveFolder);
            //كتابة البيانات في الملف
            File.WriteAllText(SaveFilePath, json);

        }

        public Player? Load()
        {
            
                //اذا لم يجد الملف لا يقرأ
                if (!File.Exists(SaveFilePath)) return null;

                //نعكس العملية التي في save
                string json = File.ReadAllText(SaveFilePath);

                //عكس عملية التحويل
                PlayerSaveData? data = JsonSerializer.Deserialize<PlayerSaveData>(json);

                //شرط احترازي
                if (data == null)
                {
                    return null;
                }

                //انشاء كائن جديد واسناد اليه البيانات المحفوظة
                Player player = new Player(data.Name);
                player.RestoreState(data);
                foreach (InventoryItemSaveData inventoryItemData in data.InventoryItems)
                {
                    Item? item = CreateItem(inventoryItemData.Item);

                    if (item != null)
                    {
                        player.Inventory.AddItem(item,inventoryItemData.Quantity);
                    }
                }
                if (data.EquippedWeapon != null)
                {
                    Item? item = CreateItem(data.EquippedWeapon);

                    if (item is Weapon weapon)
                    {
                        player.RestoreEquippedEquipment(weapon);
                    }
                }
                if (data.EquippedArmor != null)
                {
                    Item? item = CreateItem(data.EquippedArmor);

                    if (item is Armor armor)
                    {
                        player.RestoreEquippedEquipment(armor);
                    }
                }

                return player;
            
        }

        private ItemSaveData CreateItemSaveData(Item item)
        {
            ItemSaveData data = new ItemSaveData();

            data.Name = item.Name;
            data.Price = item.Price;
            data.Description = item.Description;

            if (item is Weapon weapon)
            {
                data.Type = nameof(Weapon);
                data.Damage = weapon.Damage;
                data.MaxDurability = weapon.MaxDurability;
                data.Durability = weapon.Durability;
            }
            else if (item is HealthPotion potion)
            {
                data.Type = nameof(HealthPotion);
                data.HealAmount = potion.HealAmount;
            }
            else if (item is Armor armor) 
            {
                data.Type = nameof(Armor);
                data.DamageReductionPercentage = armor.DamageReductionPercentage;
                data.MaxDurability = armor.MaxDurability;
                data.Durability = armor.Durability;
            }
            else if (item is DefensePotion defensePotion)
            {
                data.Type = nameof(DefensePotion);
                data.DefenseAmount = defensePotion.DefenseAmount;
            }

            return data;
        }

        private Item? CreateItem(ItemSaveData data)
        {
            if (data.Type == nameof(Weapon))
            {
                Weapon weapon = new Weapon(
                    data.Name,
                    data.Price,
                    data.Description,
                    data.Damage,
                    data.MaxDurability
                );

                weapon.RestoreDurability(data.Durability);

                return weapon;
            }

            if (data.Type == nameof(Armor))
            {
                Armor armor = new Armor(
                    data.Name,
                    data.Price,
                    data.Description,
                    data.MaxDurability,
                    data.DamageReductionPercentage
                );

                armor.RestoreDurability(data.Durability);

                return armor;
            }

            if (data.Type == nameof(HealthPotion))
            {
                return new HealthPotion(
                    data.Name,
                    data.Price,
                    data.Description,
                    data.HealAmount
                );
            }
            if (data.Type == nameof(DefensePotion))
            {
                return new DefensePotion(
                    data.Name,
                    data.Price,
                    data.Description,
                    data.DefenseAmount
                );
            }

            return null;
        }

    }
}