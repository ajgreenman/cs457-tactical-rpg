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
            //Finds its position
            Vector2 pos = new Vector2(-1,-1);
            Dictionary<Vector2, Character> charList = currLevel.PlayerUnits.ContainsValue(character) ? currLevel.PlayerUnits : currLevel.EnemyUnits;
            Dictionary<Vector2, Character> targetList = currLevel.PlayerUnits.ContainsValue(character) ? currLevel.EnemyUnits : currLevel.PlayerUnits;
            foreach (KeyValuePair<Vector2, Character> i in charList)
            {
                if (i.Value == character)
                {
                    pos = i.Key;
                }
            }

            //Picks closest target
            int dist;
            int shortestDist = 64;
            targetPos = new Vector2(-1, -1);
            foreach (KeyValuePair<Vector2, Character> t in targetList)
            {
                dist = currLevel.calcDist(pos, t.Key);
                if (dist < shortestDist)
                {
                    shortestDist = dist;
                    targetPos = t.Key;
                }

            }

            //Move towards target if found
            if (!targetPos.Equals(new Vector2(-1, -1)))
            {
                currLevel.moveUnit(pos, targetPos, true);
            }
        }


        public void attack()
        {
            if (currLevel.calcDist(character.Pos, targetPos) <= character.AttackRange)
            {
                Character target = currLevel.PlayerUnits.ContainsKey(targetPos) ? currLevel.PlayerUnits[targetPos] : currLevel.EnemyUnits[targetPos];
                System.Diagnostics.Debug.Print("Here.");
                if (BattleController.isValidAction(character.Attack, character, character.Pos, targetPos) && currLevel.calcDist(character.Pos, targetPos) <= character.AttackRange)
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
