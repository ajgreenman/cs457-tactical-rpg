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
        public static void update(Level level, GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState(PlayerIndex.One);

            if (keyboard.IsKeyDown(Keys.X) && !level.ButtonPressed)
            {
                level.State = LevelState.CursorSelection;
                level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].IsSelected = false;
            }

            else if (keyboard.IsKeyDown(Keys.M) && !level.ButtonPressed && !level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant.MoveDisabled)
            {
                if (level.Board[(int)level.Cursor.CursorPos.X, (int)level.Cursor.CursorPos.Y].Occupant != null)
                {
                    level.toggleMoveRange(true, level.Cursor.CursorPos, level.Board[(int)level.Cursor.CursorPos.X, (int)level.Cursor.CursorPos.Y].Occupant.Movement);
                    level.State = LevelState.Movement;
                }
            }

            else if (keyboard.IsKeyDown(Keys.A) && !level.ButtonPressed && !level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant.ActionDisabled)
            {
                //Scan and mark potential targets
                level.scanForTargets(true, level.SelectedPos, level.Board[(int)level.SelectedPos.X, (int)level.SelectedPos.Y].Occupant.Actions[0].Range);
                level.State = LevelState.Action;
            }

            

        }
    }
}
