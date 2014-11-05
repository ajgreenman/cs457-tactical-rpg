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
using JSA_Game.Battle_Controller;
using JSA_Game.Maps;

namespace JSA_Game.Maps.State
{
    class ActionState
    {
        public static void update(Level level, GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState(PlayerIndex.One);

            if (level.MoveTimeElapsed >= level.MoveDelay)   // A unit is selected
            {
                //Scroll board if necessary
                if (keyboard.IsKeyDown(Keys.Left) && level.Cursor.CursorPos.X == 1 && level.ShowStartX != 0)
                {
                    level.ShowStartX--;
                }
                else if (keyboard.IsKeyDown(Keys.Right) && level.Cursor.CursorPos.X == level.NumTilesShowing - 2 && level.ShowStartX + level.NumTilesShowing != level.BoardWidth)
                {
                    level.ShowStartX++;
                }
                else if (keyboard.IsKeyDown(Keys.Up) && level.Cursor.CursorPos.Y == 1 && level.ShowStartY != 0)
                {
                    level.ShowStartY--;
                }
                else if (keyboard.IsKeyDown(Keys.Down) && level.Cursor.CursorPos.Y == level.NumTilesShowing - 2 && level.ShowStartY + level.NumTilesShowing != level.BoardHeight)
                {
                    level.ShowStartY++;
                }
                else if ((keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.Down))
                    && level.Cursor.CursorPos.X != level.ShowStartX + level.NumTilesShowing && level.Cursor.CursorPos.Y != level.ShowStartY + level.NumTilesShowing)
                {
                    //Listen for input to move cursor
                    level.Cursor.moveCursor(gameTime);
                }

                level.MoveTimeElapsed = 0;
            }

            //Confirm attack
            if (keyboard.IsKeyDown(Keys.Z) && !level.ButtonPressed)
            {
                int x = (int)level.Cursor.CursorPos.X + level.ShowStartX;
                int y = (int)level.Cursor.CursorPos.Y + level.ShowStartY;
                if (level.Board[x, y].Occupant != null)
                {
                    if (level.Board[x, y].Occupant.IsEnemy && level.Board[x, y].IsSelected)
                    {
                        JSA_Game.Battle_Controller.Action action = level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant.Attack;
                        level.attackTarget(level.SelectedPos, new Vector2(x,y), action);

                        level.State = LevelState.CursorSelection;
                    }
                }
            }
            else if (keyboard.IsKeyDown(Keys.X) && !level.ButtonPressed)
            {
                level.State = LevelState.CursorSelection;
                level.scanForTargets(false, level.SelectedPos, level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant.Attack.Range);
            }
        }
    }
}
