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
            level.Cursor.moveCursor(gameTime, level);

            //Confirm attack
            if (keyboard.IsKeyDown(Keys.Z) && !level.ButtonPressed)
            {
                System.Diagnostics.Debug.Print("Confirmed attack.");
                if (level.EnemyUnits.ContainsKey(level.Cursor.CursorPos) && level.Board[(int)level.Cursor.CursorPos.X, (int)level.Cursor.CursorPos.Y].IsSelected)
                {
                    Character c = level.PlayerUnits[level.SelectedPos];
                    Character e = level.EnemyUnits[level.Cursor.CursorPos];

                    if (BattleController.isValidAction(c.Actions[0], c, level.SelectedPos, level.Cursor.CursorPos))
                    {
                        System.Diagnostics.Debug.Print("Enemy HP is " + e.CurrHp);
                        BattleController.performAction(c.Actions[0], c, e);
                        System.Diagnostics.Debug.Print("Enemy HP now is " + e.CurrHp);
                    }

                    if (level.EnemyUnits[level.Cursor.CursorPos].CurrHp < 1)
                    {
                        level.Board[(int)level.Cursor.CursorPos.X, (int)level.Cursor.CursorPos.Y].IsOccupied = false;
                        level.EnemyUnits.Remove( level.Cursor.CursorPos);
                    }
                    level.PlayerUnits[level.SelectedPos].ActionDisabled = true;
                    level.scanForTargets(false, level.SelectedPos, level.PlayerUnits[level.SelectedPos].Attack.Range);
                    level.State = LevelState.CursorSelection;

                    //Check for win
                    if (level.EnemyUnits.Count <= 0)
                    {
                        System.Diagnostics.Debug.Print("Player Won!");
                    }
                }
            }
            else if (keyboard.IsKeyDown(Keys.X) && !level.ButtonPressed)
            {
                level.State = LevelState.CursorSelection;
                level.scanForTargets(false, level.SelectedPos, level.PlayerUnits[level.SelectedPos].Attack.Range);
            }

        }
    }
}
