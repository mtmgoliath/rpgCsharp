using System;
using Engine.Models;
using Engine.Services;
namespace Engine.Actions
{
    public class Block : BaseAction, IAction
    {
        private readonly string _baseDefense;
        public Block(GameItem itemInUse, string baseDefense)
            : base(itemInUse)
        {
            if (itemInUse.Category != GameItem.ItemCategory.Shield)
            {
                throw new ArgumentException($"{itemInUse.Name} is a shield");
            }

            _baseDefense = baseDefense;
        }
        public void Execute(LivingEntity actor, LivingEntity target)
        {
            actor.ActionTaken = "Blocked";
            ReportResult("Blocked");
        }
    }
}