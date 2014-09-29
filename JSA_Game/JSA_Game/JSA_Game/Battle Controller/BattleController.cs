using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game
{
    // Ideas
    // Add functionality for actions that affect more than one target.
    // Add functionality for actions against non-characters (for example, land).

    class BattleController
    {
        BattleController()
        {
            // Does anything actually need to be initialized?
        }

        /// <summary>
        /// Attempts to initiate an action on a target character.
        /// </summary>
        /// <param name="action">Attempted action.</param>
        /// <param name="unit">Character initiating the action.</param>
        /// <param name="target">Target character to perform the action on.</param>
        /// <returns>True if the action is valid, false otherwise.</returns>
        public Boolean initiateAction(Action action, Character unit, Character target)
        {
            if(!isValidAction(action, unit, target))
            {
                return false;
            }

            if (didUnitMiss(unit, target))
            {
                return false;
            }

            unit.CurrMp -= action.MpCost;

            int amount;
            switch (action.StatTarget)
            {
                case "hp":
                    amount = calculateHp(action, unit, target);
                    target.CurrHp -= amount;
                    break;
                case "mp":
                    amount = calculateMp(action, unit, target);
                    target.CurrMp -= amount;
                    break;
                case "strength":
                    amount = calculateStrength(action, unit, target);
                    target.Strength -= amount;
                    break;
                case "armor":
                    amount = calculateArmor(action, unit, target);
                    target.Armor -= amount;
                    break;
                case "accuracy":
                    amount = calculateAccuracy(action, unit, target);
                    target.Accuracy -= amount;
                    break;
                case "dodge":
                    amount = calculateDodge(action, unit, target);
                    target.Dodge -= amount;
                    break;
                case "magic":
                    amount = calculateMagic(action, unit, target);
                    target.Magic -= amount;
                    break;
                case "resist":
                    amount = calculateResist(action, unit, target);
                    target.Resist -= amount;
                    break;
                default:
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether an action is valid or not. An action is valid if the character performing
        /// the action is both within range of the target and if the target character is a valid target 
        /// to perform the action on.
        /// </summary>
        /// <param name="action">Attempted action.</param>
        /// <param name="unit">Character initiating the action.</param>
        /// <param name="target">Target character to perform the action on.</param>
        /// <returns>True if the action is valid, false otherwise.</returns>
        private Boolean isValidAction(Action action, Character unit, Character target)
        {
            return isValidTarget(unit, target, action) && isWithinRange(action, unit, target);
        }

        /// <summary>
        /// Checks if the target character is a valid target to perform the action on.
        /// </summary>
        /// <param name="unit">Character initiating the action.</param>
        /// <param name="target">Target character to perform the action on.</param>
        /// <param name="action">Attempted action.</param>
        /// <returns>True is the target is valid, false otherwise.</returns>
        private Boolean isValidTarget(Character unit, Character target, Action action)
        {
            if (action.Friendly)
            {
                return unit.IsEnemy == target.IsEnemy;
            }
            else
            {
                return unit.IsEnemy != target.IsEnemy;
            }
        }

        /// <summary>
        /// Checks that the unit performing the action is within range of the target.
        /// </summary>
        /// <param name="action">Attempted action.</param>
        /// <param name="unit">Chracter initiating the action.</param>
        /// <param name="target">Attempted action.</param>
        /// <returns>True if target is within range, false otherwise.</returns>
        private Boolean isWithinRange(Action action, Character unit, Character target)
        {
            int distance = calculateRange(unit, target);

            if (action.Ranged)
            {
                return distance < 10;
            }
            else
            {
                return distance == 1;
            }
        }

        /// <summary>
        /// Calculates the distance between two characters.
        /// </summary>
        /// <param name="char1">First character.</param>
        /// <param name="char2">Second character</param>
        /// <returns>Distance between the two characters.</returns>
        private int calculateRange(Character char1, Character char2)
        {
            float xDistance = Math.Abs(char1.Pos.X - char2.Pos.X);
            float yDistance = Math.Abs(char1.Pos.Y - char2.Pos.Y);

            return (int) (xDistance + yDistance);
        }

        /// <summary>
        /// Determines whether or not the action will miss.
        /// </summary>
        /// <param name="unit">Character initiating the action.</param>
        /// <param name="target">Target character to perform the action on.</param>
        /// <returns>True if the action missed, false otherwise.</returns>
        private Boolean didUnitMiss(Character unit, Character target)
        {
            Random rng = new Random(); // Seed this.

            int chanceToHit = unit.Accuracy - target.Dodge + 70;
            int value = rng.Next(100);

            return chanceToHit < value;
        }

        /// <summary>
        /// Calculates the amount of hp affected by an action.
        /// </summary>
        /// <param name="action">Attempted action.</param>
        /// <param name="unit">Character initiating the action.</param>
        /// <param name="target">Target character to perform the action on.</param>
        /// <returns>Amount of hp affected.</returns>
        private int calculateHp(Action action, Character unit, Character target)
        {
            int amount = 0;

            if (action.Spell)
            {
                amount = unit.Magic - target.Resist + 5;
            }
            else
            {
                amount = unit.Strength - target.Armor + 5;
            }

            return amount;
        }

        /// <summary>
        /// Calculates the amount of mp affected by an action.
        /// </summary>
        /// <param name="action">Attempted action.</param>
        /// <param name="unit">Character initiating the action.</param>
        /// <param name="target">Target character to perform the action on.</param>
        /// <returns>Amount of mp affected.</returns>
        private int calculateMp(Action action, Character unit, Character target)
        {
            int amount = 0;

            if (action.Spell)
            {
                amount = unit.Magic - target.Resist + 5;
            }
            else
            {
                amount = unit.Strength - target.Resist + 5;
            }

            return amount;
        }

        /// <summary>
        /// Calculates the amount of strength affected by an action.
        /// </summary>
        /// <param name="action">Attempted action.</param>
        /// <param name="unit">Character initiating the action.</param>
        /// <param name="target">Target character to perform the action on.</param>
        /// <returns>Amount of strength affected.</returns>
        private int calculateStrength(Action action, Character unit, Character target)
        {
            int amount = 0;

            if (action.Spell)
            {
                amount = unit.Magic - target.Resist + 5;
            }
            else
            {
                amount = unit.Strength - target.Resist + 5;
            }

            return amount;
        }


        /// <summary>
        /// Calculates the amount of armor affected by an action.
        /// </summary>
        /// <param name="action">Attempted action.</param>
        /// <param name="unit">Character initiating the action.</param>
        /// <param name="target">Target character to perform the action on.</param>
        /// <returns>Amount of armor affected.</returns>
        private int calculateArmor(Action action, Character unit, Character target)
        {
            int amount = 0;

            if (action.Spell)
            {
                amount = unit.Magic - target.Armor + 5;
            }
            else
            {
                amount = unit.Strength - target.Armor + 5;
            }

            return amount;
        }


        /// <summary>
        /// Calculates the amount of accuracy affected by an action.
        /// </summary>
        /// <param name="action">Attempted action.</param>
        /// <param name="unit">Character initiating the action.</param>
        /// <param name="target">Target character to perform the action on.</param>
        /// <returns>Amount of accuracy affected.</returns>
        private int calculateAccuracy(Action action, Character unit, Character target)
        {
            int amount = 0;

            if (action.Spell)
            {
                amount = unit.Magic - target.Resist + 5;
            }
            else
            {
                amount = unit.Strength - target.Armor + 5;
            }

            return amount;
        }


        /// <summary>
        /// Calculates the amount of dodge affected by an action.
        /// </summary>
        /// <param name="action">Attempted action.</param>
        /// <param name="unit">Character initiating the action.</param>
        /// <param name="target">Target character to perform the action on.</param>
        /// <returns>Amount of dodge affected.</returns>
        private int calculateDodge(Action action, Character unit, Character target)
        {
            int amount = 0;

            if (action.Spell)
            {
                amount = unit.Magic - target.Resist + 5;
            }
            else
            {
                amount = unit.Strength - target.Armor + 5;
            }

            return amount;
        }


        /// <summary>
        /// Calculates the amount of magic affected by an action.
        /// </summary>
        /// <param name="action">Attempted action.</param>
        /// <param name="unit">Character initiating the action.</param>
        /// <param name="target">Target character to perform the action on.</param>
        /// <returns>Amount of magic affected.</returns>
        private int calculateMagic(Action action, Character unit, Character target)
        {
            int amount = 0;

            if (action.Spell)
            {
                amount = unit.Magic - target.Resist + 5;
            }
            else
            {
                amount = unit.Strength - target.Resist + 5;
            }

            return amount;
        }


        /// <summary>
        /// Calculates the amount of resist affected by an action.
        /// </summary>
        /// <param name="action">Attempted action.</param>
        /// <param name="unit">Character initiating the action.</param>
        /// <param name="target">Target character to perform the action on.</param>
        /// <returns>Amount of resist affected.</returns>
        private int calculateResist(Action action, Character unit, Character target)
        {
            int amount = 0;

            if (action.Spell)
            {
                amount = unit.Magic - target.Resist + 5;
            }
            else
            {
                amount = unit.Strength - target.Resist + 5;
            }

            return amount;
        }
    }
}
