using System;
using Engine.Models;
using Engine.Services;
namespace Engine.Actions
{
    public class AttackWithWeapon : BaseAction, IAction
    {
        private readonly string _damageDice;
        private readonly string _attackStat;
        
        public AttackWithWeapon(GameItem itemInUse, string damageDice)
            : base(itemInUse)
        {
            if (itemInUse.Category != GameItem.ItemCategory.Weapon)
            {
                throw new ArgumentException($"{itemInUse.Name} is not a weapon");
            }
            if (string.IsNullOrWhiteSpace(damageDice))
            {
                throw new ArgumentException("damageDice must be valid dice notation");
            }

            _attackStat = itemInUse.AttackStat;
            _damageDice = damageDice;
        }
        public void Execute(LivingEntity actor, LivingEntity target)
        {
            actor.ActionTaken = "Attacked";
            string actorName = (actor is Player) ? "You" : $"The {actor.Name.ToLower()}";
            string targetName = (target is Player) ? "you" : $"the {target.Name.ToLower()}";
            if (CombatService.AttackSucceeded(actor, target) && CombatService.CriticalHit)
            {
                int damage = (2 * DiceService.Instance.Roll(_damageDice).Value)
                             + actor.GetAttributeValueModifier(actor, _attackStat);
                if (CombatService.EvaluateActionTaken(target.ActionTaken))
                {
                    damage = (int) Math.Floor(0.25 * damage);
                }

                if (damage < 0)
                {
                    damage = 0;
                }

                ReportResult($"{actorName} rolled {CombatService.AttackRollResult} to hit." + "\n" +
                             $"{actorName} critically hit {targetName} for {damage} point{(damage > 1 ? "s" : "")}!");
               // CombatService.AttackRollResult = 0;
                target.TakeDamage(damage);
            }
            else if (CombatService.AttackSucceeded(actor, target))
            {
                int damage = DiceService.Instance.Roll(_damageDice).Value 
                             + actor.GetAttributeValueModifier(actor, _attackStat);
                if (CombatService.EvaluateActionTaken(target.ActionTaken))
                {
                    damage = (int)Math.Floor(0.5 * damage);
                }

                if (damage < 0)
                {
                    damage = 0;
                }
                
                ReportResult($"{actorName} rolled {CombatService.AttackRollResult} to hit." + "\n" +
                             $"{actorName} hit {targetName} for {damage} point{(damage > 1 ? "s" : "")}.");
               // CombatService.AttackRollResult = 0;
                target.TakeDamage(damage);
            }
            else
            {
                ReportResult($"{actorName} missed {targetName}.");
            }
        }
    }
}