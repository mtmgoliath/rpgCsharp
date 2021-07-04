using System;
using System.ComponentModel.DataAnnotations;
using Engine.Models;
using Engine.Shared;

namespace Engine.Services
{
    public static class CombatService
    {
        public enum Combatant
        {
            Player,
            Opponent
        }

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
            
            int playerResult = DiceService.Instance.Roll(20, 1, modifier: player.GetAttributeValueModifier(player, "DEX")).Value;
            int opponentResult = DiceService.Instance.Roll(20, 1, modifier: opponent.GetAttributeValueModifier(opponent, "DEX")).Value;
            return playerResult >= opponentResult ? Combatant.Player : Combatant.Opponent;
        }

        public static bool AttackSucceeded(LivingEntity attacker, LivingEntity target)
        {
            // Currently using the same formula as FirstAttacker initiative.
            // This will change as we include attack/defense skills,
            // armor, weapon bonuses, enchantments/curses, etc.
            int playerDexterity = attacker.GetAttributeModifiedValue("DEX")*
                                  attacker.GetAttributeModifiedValue("DEX");
            int opponentDexterity = target.GetAttributeModifiedValue("DEX")*
                                    target.GetAttributeModifiedValue("DEX");
            decimal dexterityOffset = (playerDexterity - opponentDexterity) / 10m;
            int randomOffset = DiceService.Instance.Roll(20).Value - 10;
            decimal totalOffset = dexterityOffset + randomOffset;

            return DiceService.Instance.Roll(100).Value <= 50 + totalOffset;
        }
    }
}