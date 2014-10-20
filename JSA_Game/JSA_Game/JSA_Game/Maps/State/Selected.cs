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

            if (keyboard.IsKeyDown(Keys.M) && !level.ButtonPressed && !level.PlayerUnits[level.SelectedPos].MoveDisabled)
            {
                if (level.PlayerUnits.ContainsKey(level.Cursor.CursorPos))
                {
                    level.toggleMoveRange(true, level.Cursor.CursorPos, level.PlayerUnits[level.Cursor.CursorPos].Movement);
                    level.State = LevelState.Movement;
                }
            }

            if (keyboard.IsKeyDown(Keys.A) && !level.ButtonPressed && !level.PlayerUnits[level.SelectedPos].ActionDisabled)
            {
                //Scan and mark potential targets
                level.scanForTargets(true, level.SelectedPos, level.PlayerUnits[level.SelectedPos].AttackRange);
                level.State = LevelState.Action;
            }

            //Test AI for player unit
            else if (keyboard.IsKeyDown(Keys.K) && !level.ButtonPressed)
            {
                level.PlayerUnits[level.Cursor.CursorPos].AI.move();
                System.Diagnostics.Debug.Print("Player AI moved");
                foreach (KeyValuePair<Vector2, Character> c in level.PlayerUnits)
                {
                    level.Cursor.CursorPos = c.Key;
                }
            }

        }
    }
}
