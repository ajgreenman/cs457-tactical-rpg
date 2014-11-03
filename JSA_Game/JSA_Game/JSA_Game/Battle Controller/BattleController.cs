  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using JSA_Game.Battle_Controller.StatEffect;

namespace JSA_Game.Battle_Controller
{
    static class BattleController
    {
        private const int BASE_HIT = 75;

        /// <summary>
        /// Checks that a particular action is valid. At the moment it simply checks that the user is within range of the target.
        /// </summary>
        /// <param name="action">Action to be performed.</param>
        /// <param name="userPosition">Position of the character performing the action. </param>
        /// <param name="targetPosition">Position of the target character.</param>
        /// <returns>True if the action is valid, false otherwise.</returns>
        public static Boolean isValidAction(Action action, Character user, Vector2 userPosition, Vector2 targetPosition)
        {
            if (action.Aoe)
            {
                // If the action is an area of effect action, its range does not need to be checked.
                return true;
            }
            return action.Range >= calculateDistance(userPosition, targetPosition);
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

            if (action.Aoe)
            {
                // Different logic will need to be implemented for area of effect actions.
            }
            else
            {
                calculateAction(action, user, target);
            }

            return true;
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

            //StatType[] effectType = action.TargetStat;
            StatType[] effectType = {StatType.Hp};

            int amount = 0;
            foreach (StatType stat in effectType)
            {
                switch (stat)
                {
                    case StatType.Accuracy:
                        if (action.Type == ActionType.Spell)
                        {
                            amount = user.Magic / target.Resist + 5;
                        }
                        else
                        {
                            amount = user.Strength / target.Armor + 5;
                        }
                        target.Accuracy -= amount;
                        break;
                    case StatType.Armor:
                        if (action.Type == ActionType.Spell)
                        {
                            amount = user.Magic / target.Resist + 5;
                        }
                        else
                        {
                            amount = user.Strength / target.Armor + 5;
                        }
                        target.Armor -= amount;
                        break;
                    case StatType.Dodge:
                        if (action.Type == ActionType.Spell)
                        {
                            amount = user.Magic / target.Resist + 5;
                        }
                        else
                        {
                            amount = user.Strength / target.Armor + 5;
                        }
                        target.Dodge -= amount;
                        break;
                    case StatType.Hp:
                        if (action.Type == ActionType.Spell)
                        {
                            amount = user.Magic / target.Resist + 5;
                        }
                        else
                        {
                            amount = user.Strength / target.Armor + 5;
                        }
                        target.CurrHp -= amount;
                        break;
                    case StatType.Magic:
                        if (action.Type == ActionType.Spell)
                        {
                            amount = user.Magic / target.Resist + 5;
                        }
                        else
                        {
                            amount = user.Strength / target.Armor + 5;
                        }
                        target.Magic -= amount;
                        break;
                    case StatType.Mp:
                        if (action.Type == ActionType.Spell)
                        {
                            amount = user.Magic / target.Resist + 5;
                        }
                        else
                        {
                            amount = user.Strength / target.Armor + 5;
                        }
                        target.CurrMp -= amount;
                        break;
                    case StatType.Resist:
                        if (action.Type == ActionType.Spell)
                        {
                            amount = user.Magic / target.Resist + 5;
                        }
                        else
                        {
                            amount = user.Strength / target.Armor + 5;
                        }
                        target.Resist -= amount;
                        break;
                    case StatType.Strength:
                        if (action.Type == ActionType.Spell)
                        {
                            amount = user.Magic / target.Resist + 5;
                        }
                        else
                        {
                            amount = user.Strength / target.Armor + 5;
                        }
                        target.Strength -= amount;
                        break;
                    case StatType.Movement:
                        if (action.Type == ActionType.Spell)
                        {
                            amount = user.Magic / target.Resist + 5;
                        }
                        else
                        {
                            amount = user.Strength / target.Armor + 5;
                        }
                        target.Movement -= amount;
                        break;
                    default:
                        if (action.Type == ActionType.Physical)
                        {
                            amount = user.Strength / target.Armor + 5;
                        } 
                        else
                        {
                            amount = user.Magic / target.Resist + 5;
                        }
                        target.CurrHp -= amount;
                        break;
                }
         
       
            }
         
        }
        
    }
}
