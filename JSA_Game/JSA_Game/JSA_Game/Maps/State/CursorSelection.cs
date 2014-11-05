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
    static class CursorSelection
    {

        public static void update(Level level, GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState(PlayerIndex.One);


            if (level.MoveTimeElapsed >= level.MoveDelay) 
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

            if (keyboard.IsKeyDown(Keys.Z) && !level.ButtonPressed)
            {
                //Selecting a unit. 
                int x = (int)level.Cursor.CursorPos.X + level.ShowStartX;
                int y = (int)level.Cursor.CursorPos.Y + level.ShowStartY;
                if (level.Board[x, y].Occupant != null)
                {
                    level.SelectedPos = new Vector2(x, y);
                    level.State = LevelState.Selected;
                    level.Board[x, y].IsSelected = true;

                    //Send HUD character info
                    level.HUD.characterSelect(level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant);
                }
            }

            //End turn
            else if (keyboard.IsKeyDown(Keys.E) && !level.ButtonPressed)
            {
                level.PlayerTurn = TurnState.Enemy;
                System.Diagnostics.Debug.Print("Enemy's turn");
            }
        }
    }
}
