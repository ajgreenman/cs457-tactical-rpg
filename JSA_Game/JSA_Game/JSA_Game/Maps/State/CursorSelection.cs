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

            //Listen for input to move cursor
            level.Cursor.moveCursor(gameTime);

            if (keyboard.IsKeyDown(Keys.Z) && !level.ButtonPressed)
            {
                //Selecting a unit. 
                if (level.Board[(int)level.Cursor.CursorPos.X, (int)level.Cursor.CursorPos.Y].Occupant != null)
                {
                    int x = (int)level.Cursor.CursorPos.X;
                    int y = (int)level.Cursor.CursorPos.Y;
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
