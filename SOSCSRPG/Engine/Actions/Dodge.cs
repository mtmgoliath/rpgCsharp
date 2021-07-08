using System;
using Engine.Models;
using Engine.Services;
namespace Engine.Actions
{
    public class Dodge : BaseAction, IAction
    {
        public Dodge(GameItem itemInUse)
            : base(itemInUse)
        {
            //Dodge will boost ArmourRating by DEX.    
        }
        public void Execute(LivingEntity actor, LivingEntity target)
        {
            actor.ActionTaken = "Dodged";
            ReportResult("Dodged");
        }
    }
}
