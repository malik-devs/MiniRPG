using MiniRPG.Characters;
using System.Text.Json;
using MiniRPG.Items;

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
            foreach (Item item in player.Inventory.Items)
            {
                ItemSaveData itemData = CreateItemSaveData(item);

                data.InventoryItems.Add(itemData);
            }
            if (player.EquippedWeapon != null)
            {
                data.EquippedWeapon = CreateItemSaveData(player.EquippedWeapon);
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
            try
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
                foreach (ItemSaveData itemData in data.InventoryItems)
                {
                    Item? item = CreateItem(itemData);

                    if (item != null)
                    {
                        player.Inventory.AddItem(item);
                    }
                }
                if (data.EquippedWeapon != null)
                {
                    Item? item = CreateItem(data.EquippedWeapon);

                    if (item is Weapon weapon)
                    {
                        player.RestoreEquippedWeapon(weapon);
                    }
                }

                return player;
            }
            catch (JsonException)
            {
                return null;
            }
            catch (IOException)
            {
                return null;
            }
        }

        private ItemSaveData CreateItemSaveData(Item item)
        {
            ItemSaveData data = new ItemSaveData();

            data.Name = item.Name;
            data.Price = item.Price;
            data.Description = item.Description;

            if(item is Weapon weapon)
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

            if (data.Type == nameof(HealthPotion))
            {
                return new HealthPotion(
                    data.Name,
                    data.Price,
                    data.Description,
                    data.HealAmount
                );
            }

            return null;
        }

    }
}