﻿using System;
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
                    lvl.SelectedAction = c.Attack;
                    lvl.scanForTargets(true, lvl.SelectedPos, c.Attack.Range);
                    lvl.State = LevelState.Action;
                    break;

                 case PerformedType.Ability:
                    lvl.SelectedAction = c.Actions[index];
                    lvl.scanForTargets(true, lvl.SelectedPos, c.Actions[index].Range);
                    lvl.State = LevelState.Action;
                    break;
                 //case PerformedType.Item:
                 //   lvl.SelectedAction = c.Inventory[index].Action;
                 //   break;

                 case PerformedType.Move:
                    Vector2 sourcePos = new Vector2(lvl.Cursor.CursorPos.X + lvl.ShowStartX, lvl.Cursor.CursorPos.Y + lvl.ShowStartY);
                    if (lvl.Board[(int)sourcePos.X, (int)sourcePos.Y].Occupant != null)
                    {
                        lvl.toggleMoveRange(true, sourcePos, lvl.Board[(int)sourcePos.X, (int)sourcePos.Y].Occupant.Movement);
                        lvl.State = LevelState.Movement;
                    }
                    break;
                     

            }
        
        }
    }
}
 