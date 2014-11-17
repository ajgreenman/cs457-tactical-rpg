using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using JSA_Game.Maps;

namespace JSA_Game.Maps.State
{
    class Selected
    {
        private static Level lvl;
        public static void update(Level level, GameTime gameTime)
        {
            lvl = level;
            KeyboardState keyboard = Keyboard.GetState(PlayerIndex.One);
            
            if (keyboard.IsKeyDown(Keys.X) && !level.ButtonPressed)
            {
                level.State = LevelState.CursorSelection;
                level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].IsSelected = false;
            }

            else if (keyboard.IsKeyDown(Keys.M) && !level.ButtonPressed && !level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant.MoveDisabled)
            {
                Vector2 sourcePos = new Vector2(level.Cursor.CursorPos.X + level.ShowStartX, level.Cursor.CursorPos.Y + level.ShowStartY);
                if (level.Board[(int)sourcePos.X, (int)sourcePos.Y].Occupant != null)
                {
                   
                    level.toggleMoveRange(true, sourcePos, level.Board[(int)sourcePos.X, (int)sourcePos.Y].Occupant.Movement);
                    level.State = LevelState.Movement;
                }
            }

            else if (keyboard.IsKeyDown(Keys.A) && !level.ButtonPressed && !level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant.ActionDisabled)
            {
                //Scan and mark potential targets
                lvl.SelectedAction = level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant.Attack;
                level.scanForTargets(true, level.SelectedPos, level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant.Attack.Range);
                level.State = LevelState.Action;
            }

           // else if(level.HUD.

            

        }

        public static void setAction(PerformedType type, int index)
        {
            Character c = lvl.Board[(int)lvl.SelectedPos.X, (int)lvl.SelectedPos.Y].Occupant;
             switch (type)
            {
                 case PerformedType.Attack:
                    if (!c.ActionDisabled)
                    {
                        lvl.SelectedAction = c.Attack;
                        lvl.scanForTargets(true, lvl.SelectedPos, c.Attack.Range);
                        //if (lvl.SelectedAction.Aoe) //If the action is an aoe action
                       // {
                            //Show aoe range
                            lvl.scanForTargets(true, lvl.SelectedPos, lvl.SelectedAction.AoeRange, true);
                       // }
                        lvl.State = LevelState.Action;
                        
                    }
                    break;
                 case PerformedType.Ability:
                    if (!c.ActionDisabled)
                    {                      
                        lvl.SelectedAction = c.Actions[index];

                        if (lvl.PrevselectedAction != null)
                        {
                            if (!lvl.PrevselectedAction.Name.Equals(lvl.SelectedAction))
                            {
                                lvl.scanForTargets(false, lvl.SelectedPos, lvl.PrevselectedAction.Range);
                            }
                        }


                        lvl.scanForTargets(true, lvl.SelectedPos, c.Actions[index].Range);
                        System.Diagnostics.Debug.Print("Action selected was " + lvl.SelectedAction.Name);
                        if (lvl.SelectedAction.Aoe) //If the action is an aoe action
                        {
                            //Show aoe range
                            lvl.scanForTargets(true, lvl.SelectedPos, lvl.SelectedAction.AoeRange, true);
                        }
                        lvl.State = LevelState.Action;
                        lvl.PrevselectedAction = new Battle_Controller.Action(lvl.SelectedAction.Name, lvl.SelectedAction.Description, lvl.SelectedAction.ActionEffect,
                            lvl.SelectedAction.StatCost, lvl.SelectedAction.Type, lvl.SelectedAction.IgnoreEnemyStats, lvl.SelectedAction.Friendly, lvl.SelectedAction.Aoe,
                            lvl.SelectedAction.PowerMultiplier, lvl.SelectedAction.Cost, lvl.SelectedAction.Range, lvl.SelectedAction.AoeRange);
                        
                    }
                    break;
                 case PerformedType.Item:
                    lvl.SelectedAction = c.Inventory[index].Action;
                    lvl.scanForTargets(true, lvl.SelectedPos, c.Inventory[index].Action.Range);
                    break;

                 case PerformedType.Move:
                     
                    Vector2 sourcePos = new Vector2(lvl.Cursor.CursorPos.X + lvl.ShowStartX, lvl.Cursor.CursorPos.Y + lvl.ShowStartY);
                    if (c != null && !c.MoveDisabled)
                    {
                        lvl.toggleMoveRange(true, sourcePos, lvl.Board[(int)sourcePos.X, (int)sourcePos.Y].Occupant.Movement);
                        lvl.State = LevelState.Movement;
                    }
                    break;
                     

            }
        
        }
    }
}
 