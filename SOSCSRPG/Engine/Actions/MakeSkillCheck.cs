﻿using System;
using Engine.Models;
using Engine.Services;
namespace Engine.Actions
{
    public class MakeSkillCheck : BaseAction, IAction
    {
        private readonly string _damageDice;
        public MakeSkillCheck(GameItem itemInUse, string damageDice)
            : base(itemInUse)
        {

        }
        public void Execute(LivingEntity actor, LivingEntity target)
        {
            string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
            string targetName = (target is Player) ? "you" : $"the {target.Name.ToLower()}";
            if (CombatService.AttackSucceeded(actor, target))
            {
                int damage = DiceService.Instance.Roll(_damageDice).Value + actor.GetAttributeValueRollModifier(actor, "STR");
                ReportResult($"{actorName} hit {targetName} for {damage} point{(damage > 1 ? "s" : "")}.");
                target.TakeDamage(damage);
            }
            else
            {
                ReportResult($"{actorName} missed {targetName}.");
            }
        }
    }
}