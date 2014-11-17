using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using JSA_Game.Battle_Controller.StatEffect;
using JSA_Game.CharClasses;

namespace JSA_Game.Battle_Controller
{
    static class BattleController
    {
        private const int BASE_HIT = 80;
        private const int BASE_PHYSICAL = 4;
        private const int BASE_SPELL = 5;
        private const int CRIT_CHANCE = 15;

        /// <summary>
        /// Checks that a particular action is valid. At the moment it simply checks that the user is within range of the target.
        /// </summary>
        /// <param name="action">Action to be performed.</param>
        /// <param name="userPosition">Position of the character performing the action. </param>
        /// <param name="targetPosition">Position of the target character.</param>
        /// <returns>True if the action is valid, false otherwise.</returns>
        public static Boolean isValidAction(Action action, Character user, Vector2 userPosition, Vector2 targetPosition)
        {
            return action.Range + action.AoeRange >= calculateDistance(userPosition, targetPosition);
        }

        /// <summary>
        /// Attempts to perform an action on a single target. Assumes that the action has already been checked to be valid.
        /// </summary>
        /// <param name="action">Action to be performed.</param>
        /// <param name="user">Character performing the action.</param>
        /// <param name="target">Target character.</param>
        /// <returns>True if the action was successful (didn't miss), false otherwise.</returns>
        public static Boolean performAction(Action action, Character user, Character target)
        {
            if (target == null)
            {
                return false;
            }

            if(!didActionHit(action, user, target))
            {
                return false;
            }
            
            calculateAction(action, user, target);
            

            return true;
        }

        public static Boolean performAction(Action action, Character user, Character[] targets)
        {
            Boolean ret_val = false;
            foreach (Character target in targets)
            {
                if (performAction(action, user, target))
                {
                    ret_val = true;
                }
            }

            return ret_val;
        }

        /// <summary>
        /// Calculates the distance between two positions on the board.
        /// </summary>
        /// <param name="position1">Position 1.</param>
        /// <param name="position2">Position 2.</param>
        /// <returns>Distance between two positions.</returns>
        private static int calculateDistance(Vector2 position1, Vector2 position2)
        {
            return Math.Abs((int)position1.X - (int)position2.X) + Math.Abs((int)position1.Y - (int)position2.Y);
        }

        /// <summary>
        /// Determines whether or not the action hits the intended target.
        /// </summary>
        /// <param name="action">Action to be performed.</param>
        /// <param name="user">Character performing the action.</param>
        /// <param name="target">Target character.</param>
        /// <returns>True if the action hit, false if it misses.</returns>
        private static Boolean didActionHit(Action action, Character user, Character target)
        {
            if (action.Aoe)
            {
                // Area of effect attacks always hit.
                return true;
            }

            Random rng = new Random();
            int random = rng.Next(0, 100);

            int chanceToHit = BASE_HIT;
            chanceToHit += (user.Accuracy / target.Dodge) + user.Accuracy;

            return chanceToHit >= random;
        }

        private static void calculateAction(Action action, Character user, Character target)
        {
            calculateUserCost(action, user);
            calculateTargetEffect(action, user, target);
        }

        /// <summary>
        /// Calculates the cost of an action on the user.
        /// </summary>
        /// <param name="action">Action to be performed.</param>
        /// <param name="user">User performing the action.</param>
        private static void calculateUserCost(Action action, Character user)
        {
            StatType[] costType = action.StatCost;

            foreach (StatType stat in costType)
            {
                switch (stat)
                {
                    case StatType.Accuracy:
                        user.Accuracy -= action.Cost;
                        break;
                    case StatType.Armor:
                        user.Armor -= action.Cost;
                        break;
                    case StatType.Dodge:
                        user.Dodge -= action.Cost;
                        break;
                    case StatType.Hp:
                        user.CurrHp -= action.Cost;
                        break;
                    case StatType.Magic:
                        user.Magic -= action.Cost;
                        break;
                    case StatType.Mp:
                        user.CurrMp -= action.Cost;
                        break;
                    case StatType.Resist:
                        user.Resist -= action.Cost;
                        break;
                    case StatType.Strength:
                        user.Strength -= action.Cost;
                        break;
                    default:
                        user.CurrMp -= action.Cost;
                        break;
                }
            }
        }

        /// <summary>
        /// Calculates the cost of an action on the target.
        /// </summary>
        /// <param name="action">Action to be performed.</param>
        /// <param name="user">User performing the action.</param>
        /// <param name="target">Target character.</param>
        
        private static void calculateTargetEffect(Action action, Character user, Character target)
        {
            double amount = 0;

            if (action.Type == ActionType.Physical)
            {
                if (action.IgnoreEnemyStats)
                {
                    amount = (user.Strength / 2);
                }
                else
                {
                    amount = (user.Strength / 2) - (target.Armor);
                }

                if (amount < 0)
                {
                    amount = 0;
                }

                amount += BASE_PHYSICAL;
            }
            else
            {
                if (action.IgnoreEnemyStats)
                {
                    amount = (user.Magic / 2);
                }
                else
                {
                    amount = (user.Magic / 2) - (target.Resist);
                }
                
                if (amount < 0)
                {
                    amount = 0;
                }

                amount += BASE_SPELL;
            }

            target.CurrHp -= calculateActionAmount(user, action, amount);

            if (target.CurrHp <= 0)
            {
                LevelUpManager.KillingBlow(user, target);
            }
        }

        private static int calculateActionAmount(Character user, Action action, double value)
        {
            if (value <= 0)
            {
                value = 1;
            }
            double amount = value * user.Level;

            Random rng = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            int rand = rng.Next((int) amount - (3), (int) amount + (3));
            amount = rand;
            
            if (amount <= 0)
            {
                amount = 1;
            }

            amount *= action.PowerMultiplier;

            if (criticalHit())
            {
                amount *= 1.5;
            }

            amount = Math.Ceiling(amount);

            Console.WriteLine(user.ClassName + " attack hit for " + amount + " damage.");

            return (int) amount;
        }

        private static Boolean criticalHit()
        {
            Random rng = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            int rand = rng.Next(0, 100);

            if (rand <= CRIT_CHANCE)
            {
                return true;
            }

            return false;
        }
    }
}
