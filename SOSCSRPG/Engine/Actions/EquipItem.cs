using System;
using Engine.Models;
using Engine.Services;
namespace Engine.Actions
{
    public class EquipItem : BaseAction, IAction
    {
        private readonly int _baseDefense;

        public EquipItem(GameItem itemInUse)
            : base(itemInUse)
        {
            if (itemInUse.Category != GameItem.ItemCategory.Armour)
            {
                throw new ArgumentException($"{itemInUse.Name} is not armour.");
            }

            _baseDefense = itemInUse.BaseDefense;

        }
        public void Execute(LivingEntity actor, LivingEntity target)
        {
            actor.ActionTaken = "EquippedItem";
            ReportResult("You equipped " + _itemInUse.Name);
            actor.ArmourRating = _baseDefense + actor.GetAttributeValueRollModifier(actor, "DEX");
        }
    }
}