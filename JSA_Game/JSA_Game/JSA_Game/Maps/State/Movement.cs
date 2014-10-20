using System;
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
                if (level.PlayerUnits.ContainsKey(level.SelectedPos))
                {
                    level.toggleMoveRange(false, level.SelectedPos, level.PlayerUnits[level.SelectedPos].Movement);
                }
                level.PlayerUnits[level.SelectedPos].MoveDisabled = true;
                level.moveUnit(level.SelectedPos, level.Cursor.CursorPos, false);
                level.State = LevelState.CursorSelection;
                level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].IsSelected = false;
            }


            //Cancel button.  Undo's a move if the selected unit moved.
            if (keyboard.IsKeyDown(Keys.X) && !level.ButtonPressed)
            {
                level.State = LevelState.CursorSelection;
                level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].IsSelected = false;
                if (level.PlayerUnits.ContainsKey(level.SelectedPos))
                {
                    level.toggleMoveRange(false, level.SelectedPos, level.PlayerUnits[level.SelectedPos].Movement);
                    level.Cursor.CursorPos = new Vector2(level.SelectedPos.X, level.SelectedPos.Y);
                }
            }

            if (level.MoveTimeElapsed >= level.MoveDelay)   // A unit is selected
            {
                //Move player character
                //moveUnit method used a character to determine direction (l = left, r = right, u = up, d = down)
                if (level.PlayerUnits.ContainsKey(level.SelectedPos))
                {
                    //Cursor now selects move location and selectedPos keeps track of original position.  Refactoring needed.
                    int cX = (int)level.Cursor.CursorPos.X;
                    int cY = (int)level.Cursor.CursorPos.Y;
                    char dir = '0';
                    if (keyboard.IsKeyDown(Keys.Left) && cX > 0 && level.Board[cX - 1, cY].IsHighlighted && !level.Board[cX - 1, cY].IsOccupied)
                    {
                        dir = 'l';
                    }
                    else if (keyboard.IsKeyDown(Keys.Right) && cX < level.BoardWidth - 1 && level.Board[cX + 1, cY].IsHighlighted && (!level.Board[cX + 1, cY].IsOccupied))
                    {
                        dir = 'r';
                    }
                    else if (keyboard.IsKeyDown(Keys.Up) && cY > 0 && level.Board[cX, cY - 1].IsHighlighted && (!level.Board[cX, cY - 1].IsOccupied))
                    {
                        dir = 'u';
                    }
                    else if (keyboard.IsKeyDown(Keys.Down) && cY < level.BoardHeight - 1 && level.Board[cX, cY + 1].IsHighlighted && (!level.Board[cX, cY + 1].IsOccupied))
                    {
                        dir = 'd';
                    }

                    level.Cursor.moveCursorDir(dir);
                }
                level.MoveTimeElapsed = 0;
            }

        }
    }
}
