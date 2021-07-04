using Engine.Actions;
using Newtonsoft.Json;

namespace Engine.Models
{
    public class GameItem
    {
        public enum ItemCategory
        {
            Miscellaneous,
            Weapon,
            Consumable,
            Armour
        }

        [JsonIgnore]
        public ItemCategory Category { get; }
        public int ItemTypeID { get; }
        [JsonIgnore]
        public string Name { get; }
        [JsonIgnore]
        public int Price { get; }
        [JsonIgnore]
        public string AttackStat { get; set; }
        [JsonIgnore]
        public int Base { get; set; }
        [JsonIgnore]
        public bool IsUnique { get; }
        [JsonIgnore]
        public IAction Action { get; set; }

        public GameItem(ItemCategory category, int itemTypeID, string name, int price,
            bool isUnique = false, IAction action = null, string attackStat = "STR", int baseDefense = 10)
        {
            Category = category;
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
            IsUnique = isUnique;
            Action = action;
            AttackStat = attackStat;
            Base = baseDefense;
        }

        public void PerformAction(LivingEntity actor, LivingEntity target)
        {
            Action?.Execute(actor, target);
        }

        public GameItem Clone()
        {
            return new GameItem(Category, ItemTypeID, Name, Price, IsUnique, Action);
        }
    }
}