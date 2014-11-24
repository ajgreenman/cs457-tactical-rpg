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
    class Movement
    {
        public static void update(Level level, GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState(PlayerIndex.One);

            //If already selected, confirm move and hide movement range.
            if (keyboard.IsKeyDown(Keys.Z))
            {
                if (level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant != null)
                {
                    level.toggleMoveRange(false, level.SelectedPos, level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant.Movement);
                }
                if (!level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant.IsEnemy)
                {
                    level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant.MoveDisabled = true;
                    Vector2 destination = new Vector2(level.Cursor.CursorPos.X + level.ShowStartX, level.Cursor.CursorPos.Y + level.ShowStartY);
                    level.moveUnit(gameTime, level.SelectedPos, destination, false, false);      
                    level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].IsSelected = false;
                }
                level.SelectedPos = new Vector2(level.Cursor.CursorPos.X + level.ShowStartX, level.Cursor.CursorPos.Y + level.ShowStartY);
                    
                level.State = LevelState.Selected;
                
            }


            //Cancel button.  Undo's a move if the selected unit moved.
            if (keyboard.IsKeyDown(Keys.X) && !level.ButtonPressed)
            {
                level.State = LevelState.CursorSelection;
                level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].IsSelected = false;
                if (level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant != null)
                {
                    level.toggleMoveRange(false, level.SelectedPos, level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant.Movement);
                    level.Cursor.CursorPos = new Vector2(level.SelectedPos.X-level.ShowStartX, level.SelectedPos.Y-level.ShowStartY);
                }
            }

            if (level.MoveTimeElapsed >= level.MoveDelay)   // A unit is selected
            {
                //Move player character
                //moveUnit method used a character to determine direction (l = left, r = right, u = up, d = down)
                if (level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant != null)
                {
                    //Cursor now selects move location and selectedPos keeps track of original position.
                    int cX = (int)level.Cursor.CursorPos.X + level.ShowStartX;
                    int cY = (int)level.Cursor.CursorPos.Y + level.ShowStartY;
                    char dir = '0';
                    if (keyboard.IsKeyDown(Keys.Left) && cX > 0 && level.Board[cX - 1, cY].HlState == HighlightState.MOVE && !level.Board[cX - 1, cY].IsOccupied)
                    {
                        if (level.Cursor.CursorPos.X == 1 && cX != 1)
                        {
                            level.ShowStartX--;
                        }
                        else
                        {
                            dir = 'l';
                        }
                    }
                    else if (keyboard.IsKeyDown(Keys.Right) && cX < level.BoardWidth - 1 && level.Board[cX + 1, cY].HlState == HighlightState.MOVE && (!level.Board[cX + 1, cY].IsOccupied))
                    {
                        if (level.ShowStartX + level.NumTilesShowing - 2 == cX && cY != level.BoardWidth - 3)
                        {
                            level.ShowStartX++;
                        }
                        else
                        {
                            dir = 'r';
                        }
                    }
                    else if (keyboard.IsKeyDown(Keys.Up) && cY > 0 && level.Board[cX, cY - 1].HlState == HighlightState.MOVE && (!level.Board[cX, cY - 1].IsOccupied))
                    {
                        if (level.Cursor.CursorPos.Y == 1 && cY != 1)
                        {
                            level.ShowStartY--;
                        }
                        else
                        {
                            dir = 'u';
                        }
                    }
                    else if (keyboard.IsKeyDown(Keys.Down) && cY < level.BoardHeight - 1 && level.Board[cX, cY + 1].HlState == HighlightState.MOVE && (!level.Board[cX, cY + 1].IsOccupied))
                    {
                        if (level.ShowStartY + level.NumTilesShowing - 2 == cY && cY != level.BoardHeight - 3)
                        {
                            level.ShowStartY++;
                        }
                        else
                        {
                            dir = 'd';
                        }
                    }

                    level.Cursor.moveCursorDir(dir);
                }
                level.MoveTimeElapsed = 0;
            }

        }
    }
}
