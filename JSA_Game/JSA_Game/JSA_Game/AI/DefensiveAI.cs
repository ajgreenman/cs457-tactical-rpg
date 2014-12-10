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
    class DefensiveAI : iAI
    {
        private Level currLevel;
        private Boolean friendly;
        private const int FRIENDLY_CHANCE = 80;

        Character character;
        Vector2 targetPos;

        // AI knows what character it is and its surroundings.
        public DefensiveAI(Character c, Level currentLevel)
        {
            character = c;
            currLevel = currentLevel;
            targetPos = new Vector2(-1, -1);
            performFriendly();
        }

        public void move(GameTime gameTime)
        {
            //Picks closest target
            int dist;
            int shortestDist = 64;
            targetPos = new Vector2(-1, -1);
            ArrayList targetList;
            if (friendly)
            {
                targetList = getFriendlyTargets();
            }
            else
            {
                targetList = getEnemyTargets();
            }
            foreach (Character t in targetList)
            {
                dist = AStar.calcDist(character.Pos, t.Pos);
                if (dist < shortestDist && dist <= character.Movement)
                {
                    shortestDist = dist;
                    targetPos = t.Pos;
                }
            }

            //Move towards target if found
            if (!targetPos.Equals(new Vector2(-1, -1)))
            {
                currLevel.moveUnit(gameTime, character.Pos, targetPos, true, true);
            }
            character.MoveDisabled = true;
        }


        public void action()
        {
            if (!targetPos.Equals(new Vector2(-1, -1)))
            {
                if (AStar.calcDist(character.Pos, targetPos) <= character.Attack.Range)
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
                        Random rng = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                        int rand = rng.Next(0, count);

                        if (Maps.AStar.calcDist(character.Pos, targetPos) <= character.Actions[rand].Range)
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

        private void performFriendly()
        {
            if (!hasFriendlyMove())
            {
                friendly = false;
                return;
            }

            Random rand = new Random();
            int value = rand.Next(0, 100);

            friendly = value <= FRIENDLY_CHANCE;
        }

        private ArrayList getFriendlyTargets()
        {
            if (character.IsEnemy)
            {
                return currLevel.EUnits;
            }
            else
            {
                return currLevel.PUnits;
            }
        }

        private ArrayList getEnemyTargets()
        {
            if (character.IsEnemy)
            {
                return currLevel.PUnits;
            }
            else
            {
                return currLevel.EUnits;
            }
        }

        private Boolean hasFriendlyMove()
        {
            foreach (Battle_Controller.Action action in character.Actions)
            {
                if (action.Friendly)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
