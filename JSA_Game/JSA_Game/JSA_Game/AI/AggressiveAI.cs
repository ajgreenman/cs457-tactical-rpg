using System;
using System.Collections;
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
        private Level currLevel;
        Character character;
        Vector2 targetPos;

        // AI knows what character it is and its surroundings.
        public AggressiveAI(Character c, Level currentLevel)
        {
            character = c;
            currLevel = currentLevel;
            targetPos = new Vector2(-1, -1);
        }

        public void move(GameTime gameTime)
        {

            //Picks closest target
            int dist;
            int shortestDist = 64;
            targetPos = new Vector2(-1, -1);
            ArrayList targetList = currLevel.PUnits.Contains(character) ? currLevel.EUnits : currLevel.PUnits;
            foreach (Character t in targetList)
            {
                dist = currLevel.calcDist(character.Pos, t.Pos);
                if (dist < shortestDist)
                {
                    shortestDist = dist;
                    targetPos = t.Pos;
                }

            }

            //Move towards target if found
            if (!targetPos.Equals(new Vector2(-1, -1)))
            {
                currLevel.moveUnit(gameTime, character.Pos, targetPos, true, false);
            }
        }


        public void action()
        {

            if (!targetPos.Equals(new Vector2(-1, -1)))
            {
                if (currLevel.calcDist(character.Pos, targetPos) <= character.Attack.Range)
                {
                    performMove(character, targetPos);
                }
                else
                {
                    int count = 0;
                    Battle_Controller.Action[] actions = new Battle_Controller.Action[4];
                    foreach (Battle_Controller.Action action in character.Actions)
                    {
                        if (!action.Friendly && action.Range > 1)
                        {
                            actions[count] = action;
                            count++;
                        }
                    }

                    if (count > 0)
                    {
                        Random rng = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
                        int rand = rng.Next(0, count);

                        if (currLevel.calcDist(character.Pos, targetPos) <= character.Actions[rand].Range)
                        {
                            Console.WriteLine("AggresiveAI move: " + character.Actions[rand].Name);
                            currLevel.attackTarget(character.Pos, targetPos, character.Actions[rand]);
                        }
                    }
                }
            }
        }

        public Level CurrLevel
        {
            get { return currLevel; }
            set { currLevel = value; }
        }

        private void performMove(Character c, Vector2 targetPos)
        {
            int[] actions = new int[6];
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i] = 0;
            }
            for (int j = 0; j < c.Actions.Length; j++)
            {
                if (!c.Actions[j].Friendly && c.Actions[j].Range > 0)
                {
                    actions[j] = 1;
                }
            }

            actions[4] = 1;
            int rand = 5;

            while (actions[rand] != 1)
            {
                Random rng = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                rand = rng.Next(0, 5);
            }
            Console.WriteLine(rand);
            if (rand == 4)
            {
                currLevel.attackTarget(c.Pos, targetPos, c.Attack);
            }
            else
            {
                currLevel.attackTarget(c.Pos, targetPos, c.Actions[rand]);
            }
        }
    }
}
