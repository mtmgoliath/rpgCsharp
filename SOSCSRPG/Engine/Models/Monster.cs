using System.Collections.Generic;
using System.Collections.ObjectModel;
using Engine.Factories;
using Engine.Services;

namespace Engine.Models
{
    public class Monster : LivingEntity
    {
        private readonly List<ItemPercentage> _lootTable =
            new List<ItemPercentage>();

        public int ID { get; }
        public string ImageName { get; }
        public int RewardExperiencePoints { get; }

        public new ObservableCollection<PlayerAttribute> Attributes { get; } =
            new ObservableCollection<PlayerAttribute>();

        public Monster(int id, string name, string imageName,
                       int maximumHitPoints, ObservableCollection<PlayerAttribute> attributes,
                       GameItem currentWeapon,
                       int rewardExperiencePoints, int gold) :
            base(name, maximumHitPoints, maximumHitPoints, attributes, gold)
        {
            ID = id;
            ImageName = imageName;
            CurrentWeapon = currentWeapon;
            RewardExperiencePoints = rewardExperiencePoints;
            Attributes = attributes;
        }

        public void AddItemToLootTable(int id, int percentage)
        {
            // Remove the entry from the loot table,
            // if it already contains an entry with this ID
            _lootTable.RemoveAll(ip => ip.ID == id);

            _lootTable.Add(new ItemPercentage(id, percentage));
        }

        public Monster GetNewInstance()
        {
            // "Clone" this monster to a new Monster object
            Monster newMonster =
                new Monster(ID, Name, ImageName, MaximumHitPoints, Attributes,
                            CurrentWeapon, RewardExperiencePoints, Gold);

            foreach (ItemPercentage itemPercentage in _lootTable)
            {
                // Clone the loot table - even though we probably won't need it
                newMonster.AddItemToLootTable(itemPercentage.ID, itemPercentage.Percentage);

                // Populate the new monster's inventory, using the loot table
                if (DiceService.Instance.Roll(100).Value <= itemPercentage.Percentage)
                {
                    newMonster.AddItemToInventory(ItemFactory.CreateGameItem(itemPercentage.ID));
                }
            }

            return newMonster;
        }

        public int GetAttributeModifiedValue(string key)
        {
            var tempModValue = 0;
            foreach (PlayerAttribute attribute in Attributes)
            {
                if (attribute.Key == key)
                {
                    tempModValue = attribute.ModifiedValue;
                }

            }
            return tempModValue;
        }
    }
}