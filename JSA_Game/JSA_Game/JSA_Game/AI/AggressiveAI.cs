using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using JSA_Game.Maps;
using JSA_Game.CharClasses;
using JSA_Game.Battle_Controller;

namespace JSA_Game.AI
{
    class AggressiveAI : iAI
    {
        Level currLevel;
        Character character;
        Vector2 targetPos;

        // AI knows what character it is and its surroundings.
        public AggressiveAI(Character c, Level currentLevel)
        {
            character = c;
            currLevel = currentLevel;
            targetPos = new Vector2(-1, -1);
        }

        public void move()
        {
            Dictionary<Vector2, Character> charList = currLevel.PlayerUnits.ContainsValue(character) ? currLevel.PlayerUnits : currLevel.EnemyUnits;
            Dictionary<Vector2, Character> targetList = currLevel.PlayerUnits.ContainsValue(character) ? currLevel.EnemyUnits : currLevel.PlayerUnits;

            //Picks closest target
            int dist;
            int shortestDist = 64;
            targetPos = new Vector2(-1, -1);
            foreach (KeyValuePair<Vector2, Character> t in targetList)
            {
                dist = currLevel.calcDist(character.Pos, t.Key);
                if (dist < shortestDist)
                {
                    shortestDist = dist;
                    targetPos = t.Key;
                }

            }

            //Move towards target if found
            if (!targetPos.Equals(new Vector2(-1, -1)))
            {
                currLevel.moveUnit(character.Pos, targetPos, true);
            }
        }


        public void attack()
        {
            if (currLevel.calcDist(character.Pos, targetPos) <= character.Attack.Range)
            {
                Character target = currLevel.PlayerUnits.ContainsKey(targetPos) ? currLevel.PlayerUnits[targetPos] : currLevel.EnemyUnits[targetPos];

                if (BattleController.isValidAction(character.Attack, character, character.Pos, targetPos) && currLevel.calcDist(character.Pos, targetPos) <= character.Attack.Range)
                {
                    System.Diagnostics.Debug.Print("Target HP is " + target.CurrHp);
                    BattleController.performAction(character.Attack, character, target);
                    System.Diagnostics.Debug.Print("Target HP now is " + target.CurrHp);
                }

                if (target.CurrHp < 1)
                {
                    currLevel.Board[(int)targetPos.X, (int)targetPos.Y].IsOccupied = false;
                    if (target.IsEnemy)
                        currLevel.EnemyUnits.Remove(targetPos);
                    else
                        currLevel.PlayerUnits.Remove(targetPos);
                }
            }
        }

    }
}
