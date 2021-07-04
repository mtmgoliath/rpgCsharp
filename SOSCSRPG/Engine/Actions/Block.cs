using System;
using Engine.Models;
using Engine.Services;
namespace Engine.Actions
{
    public class Block : BaseAction, IAction
    {
        private readonly string _rating;
        public Block(GameItem itemInUse, string rating)
            : base(itemInUse)
        {
            if (itemInUse.Category != GameItem.ItemCategory.Armour)
            {
                throw new ArgumentException($"{itemInUse.Name} is not armour");
            }

            _rating = rating;
        }
        public void Execute(LivingEntity actor, LivingEntity target)
        {
            actor.ActionTaken = "Blocked";
            ReportResult("Blocked");
        }
    }
}