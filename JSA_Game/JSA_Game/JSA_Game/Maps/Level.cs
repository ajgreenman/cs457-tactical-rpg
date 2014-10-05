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
using JSA_Game.HUD;
using JSA_Game.Battle_Controller;

namespace JSA_Game.Maps
{
    class Level
    {

        const int TILE_IMAGE_COUNT = 1;
        const int HIGHLIGHT_IMAGE_COUNT = 2;


        int boardWidth;
        public int BoardWidth
        {
            get { return boardWidth; }
            set { boardWidth = value; }
        }

        int boardHeight;
        public int BoardHeight
        {
            get { return boardHeight; }
            set { boardHeight = value; }
        }

        private Tile[,] board;
        public Tile[,] Board
        {
            get { return board; }
            set { board = value; }
        }

        Boolean buttonPressed;
        Boolean selected = false;
        Vector2 selectedPos;

        //Cursor Variables
        Cursor cursor;

        //Timing variables
        float moveDelay = 100;
        float moveTimeElapsed;

        //Image Data Structures
        Texture2D[] tileImages;
        Texture2D[] highlightImages;

        //HUD
        HUD_Controller HUD;

        LevelState state;
        TurnState playerTurn;


        int maxPlayerUnits, MaxEnemyUnits, playerUnitCount, enemyUnitCount;

        Dictionary<Vector2, Character> playerUnits, enemyUnits;
        Dictionary<String, Texture2D> characterImages;

        public Dictionary<Vector2, Character> PlayerUnits
        {
            get { return playerUnits; }
            set { playerUnits = value; }
        }
        public Dictionary<Vector2, Character> EnemyUnits
        {
            get { return enemyUnits; }
            set { enemyUnits = value; }
        }


        /// <summary>
        /// Creates a level and initializes variables
        /// </summary>
        /// <param name="width">Width of the board</param>
        /// <param name="height">Height of the board</param>
        /// <param name="numPlayerUnits">Number of player units to place on the board</param>
        /// <param name="numEnemyUnits">Number of enemy units to place on the board</param>

        public Level(int width, int height, int numPlayerUnits, int numEnemyUnits)
        {
            boardWidth = width;
            boardHeight = height;
            board = new Tile[width, height];

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    board[i, j] = new Tile();
                }
            }

            state = LevelState.CursorSelection;
            playerTurn = TurnState.Player;

            playerUnitCount = 0;
            enemyUnitCount = 0;
            maxPlayerUnits = numPlayerUnits;
            MaxEnemyUnits = numEnemyUnits;
            buttonPressed = selected = false;
            cursor = new Cursor();

            playerUnits = new Dictionary<Vector2, Character>();
            enemyUnits = new Dictionary<Vector2, Character>();

            characterImages = new Dictionary<string, Texture2D>();
            tileImages = new Texture2D[TILE_IMAGE_COUNT];
            highlightImages = new Texture2D[HIGHLIGHT_IMAGE_COUNT];

