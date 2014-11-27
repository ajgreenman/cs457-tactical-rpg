using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using JSA_Game.Battle_Controller.StatEffect;
using JSA_Game.CharClasses;
using JSA_Game.Maps;

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
            return hasEnough(action, user) && (action.Range + action.AoeRange >= calculateDistance(userPosition, targetPosition));
        }

        private static Boolean hasEnough(Action action, Character user)
        {
            foreach (StatType type in action.StatCost)
            {
                switch (type)
                {
                    case StatType.Accuracy:
                        if (user.Accuracy < action.Cost)
                            return false;
                        break;
                    case StatType.Armor:
                        if (user.Armor < action.Cost)
                            return false;
                        break;
                    case StatType.Dodge:
                        if (user.Dodge < action.Cost)
                            return false;
                        break;
                    case StatType.Hp:
                        if (user.CurrHp < action.Cost)
                            return false;
                        break;
                    case StatType.Magic:
                        if (user.Magic < action.Cost)
                            return false;
                        break;
                    case StatType.Mp:
                        if (user.CurrMp < action.Cost)
                            return false;
                        break;
                    case StatType.Resist:
                        if (user.Resist < action.Cost)
                            return false;
                        break;
                    case StatType.Strength:
                        if (user.Strength < action.Cost)
                            return false;
                        break;
                }
            }

            return true;
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
            if (action.Name != "Defend" || action.Name != "Rest")
            {
                if (target == null)
                {
                    return false;
                }

                if (!didActionHit(action, user, target))
                {
                    floatingTextLogic("Miss", user, 0);
                    Game1.PlaySound("miss");
                    return false;
                }

                calculateAction(action, user, target);

                if (!action.Aoe)
                {
                    Game1.PlaySound(action.Sound);
                }
            }
            else
            {
                if (action.Name == "Defend")
                {
                    user.Armor += 4;
                    user.Resist += 4;
                    Game1.PlaySound(action.Sound);
                    floatingTextLogic("Defend", user, 0);
                }
                else
                {
                    int value = ((int)(user.MaxHP * .1));
                    user.CurrHp += value;
                    user.CurrMp += ((int)(user.MaxMP * .1));
                    Game1.PlaySound(action.Sound);
                    floatingTextLogic("Heal", user, value);
                }
                
            }
            

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

            Game1.PlaySound(action.Sound);

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

            int value = calculateActionAmount(user, action, amount);
            target.CurrHp -= value;

            if (action.Friendly)
            {
                floatingTextLogic("Heal", user, value);
            }
            else
            {
                floatingTextLogic("Attack", user, value);
            }

            if (target.CurrHp <= 0)
            {
                LevelUpManager.KillingBlow(user, target);
            }
            else
            {
                if (action.ActionEffect != null)
                {
                    if (action.ActionEffect.Friendly)
                    {
                        target.Status[0] = action.ActionEffect;
                    }
                    else
                    {
                        target.Status[1] = action.ActionEffect;
                    }
                }
            }
        }

        public static void newTurn(Level level)
        {
            level.Turn++;
            foreach (Character c in level.PUnits)
            {
                for(int i = 0; i < c.Status.Length; i++)
                {
                    if (c.Status[i] != null)
                    {
                        if (c.Status[i].TurnByTurn)
                        {
                            if (c.Status[i].Friendly)
                            {
                                for(int j = 0; j < c.Status[i].AffectedStats.Length; j++)
                                {
                                    switch (c.Status[i].AffectedStats[j])
                                    {
                                        case StatType.Accuracy:
                                            c.Accuracy += c.Status[i].Amount[j];
                                            break;
                                        case StatType.Armor:
                                            c.Armor += c.Status[i].Amount[j];
                                            break;
                                        case StatType.Dodge:
                                            c.Dodge += c.Status[i].Amount[j];
                                            break;
                                        case StatType.Hp:
                                            c.CurrHp += c.Status[i].Amount[j];
                                            break;
                                        case StatType.Magic:
                                            c.Magic += c.Status[i].Amount[j];
                                            break;
                                        case StatType.Mp:
                                            c.CurrMp += c.Status[i].Amount[j];
                                            break;
                                        case StatType.Resist:
                                            c.Resist += c.Status[i].Amount[j];
                                            break;
                                        case StatType.Strength:
                                            c.Strength += c.Status[i].Amount[j];
                                            break;
                                        default:
                                            c.CurrHp += c.Status[i].Amount[j];
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < c.Status[i].AffectedStats.Length; j++)
                                {
                                    switch (c.Status[i].AffectedStats[j])
                                    {
                                        case StatType.Accuracy:
                                            c.Accuracy -= c.Status[i].Amount[j];
                                            break;
                                        case StatType.Armor:
                                            c.Armor -= c.Status[i].Amount[j];
                                            break;
                                        case StatType.Dodge:
                                            c.Dodge -= c.Status[i].Amount[j];
                                            break;
                                        case StatType.Hp:
                                            c.CurrHp -= c.Status[i].Amount[j];
                                            break;
                                        case StatType.Magic:
                                            c.Magic -= c.Status[i].Amount[j];
                                            break;
                                        case StatType.Mp:
                                            c.CurrMp -= c.Status[i].Amount[j];
                                            break;
                                        case StatType.Resist:
                                            c.Resist -= c.Status[i].Amount[j];
                                            break;
                                        case StatType.Strength:
                                            c.Strength -= c.Status[i].Amount[j];
                                            break;
                                        default:
                                            c.CurrHp -= c.Status[i].Amount[j];
                                            break;
                                    }
                                }
                            }
                        }

                        if (level.Turn >= c.Status[i].Expiration)
                        {
                            c.Status[i] = null;
                        }
                    }
                }
            }

            foreach (Character c in level.EUnits)
            {
                for (int i = 0; i < c.Status.Length; i++)
                {
                    if (c.Status[i] != null)
                    {
                        if (c.Status[i].TurnByTurn)
                        {
                            if (c.Status[i].Friendly)
                            {
                                for (int j = 0; j < c.Status[i].AffectedStats.Length; j++)
                                {
                                    switch (c.Status[i].AffectedStats[j])
                                    {
                                        case StatType.Accuracy:
                                            c.Accuracy += c.Status[i].Amount[j];
                                            break;
                                        case StatType.Armor:
                                            c.Armor += c.Status[i].Amount[j];
                                            break;
                                        case StatType.Dodge:
                                            c.Dodge += c.Status[i].Amount[j];
                                            break;
                                        case StatType.Hp:
                                            c.CurrHp += c.Status[i].Amount[j];
                                            break;
                                        case StatType.Magic:
                                            c.Magic += c.Status[i].Amount[j];
                                            break;
                                        case StatType.Mp:
                                            c.CurrMp += c.Status[i].Amount[j];
                                            break;
                                        case StatType.Resist:
                                            c.Resist += c.Status[i].Amount[j];
                                            break;
                                        case StatType.Strength:
                                            c.Strength += c.Status[i].Amount[j];
                                            break;
                                        case StatType.Movement:
                                            c.Movement += c.Status[i].Amount[j];
                                            break;
                                        default:
                                            c.CurrHp += 0;
                                            break;
                                    }
                                }
                            }
                            else
                            {
                                for (int j = 0; j < c.Status[i].AffectedStats.Length; j++)
                                {
                                    switch (c.Status[i].AffectedStats[j])
                                    {
                                        case StatType.Accuracy:
                                            c.Accuracy -= c.Status[i].Amount[j];
                                            break;
                                        case StatType.Armor:
                                            c.Armor -= c.Status[i].Amount[j];
                                            break;
                                        case StatType.Dodge:
                                            c.Dodge -= c.Status[i].Amount[j];
                                            break;
                                        case StatType.Hp:
                                            c.CurrHp -= c.Status[i].Amount[j];
                                            break;
                                        case StatType.Magic:
                                            c.Magic -= c.Status[i].Amount[j];
                                            break;
                                        case StatType.Mp:
                                            c.CurrMp -= c.Status[i].Amount[j];
                                            break;
                                        case StatType.Resist:
                                            c.Resist -= c.Status[i].Amount[j];
                                            break;
                                        case StatType.Strength:
                                            c.Strength -= c.Status[i].Amount[j];
                                            break;
                                        case StatType.Movement:
                                            c.Movement -= c.Status[i].Amount[j];
                                            break;
                                        default:
                                            c.CurrHp -= 0;
                                            break;
                                    }
                                }
                            }
                        }

                        if (level.Turn >= c.Status[i].Expiration)
                        {
                            c.Status[i] = null;
                        }
                    }
                }
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
        private static void floatingTextLogic(String actionType, Character user, int amount)
        {
            switch (actionType)
            {
                case "Defend":
                    user.CurrDamage = -1;
                    user.CurrHealing = -1;
                    user.Miss = false;
                    user.DidDefend = true;
                    break;
                case "Attack":
                    user.CurrDamage = amount;
                    user.CurrHealing = -1;
                    user.Miss = false;
                    user.DidDefend = false;
                    break;
                case "Heal":
                    user.CurrDamage = -1;
                    user.CurrHealing = amount;
                    user.Miss = false;
                    user.DidDefend = false;
                    break;
                case "Miss":
                    user.CurrDamage = -1;
                    user.CurrHealing = -1;
                    user.Miss = true;
                    user.DidDefend = false;
                    break;
            }

            user.Set = true;
        }
    }
}


