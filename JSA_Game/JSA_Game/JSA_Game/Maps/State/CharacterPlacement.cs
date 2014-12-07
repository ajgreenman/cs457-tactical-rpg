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
using GameStateManagement;
using JSA_Game.Battle_Controller;
using JSA_Game.Maps;
using JSA_Game.Screens;

namespace JSA_Game.Maps.State
{
    class CharacterPlacement
    {
        public static void update(Level level, ScreenManager screenManager,GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState(PlayerIndex.One);
            Vector2 cursorPos = new Vector2(level.Cursor.CursorPos.X + level.ShowStartX, level.Cursor.CursorPos.Y + level.ShowStartY);

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

            int x = (int)level.Cursor.CursorPos.X + level.ShowStartX;
            int y = (int)level.Cursor.CursorPos.Y + level.ShowStartY;

            if (keyboard.IsKeyDown(Keys.Z) && !level.ButtonPressed && level.Board[x,y].Occupant == null)
            {
                //Selecting a highlighted space. 
                
                if (level.Board[x, y].HlState == HighlightState.MOVE)
                {
                    screenManager.AddScreen(new CharacterListScreen(screenManager.GraphicsDevice, Game1.getPlayerChars(), "right", level, new Vector2(x,y)), null);
                    //System.Diagnostics.Debug.Print("Placement state character Class: " + selectedChar.ClassName);
                    //level.State = LevelState.CursorSelection;
                }
            }
            if (keyboard.IsKeyDown(Keys.X) && !level.ButtonPressed)
            {
                //Selecting a highlighted space. 
               // int x = (int)level.Cursor.CursorPos.X + level.ShowStartX;
               // int y = (int)level.Cursor.CursorPos.Y + level.ShowStartY;
                if (level.Board[x, y].Occupant != null)
                {
                    System.Diagnostics.Debug.Print("" + level.PUnits.Count);
                    level.PUnits.Remove(level.Board[x, y].Occupant);
                    System.Diagnostics.Debug.Print("" + level.PUnits.Count);
                    level.Board[x, y].Occupant = null;
                    level.Board[x, y].IsOccupied = false;
                    level.PlayerUnitCount--;
                    System.Diagnostics.Debug.Print("Placement state: Removing unit");

                    //System.Diagnostics.Debug.Print("Placement state character Class: " + selectedChar.ClassName);
                    //level.State = LevelState.CursorSelection;
                }
            }
        }
    }
}