            HUD = new HUD_Controller();
        }


        /// <summary>
        /// Adds a unit to the board.
        /// </summary>
        /// <param name="allyFlag">Flag determining if the unit is a player or enemy unit.  1 = player, 0 = enemy</param>
        /// <param name="unit">Unit to add to the level</param>
        /// <param name="xPos">X position of the location to place the unit</param>
        /// <param name="yPos">Y position of the location to place the unit</param>
        public void addUnit(int allyFlag, Character unit, Vector2 pos)
        {
            int xPos = (int)pos.X;
            int yPos = (int)pos.Y;
            unit.IsEnemy = allyFlag == 1 ? false : true;
            if (!unit.IsEnemy && playerUnitCount != maxPlayerUnits)
            {
                playerUnitCount++;
                board[xPos, yPos].IsOccupied = true;
                playerUnits.Add(new Vector2(xPos, yPos), unit);
            }

            else if (unit.IsEnemy && enemyUnitCount != MaxEnemyUnits)
            {
                enemyUnitCount++;
                board[xPos, yPos].IsOccupied = true;
                enemyUnits.Add(new Vector2(xPos, yPos), unit);
            }
            else
            {
                System.Diagnostics.Debug.Print("Level.addUnit() : Not enough space in level to place unit.");
            }
        }


        /// <summary>
        /// Moves a unit at the designated x and y coordinates in a specified direction
        /// </summary>
        /// <param name="pos">Position of the selected unit</param>
        /// <param name="dir">Direction to make the unit move: 'l'=left, 'r'=right, 'u'=up, 'd'=down</param>
        public void moveUnit(Vector2 pos, char dir)
        {
            int xPos = (int)pos.X;
            int yPos = (int)pos.Y;

            Character c = playerUnits[pos];
            if (dir == 'l' && xPos > 0)
            {
                playerUnits.Add(new Vector2(xPos - 1, yPos), c);
                board[xPos, yPos].IsOccupied = false;
                board[xPos - 1, yPos].IsOccupied = true;
                playerUnits.Remove(pos);
            }
            else if (dir == 'r' && xPos < boardWidth - 1)
            {
                playerUnits.Add(new Vector2(xPos + 1, yPos), c);
                board[xPos, yPos].IsOccupied = false;
                board[xPos + 1, yPos].IsOccupied = true;
                playerUnits.Remove(pos);
            }
            else if (dir == 'u' && yPos > 0)
            {
                playerUnits.Add(new Vector2(xPos, yPos - 1), c);
                board[xPos, yPos].IsOccupied = false;
                board[xPos, yPos - 1].IsOccupied = true;
                playerUnits.Remove(pos);
            }
            else if (dir == 'd' && yPos < boardHeight - 1)
            {
                playerUnits.Add(new Vector2(xPos, yPos + 1), c);
                board[xPos, yPos].IsOccupied = false;
                board[xPos, yPos + 1].IsOccupied = true;
                playerUnits.Remove(pos);
            }
        }


        /// <summary>
        /// Restores a player character's position if a move is cancelled
        /// </summary>
        /// <param name="former">Previous position</param>
        /// <param name="current">Current position</param>
        public void undoMove(Vector2 former, Vector2 current)
        {
            if (!former.Equals(current))
            {
                playerUnits.Add(former, playerUnits[current]);
                playerUnits.Remove(current);
                board[(int)current.X, (int)current.Y].IsOccupied = false;
                board[(int)former.X, (int)former.Y].IsOccupied = true;
                toggleMoveRange(false, former, playerUnits[former].Movement);
            }
        }


        /// <summary>
        /// Toggles between showing and hiding the movement range of a character.
        /// </summary>
        /// <param name="show">Boolean determining whether to show or hide the range</param>
        /// <param name="pos">Position of the selected unit</param>
        /// <param name="move">The movement stat of the selected unit</param>
        public void toggleMoveRange(Boolean show, Vector2 pos, int move)
        {
            board[(int)pos.X, (int)pos.Y].IsHighlighted = show;
            toggleMoveRange(show, (int)pos.X, (int)pos.Y, move);
        }

        /// <summary>
        /// Helper method to recursively find spaces on the board that
        /// the selected character can move to.
        /// </summary>
        /// <param name="show">Boolean determining whether to show or hide the range</param>
        /// <param name="x">X position of the selected unit</param>
        /// <param name="y">Y position of the selected unit</param>
        /// <param name="remMove">The remaining movement of the unit</param>
        private void toggleMoveRange(Boolean show, int x, int y, int remMove)
        {

            if (remMove <= 0) return;

            if (x > 0)
            {
                if ((show && !board[x - 1, y].IsOccupied) || !show)
                {
                    board[x - 1, y].IsHighlighted = show;
                    toggleMoveRange(show, x - 1, y, remMove - 1);
                }
            }
            if (x < boardWidth - 1)
            {
                if ((show && !board[x + 1, y].IsOccupied) || !show)
                {
                    board[x + 1, y].IsHighlighted = show;
                    toggleMoveRange(show, x + 1, y, remMove - 1);
                }
            }
            if (y > 0)
            {
                if ((show && !board[x, y - 1].IsOccupied) || !show)
                {
                    board[x, y - 1].IsHighlighted = show;
                    toggleMoveRange(show, x, y - 1, remMove - 1);
                }
            }
            if (y < boardHeight - 1)
            {
                if ((show && !board[x, y + 1].IsOccupied) || !show)
                {
                    board[x, y + 1].IsHighlighted = show;
                    toggleMoveRange(show, x, y + 1, remMove - 1);
                }
            }
        }

        /// <summary>
        /// Loads content for use in the level.
        /// </summary>
        /// <param name="content">ContentManager sent from the main class</param>
        public void loadContent(ContentManager content)
        {
            tileImages[0] = content.Load<Texture2D>("grass_tile");
            highlightImages[0] = content.Load<Texture2D>("no_highlight");
            highlightImages[1] = content.Load<Texture2D>("blue_highlight");

            foreach (KeyValuePair<Vector2, Character> c in playerUnits)
            {
                System.Diagnostics.Debug.Print("Created a player unit");
                characterImages.Add(c.Value.Texture, content.Load<Texture2D>(c.Value.Texture));
            }

            foreach (KeyValuePair<Vector2, Character> c in enemyUnits)
            {
                System.Diagnostics.Debug.Print("Created an enemy unit");
                characterImages.Add(c.Value.Texture, content.Load<Texture2D>(c.Value.Texture));
            }

            cursor.loadContent(content);
            HUD.LoadContent(content);
        }


        /// <summary>
        /// Updates the level based on user and level events.
        /// </summary>
        /// <param name="gameTime">GameTime sent from main class</param>
        public void update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState(PlayerIndex.One);

            //Animate cursor
            cursor.animate(gameTime);

            //Listen for input to move cursor
            cursor.moveCursor(gameTime, this, selected);

            if (playerTurn == TurnState.Player)
            {
                if (keyboard.IsKeyDown(Keys.Z) && !buttonPressed)
                {

                    if (state == LevelState.CursorSelection)
                    {
                        //Selecting a unit.  Shows the movement range.
                        if ((playerUnits.ContainsKey(cursor.CursorPos) || enemyUnits.ContainsKey(cursor.CursorPos)) && !selected)
                        {
                            state = LevelState.Selected;
                            selected = true;
                            //Send HUD character info
                            Character c;
                            if (playerUnits.ContainsKey(cursor.CursorPos))
                                c = playerUnits[cursor.CursorPos];
                            else
                                c = enemyUnits[cursor.CursorPos];

                            //Send c to HUD
                            HUD.characterSelect(c);

                            selectedPos = new Vector2(cursor.CursorPos.X, cursor.CursorPos.Y);

                            /*
                            if (playerUnits.ContainsKey(cursor.CursorPos))
                            {
                                toggleMoveRange(true, cursor.CursorPos, playerUnits[cursor.CursorPos].Movement);
                            }
                             */
                        }
                    }
                    //If already selected, confirm move and hide movement range.
                    else if (state == LevelState.Selected)
                    {
                        selected = false;

                        if (playerUnits.ContainsKey(selectedPos))
                        {
                            toggleMoveRange(false, cursor.CursorPos, playerUnits[selectedPos].Movement);
                        }
                        cursor.CursorPos = new Vector2(selectedPos.X, selectedPos.Y);
                        state = LevelState.CursorSelection;
                    }
                }
                

                //Unselect button.  Undo's a move if the selected unit moved.
                if (keyboard.IsKeyDown(Keys.X) && state==LevelState.Selected && !buttonPressed)
                {
                    selected = false;
                    state = LevelState.CursorSelection;
                    if (playerUnits.ContainsKey(selectedPos))
                    {
                        undoMove(cursor.CursorPos, selectedPos);
                        toggleMoveRange(false, cursor.CursorPos, playerUnits[cursor.CursorPos].Movement);
                        selectedPos = new Vector2(cursor.CursorPos.X, cursor.CursorPos.Y);
                    }
                }
                HUD.Hidden = state != LevelState.CursorSelection;
                moveTimeElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (selected && moveTimeElapsed >= moveDelay)   // A unit is selected
                {
                    //Move player character   selectedPos keeps track of current location, cursorPos keeps track of original location.
                    //moveUnit method used a character to determine direction (l = left, r = right, u = up, d = down)
                    if (playerUnits.ContainsKey(selectedPos))
                    {

                        if (keyboard.IsKeyDown(Keys.Left) && selectedPos.X > 0 && board[(int)(selectedPos.X - 1), (int)selectedPos.Y].IsHighlighted)
                        {
                            moveUnit(selectedPos, 'l');
                            selectedPos.X--;
                        }
                        else if (keyboard.IsKeyDown(Keys.Right) && selectedPos.X < boardWidth - 1 && board[(int)(selectedPos.X + 1), (int)selectedPos.Y].IsHighlighted)
                        {
                            moveUnit(selectedPos, 'r');
                            selectedPos.X++;
                        }
                        else if (keyboard.IsKeyDown(Keys.Up) && selectedPos.Y > 0 && board[(int)selectedPos.X, (int)(selectedPos.Y - 1)].IsHighlighted)
                        {
                            moveUnit(selectedPos, 'u');
                            selectedPos.Y--;
                        }
                        else if (keyboard.IsKeyDown(Keys.Down) && selectedPos.Y < boardHeight - 1 && board[(int)selectedPos.X, (int)(selectedPos.Y + 1)].IsHighlighted)
                        {
                            moveUnit(selectedPos, 'd');
                            selectedPos.Y++;
                        }
                    }
                    moveTimeElapsed = 0;
                }


                if (state == LevelState.Selected)
                {
                    if (keyboard.IsKeyDown(Keys.M) && !buttonPressed)
                    {
                        if (playerUnits.ContainsKey(cursor.CursorPos))
                        {
                            toggleMoveRange(true, cursor.CursorPos, playerUnits[cursor.CursorPos].Movement);
                        }
                    }

                    if (keyboard.IsKeyDown(Keys.A) && !buttonPressed)
                    {
                        state = LevelState.Action;
                    }
                }



                if (state == LevelState.Action)
                {
                    if (keyboard.IsKeyDown(Keys.K) && !buttonPressed)
                    {
                        Character c = playerUnits[cursor.CursorPos];
                        Vector2 target = new Vector2(cursor.CursorPos.X + 1, cursor.CursorPos.Y);
                        Character enemy = enemyUnits[target];
                        if (BattleController.isValidAction(c.Actions[0], c, cursor.CursorPos, target))
                        {
                            System.Diagnostics.Debug.Print("Enemy HP is " + enemy.CurrHp);
                            BattleController.performAction(c.Actions[0], c, enemy);
                            System.Diagnostics.Debug.Print("Enemy HP now is " + enemy.CurrHp);
                        }

                        if (enemyUnits[target].CurrHp < 1){
                            board[(int)target.X, (int)target.Y].IsOccupied = false;
                            enemyUnits.Remove(target);
                        }

                        state = LevelState.Selected;

                    }
                  
                }



                //Prevents holding a button to continuously activate events
                if (keyboard.IsKeyUp(Keys.Z) || keyboard.IsKeyUp(Keys.X) || keyboard.IsKeyUp(Keys.K) || keyboard.IsKeyUp(Keys.M) || keyboard.IsKeyUp(Keys.A))
                {
                    buttonPressed = false;
                }
                if (keyboard.IsKeyDown(Keys.Z) || keyboard.IsKeyDown(Keys.X) || keyboard.IsKeyDown(Keys.K) || keyboard.IsKeyDown(Keys.M) || keyboard.IsKeyDown(Keys.A))
                {
                    buttonPressed = true;
                }
            }





        }


        /// <summary>
        /// Draws the level to the screen.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch from main class.</param>
        /// <param name="startw">Offset width for the start of the board</param>
        /// <param name="starth">Offset height for the start of the board</param>
        /// <param name="tileSize">Size of each tile on the board</param>
        public void draw(SpriteBatch spriteBatch, int startw, int starth, int tileSize)
        {
            //Draw board
            for (int i = 0; i < boardWidth; i++)
            {
                for (int j = 0; j < boardHeight; j++)
                {
                    spriteBatch.Draw(tileImages[0], new Rectangle(startw + tileSize * i, starth + tileSize * j, tileSize, tileSize), Color.White);

                    //Draws a semi-transparent tile to show available spaces for movement/attacking
                    if (board[i, j].IsHighlighted)
                    {
                        spriteBatch.Draw(highlightImages[1], new Rectangle(startw + tileSize * i, starth + tileSize * j, tileSize, tileSize), Color.White);
                    }
                }
            }

            //Draw characters
            foreach (KeyValuePair<Vector2, Character> c in playerUnits)
            {
                //System.Diagnostics.Debug.Print("Drawing player character at position " + c.Pos.X + "," + c.Pos.Y + ". Texture = " + c.Texture);
                spriteBatch.Draw(characterImages[c.Value.Texture], new Rectangle(startw + tileSize * (int)c.Key.X, starth + tileSize * (int)c.Key.Y, tileSize, tileSize), Color.White);

            }
            foreach (KeyValuePair<Vector2, Character> c in enemyUnits)
            {
                //System.Diagnostics.Debug.Print("Drawing enemy character at position " + c.Pos.X + "," + c.Pos.Y + ". Texture = " + c.Texture);
                spriteBatch.Draw(characterImages[c.Value.Texture], new Rectangle(startw + tileSize * (int)c.Key.X, starth + tileSize * (int)c.Key.Y, tileSize, tileSize), Color.White);
            }

            //Draw cursor on top of board.
            cursor.draw(spriteBatch, startw, starth, tileSize);

            //Draw HUD
            HUD.Draw(spriteBatch);
        }
    }
}