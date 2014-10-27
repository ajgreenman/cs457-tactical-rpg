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

            //Listen for input to move cursor
            level.Cursor.moveCursor(gameTime);

            //Confirm attack
            if (keyboard.IsKeyDown(Keys.Z) && !level.ButtonPressed)
            {
                if (level.Board[(int)level.Cursor.CursorPos.X, (int)level.Cursor.CursorPos.Y].Occupant != null)
                {
                    if (level.Board[(int)level.Cursor.CursorPos.X, (int)level.Cursor.CursorPos.Y].Occupant.IsEnemy && level.Board[(int)level.Cursor.CursorPos.X, (int)level.Cursor.CursorPos.Y].IsSelected)
                    {
                        JSA_Game.Battle_Controller.Action action = level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant.Attack;
                        level.attackTarget(level.SelectedPos, level.Cursor.CursorPos, action);

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
