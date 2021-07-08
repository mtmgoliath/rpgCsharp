using System;
using System.ComponentModel.DataAnnotations;
using Engine.Models;
using Engine.Shared;
using MathNet.Numerics;

namespace Engine.Services
{
    public static class CombatService
    {
        public enum Combatant
        {
            Player,
            Opponent
        }

        //add in crit result log
        
        public static bool CriticalHit { get; set; }
        public static int AttackRollResult { get; set; }

        public static Combatant FirstAttacker(Player player, Monster opponent)
        {
            // Formula is: ((Dex(player)^2 - Dex(monster)^2)/10) + Random(-10/10)
            // For dexterity values from 3 to 18, this should produce an offset of +/- 41.5
            //int playerDexterity = player.GetAttributeModifiedValue("DEX")*
            //                      player.GetAttributeModifiedValue("DEX");
            //int opponentDexterity = opponent.GetAttributeModifiedValue("DEX")*
            //                        opponent.GetAttributeModifiedValue("DEX");
            //decimal dexterityOffset = (playerDexterity - opponentDexterity) / 10m;
            //int randomOffset = DiceService.Instance.Roll(20).Value - 10;
            //decimal totalOffset = dexterityOffset + randomOffset;

            // return DiceService.Instance.Roll(100).Value <= 50 + totalOffset
            //          ? Combatant.Player
            //        : Combatant.Opponent;

            //Roll for initiative! d20 + DexMod
            
            int playerResult = DiceService.Instance.Roll(20, 1, modifier: player.GetAttributeValueRollModifier(player, "DEX")).Value;
            int opponentResult = DiceService.Instance.Roll(20, 1, modifier: opponent.GetAttributeValueRollModifier(opponent, "DEX")).Value;
            return playerResult >= opponentResult ? Combatant.Player : Combatant.Opponent;
        }

        public static bool BlockActionTaken(string actionTaken)
        {
            return actionTaken == "Blocked";
        }
        public static bool AttackActionTaken(string actionTaken)
        {
            return actionTaken == "Attacked";
        }
        public static bool DodgeActionTaken(string actionTaken)
        {
            return actionTaken == "Dodged";
        }

        public static bool AttackSucceeded(LivingEntity attacker, LivingEntity target)
        {
            int attackRollResultWithoutModifier = DiceService.Instance.Roll(20).Value;
            CriticalHit = EvaluateCriticalSuccess(attackRollResultWithoutModifier);
            

            int attackRollResult = attackRollResultWithoutModifier +
                                   attacker.GetAttributeValueRollModifier(attacker, attacker.CurrentWeapon.AttackStat);
            
            int armourRatingToBeat = target.ArmourRating;
            attacker.LastAttackRollResult = attackRollResult;
            return attackRollResult >= armourRatingToBeat;
        }

        public static bool EvaluateCriticalSuccess(int diceResult)
        {
            //return diceResult == 20 ? Critical.Success : Critical.Nope;
            return diceResult == 20;
        }

        
    }
}