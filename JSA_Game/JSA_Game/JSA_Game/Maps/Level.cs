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
using JSA_Game.HUD;
using JSA_Game.Battle_Controller;
using JSA_Game.Maps.State;

namespace JSA_Game.Maps
{
    class Level
    {
        const int TILE_IMAGE_COUNT = 3;
        const int UTILITY_IMAGE_COUNT = 3;

        int boardWidth, boardHeight;
        int maxPlayerUnits, MaxEnemyUnits, playerUnitCount, enemyUnitCount;
        private Boolean buttonPressed;

        private Tile[,] board;

        private Vector2 selectedPos;

        //Cursor Variables
        private Cursor cursor;
       
        //Timing variables
        private float moveDelay = 100;
        private float moveTimeElapsed;
       
        //Image Data Structures
        Texture2D[] tileImages;
        Texture2D[] utilityImages;

        //HUD
        private HUD_Controller hud;
        

        //State
        private LevelState state;
        
        private TurnState playerTurn;
        

        Dictionary<Vector2, Character> playerUnits, enemyUnits;
        Dictionary<String, Texture2D> characterImages;


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


            //Make level more interesting for testing

            for (int i = 1; i < width - 1; i++)
            {
                if(i != width/2 && i != width/2-1)
                {
                    board[i, height / 2].IsOccupied = true;
                    board[i, height / 2].LandType = "water";
                    board[i, height / 2-1].IsOccupied = true;
                    board[i, height / 2-1].LandType = "water";
                }
            }

            //Very hardcoded
            board[width / 2+1, height / 2 - 2].IsOccupied = true;
            board[width / 2+1, height / 2 - 2].LandType = "water";
            board[width / 2+1, height / 2 - 3].IsOccupied = true;
            board[width / 2+1, height / 2 - 3].LandType = "water";
            board[width / 2, height / 2 - 3].IsOccupied = true;
            board[width / 2, height / 2 - 3].LandType = "water";
            board[width / 2-1, height / 2 - 3].IsOccupied = true;
            board[width / 2-1, height / 2 - 3].LandType = "water";

            board[width / 2 -2, height / 2 +1].IsOccupied = true;
            board[width / 2 -2, height / 2 +1].LandType = "water";
            board[width / 2 -2, height / 2 +2].IsOccupied = true;
            board[width / 2 -2, height / 2 +2].LandType = "water";
            board[width / 2-1, height / 2 +2].IsOccupied = true;
            board[width / 2-1, height / 2 +2].LandType = "water";
            board[width / 2 , height / 2 +2].IsOccupied = true;
            board[width / 2 , height / 2 +2].LandType = "water";

            board[width / 2+2, height / 2 + 2].IsOccupied = true;
            board[width / 2 + 2, height / 2 + 2].LandType = "stone_wall";
            board[width / 2 +2, height / 2 +3].IsOccupied = true;
            board[width / 2 +2, height / 2 + 3].LandType = "stone_wall";
            board[width / 2 + 2, height / 2 + 1].IsOccupied = true;
            board[width / 2 + 2, height / 2 + 1].LandType = "stone_wall";
            board[width / 2 + 2, height / 2 + 4].IsOccupied = true;
            board[width / 2 + 2, height / 2 + 4].LandType = "stone_wall";


            board[width / 2 - 3, height / 2 - 2].IsOccupied = true;
            board[width / 2 - 3, height / 2 - 2].LandType = "stone_wall";
            board[width / 2 - 3, height / 2 - 3].IsOccupied = true;
            board[width / 2 - 3, height / 2 - 3].LandType = "stone_wall";
            board[width / 2 - 3, height / 2 - 4].IsOccupied = true;
            board[width / 2 - 3, height / 2 - 4].LandType = "stone_wall";
            board[width / 2 - 3, height / 2 - 5].IsOccupied = true;
            board[width / 2 - 3, height / 2 - 5].LandType = "stone_wall";

            board[width / 2, height-1].IsOccupied = true;
            board[width / 2, height - 1].LandType = "stone_wall";
            board[width / 2 - 2, height - 2].IsOccupied = true;
            board[width / 2 - 2, height - 2].LandType = "stone_wall";
            
           
            
            /*  Wall off player
            board[0, 1].IsOccupied = true;
            board[0, 1].LandType = "stone_wall";

            board[1, 0].IsOccupied = true;
            board[1, 0].LandType = "stone_wall";
            */

            state = LevelState.CursorSelection;
            playerTurn = TurnState.Player;

            playerUnitCount = 0;
            enemyUnitCount = 0;
            maxPlayerUnits = numPlayerUnits;
            MaxEnemyUnits = numEnemyUnits;
            //buttonPressed = selected = false;
            buttonPressed = false;
            cursor = new Cursor();

            playerUnits = new Dictionary<Vector2, Character>();
            enemyUnits = new Dictionary<Vector2, Character>();

            characterImages = new Dictionary<string, Texture2D>();
            tileImages = new Texture2D[TILE_IMAGE_COUNT];
            utilityImages = new Texture2D[UTILITY_IMAGE_COUNT];

            hud = new HUD_Controller();
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
                unit.Pos = pos;
            }

            else if (unit.IsEnemy && enemyUnitCount != MaxEnemyUnits)
            {
                enemyUnitCount++;
                board[xPos, yPos].IsOccupied = true;
                enemyUnits.Add(new Vector2(xPos, yPos), unit);
                unit.Pos = pos;
            }
            else
            {
                System.Diagnostics.Debug.Print("Level.addUnit() : Not enough space in level to place unit.");
            }
        }



        //Moves unit given a path, provided by A*
        public void moveUnit(Vector2 startPos, Vector2 endPos, Boolean AImoved)
        {
            Stack path = findPath(startPos, endPos);
            //if path exists
            if (path.Count > 0)
            {
                int remMovement;
                Character unit;
                //stop variable stops movement for enemy units 1 early since
                //the postion of a computer's target is their actual position.
                int stop;
                Boolean isEnemy = enemyUnits.ContainsKey(startPos);
                if (isEnemy)
                {
                    unit = enemyUnits[startPos];
                    remMovement = unit.Movement;
                    stop = 1;
                }
                else
                {

                    unit = playerUnits[startPos];
                    remMovement = unit.Movement;
                    stop = AImoved ? 1 : 0;
                }

                //Starting position
                Vector2 pos = (Vector2)path.Pop();
                while (remMovement > 0 && path.Count > stop)
                {
                    int xPos = (int)pos.X;
                    int yPos = (int)pos.Y;
                    Character c;
                    Vector2 next;

                    if (isEnemy)
                    {
                        next = (Vector2)path.Pop();
                        c = enemyUnits[pos];
                        enemyUnits.Add(next, c);
                        enemyUnits.Remove(pos);
                    }
                    else
                    {
                        next = (Vector2)path.Pop();
                        c = playerUnits[pos];
                        playerUnits.Add(next, c);
                        playerUnits.Remove(pos); 
                    }
                    board[(int)pos.X, (int)pos.Y].IsOccupied = false;
                    board[(int)next.X, (int)next.Y].IsOccupied = true;
                    pos = next;
                    remMovement--;
                }
                unit.Pos = pos;
            }
        }


        /// <summary>
        /// Finds the shortest path between two positions using 
        /// the A* pathfinding algorithm.
        /// </summary>
        /// <param name="startPos">Starting position</param>
        /// <param name="endPos">Ending position</param>
        /// <returns>A stack containing the path in Vector2 objects, beginning with the 1st step to take</returns>
        public Stack findPath(Vector2 startPos, Vector2 endPos)
        {
            //Open list: potential tiles that are to be considered
            //Add all options to the list, ignoring impassible tiles
            ArrayList openList;
            
            //Once evaluated, drop from the list and add to closed list
            ArrayList closedList;

            //boolean to tell when destination is found
            Boolean found = false;

            //Maximum possible F score
            int maxFScore = boardWidth * boardHeight + boardWidth + boardHeight;

            openList = new ArrayList();
            closedList = new ArrayList();

            openList.Add(startPos);

            //System.Diagnostics.Debug.Print("Moving from (" + startPos.X + ", " + startPos.Y
              //  + ") to (" + endPos.X + ", " + endPos.Y + ")");
           // int count = 1;
            while (!found)
            {
                //System.Diagnostics.Debug.Print("Time # " + count + " going through");
                //count++;

                //Calculate F scores
                //F = G + H where G is movement cost and H is heuristic score
                //For this game, moving 1 tile will cost 1
                //So G is 1 + the parent's G score
                //The heuristic score is how far away the target is from the
                //examined tile. Simply how many tiles it is ignoring impassible tiles

                //Calculate
                int x, y;
                for (int i = 0; i < openList.Count; i++)
                {
                    Vector2 pos = (Vector2)openList[i];
                    x = (int)pos.X;
                    y = (int)pos.Y;
                    //System.Diagnostics.Debug.Print("Looking at (" + x + ", " + y + ")");
                    int h;

                    if (board[x, y].PathParent != null)
                    {
                        board[x, y].GScore = board[(int)board[x, y].PathParent.X,(int)board[x, y].PathParent.Y].GScore + 1;
                    }
                    else
                    {
                        board[x, y].GScore = 1;
                    }

                    h = calcDist(pos, endPos);

                    
                    board[x, y].FScore = board[x, y].GScore + h;
                    //System.Diagnostics.Debug.Print("G score is " + board[x, y].GScore);
                    //System.Diagnostics.Debug.Print("H score is " + h);
                    //System.Diagnostics.Debug.Print("F score is " + board[x, y].FScore);
                }

                //Choose the tile with the lowest F score in the open list
                int lowestFIndex = maxFScore; 

                for (int i = 0; i < openList.Count; i++)
                {
                    Vector2 pos = (Vector2)openList[i];
                    x = (int)pos.X;
                    y = (int)pos.Y;
                    if (board[x, y].FScore < lowestFIndex)
                    {
                        lowestFIndex = i;
                    }
                }

                //Save the position of best node 
                //Drop from open list and add to closed list.
                if (lowestFIndex == maxFScore)
                {
                    break;
                }
                Vector2 sPos = (Vector2)openList[lowestFIndex];

                //Stop if endPos is the selected node
                if (endPos.Equals(sPos))
                {
                    found = true; 
                }
                else
                {
                    closedList.Add(sPos);
                    openList.Remove(sPos);

                    //Scan tiles adjacent to selected tile
                    //Ignore those that are occupied

                    x = (int)sPos.X;
                    y = (int)sPos.Y;
                    Vector2 newTile = new Vector2(0, 0);
                    Boolean newFound = false;
                    ArrayList possibleNew = new ArrayList();

                    //If left is open
                    if ((x > 0 && !board[x - 1, y].IsOccupied) || (endPos.X == x - 1 && endPos.Y == y))
                    {
                        newTile = new Vector2(x - 1, y);
                        possibleNew.Add(newTile);
                        newFound = true;

                    }

                    //If right is open
                    if ((x < boardWidth - 1 && !board[x + 1, y].IsOccupied) || (endPos.X == x + 1 && endPos.Y == y))
                    {
                        newTile = new Vector2(x + 1, y);
                        possibleNew.Add(newTile);
                        newFound = true;
                    }

                    //If up is open
                    if ((y > 0 && !board[x, y - 1].IsOccupied) || (endPos.X == x && endPos.Y == y - 1))
                    {
                        newTile = new Vector2(x, y - 1);
                        possibleNew.Add(newTile);
                        newFound = true;
                    }

                    //If down is open
                    if ((y < boardHeight - 1 && !board[x, y + 1].IsOccupied) || (endPos.X == x && endPos.Y == y + 1))
                    {
                        newTile = new Vector2(x, y + 1);
                        possibleNew.Add(newTile);
                        newFound = true;
                    }

                    //If squares to be added are found
                    if (newFound)
                    {
                        //Check if new tile is in closed list (inefficient atm)
                        ArrayList removeList = new ArrayList();
                        //For each neighbor
                        foreach (Vector2 v in possibleNew)
                        {
                            //Check closed list
                            if(closedList.Contains(v))
                            {
                                foreach (Vector2 c in closedList)
                                {
                                    if (v.Equals(c))
                                    {
                                        //If current has lower G score
                                        if (board[x, y].GScore < board[(int)v.X, (int)v.Y].GScore)
                                        {
                                            //Update neighbor with current, lower G score 
                                            board[(int)v.X, (int)v.Y].GScore = board[x, y].GScore;

                                            //change neighbor parent to current node
                                            board[(int)v.X, (int)v.Y].PathParent = sPos;
                                        }

                                    }
                                }
                            }
                                //Check open list
                            else if(openList.Contains(v))
                            {
                                foreach (Vector2 c in openList)
                                {
                                    if (v.Equals(c))
                                    {
                                        //If current has lower G score
                                        if (board[x, y].GScore < board[(int)v.X, (int)v.Y].GScore)
                                        {
                                            //Update neighbor with current, lower G score 
                                            board[(int)v.X, (int)v.Y].GScore = board[x, y].GScore;

                                            //change neighbor parent to current node
                                            board[(int)v.X, (int)v.Y].PathParent = sPos;
                                        }

                                    }
                                }
                            }
                            //Neighbor is not in open or closed list
                            else
                            {
                                board[(int)v.X, (int)v.Y].PathParent = sPos;
                                openList.Add(v);
                            }
                        }        
                    }
                }
            }

            //When endPos is found
            //If no path is found, no move occurs
            Stack path = new Stack();
            if (found)
            {
                Vector2 lookingAt = endPos;
                // System.Diagnostics.Debug.Print("Path Backwards is......");
                while (!lookingAt.Equals(startPos))
                {
                    //System.Diagnostics.Debug.Print("(" + lookingAt.X + ", " + lookingAt.Y + ")");
                    path.Push(lookingAt);
                    lookingAt = board[(int)lookingAt.X, (int)lookingAt.Y].PathParent;
                }
                path.Push(startPos);
            }
            return path;
        }

        // Number of tiles from destination tile. H Score in A*
        public int calcDist(Vector2 pos, Vector2 target)
        {
            return (int)(Math.Abs(pos.X - target.X) + Math.Abs(pos.Y - target.Y));
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
            board[x, y].IsHighlighted = show;
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

        //Scan current location for attackable targets
        public void scanForTargets(Boolean show, Vector2 pos, int range)
        {
            board[(int)pos.X, (int)pos.Y].IsSelected = show;
            scanForTargets(show, (int)pos.X, (int)pos.Y, range);
        }

        private void scanForTargets(Boolean show, int x, int y, int range)
        {
            if (range <= 0) return;
            if (x > 0)
            {
                if ((show && enemyUnits.ContainsKey(new Vector2(x - 1, y))) || !show)
                {
                    board[x - 1, y].IsSelected = show;
                }
                scanForTargets(show, x - 1, y, range - 1);
            }
            if (x < boardWidth - 1)
            {
                if ((show && enemyUnits.ContainsKey(new Vector2(x + 1, y))) || !show)
                {
                    board[x + 1, y].IsSelected = show;
                }
                scanForTargets(show, x + 1, y, range - 1);
            }
            if (y > 0)
            {
                if ((show && enemyUnits.ContainsKey(new Vector2(x, y - 1))) || !show)
                {
                    board[x, y - 1].IsSelected = show;
                }
                scanForTargets(show, x, y - 1, range - 1);
            }
            if (y < boardHeight - 1)
            {
                if ((show && enemyUnits.ContainsKey(new Vector2(x, y + 1))) || !show)
                {
                    board[x, y + 1].IsSelected = show;
                }
                scanForTargets(show, x, y + 1, range - 1);
            }
        }
        /// <summary>
        /// Loads content for use in the level.
        /// </summary>
        /// <param name="content">ContentManager sent from the main class</param>
        public void loadContent(ContentManager content)
        {
            tileImages[0] = content.Load<Texture2D>("grass_tile");
            tileImages[1] = content.Load<Texture2D>("water");
            tileImages[2] = content.Load<Texture2D>("stone_wall");
            utilityImages[0] = content.Load<Texture2D>("no_highlight");
            utilityImages[1] = content.Load<Texture2D>("blue_highlight");
            utilityImages[2] = content.Load<Texture2D>("target_square");

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
            hud.LoadContent(content);
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

            if (playerTurn == TurnState.Player)
            {
                if (state == LevelState.CursorSelection)
                    CursorSelection.update(this, gameTime);
                else if (state == LevelState.Selected)
                    Selected.update(this, gameTime);
                else if (state == LevelState.Movement)
                    Movement.update(this, gameTime);
                else if (state == LevelState.Action)
                    ActionState.update(this, gameTime);


                hud.Hidden = state != LevelState.CursorSelection;
                moveTimeElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;


                //Prevents holding a button to continuously activate events
                if (keyboard.IsKeyUp(Keys.Z) || keyboard.IsKeyUp(Keys.X) || keyboard.IsKeyUp(Keys.K) || keyboard.IsKeyUp(Keys.M) || keyboard.IsKeyUp(Keys.A) || keyboard.IsKeyUp(Keys.E))
                {
                    buttonPressed = false;
                }
                if (keyboard.IsKeyDown(Keys.Z) || keyboard.IsKeyDown(Keys.X) || keyboard.IsKeyDown(Keys.K) || keyboard.IsKeyDown(Keys.M) || keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.E))
                {
                    buttonPressed = true;
                }
            }

            //Enemy turn
            else
            {
                //copy enemylist since enemylist will be edited during loop.
                Character[] enemies = new Character[enemyUnits.Count];
                int count = 0;
                foreach (KeyValuePair<Vector2, Character> e in enemyUnits)
                {
                    enemies[count++] = e.Value;
                }
                //Each enemy turn
                for (int i = 0; i < enemyUnits.Count; i++)
                {
                    enemies[i].AI.move();
                    enemies[i].AI.attack();
                }

                playerTurn = TurnState.Player;
                foreach (KeyValuePair<Vector2, Character> c in playerUnits)
                {
                    c.Value.MoveDisabled = false;
                    c.Value.ActionDisabled = false;
                }
                System.Diagnostics.Debug.Print("Player's turn");

                //Check for loss
                if (playerUnits.Count <= 0)
                {
                    System.Diagnostics.Debug.Print("Player Lost!");
                }
            }
            
        }
        

        // Update methods based on the state of the game


        //CursorSelection State
        private void cursorSelectionStateUpdate(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState(PlayerIndex.One);

            //Listen for input to move cursor
            cursor.moveCursor(gameTime, this);

            if (keyboard.IsKeyDown(Keys.Z) && !buttonPressed)
            {
                //Selecting a unit. 
                if ((playerUnits.ContainsKey(cursor.CursorPos) || enemyUnits.ContainsKey(cursor.CursorPos)))
                {
                    int x = (int)cursor.CursorPos.X;
                    int y = (int)cursor.CursorPos.Y;
                    selectedPos = new Vector2(x, y);
                    state = LevelState.Selected;
                    //Send HUD character info
                    Character c;
                    if (playerUnits.ContainsKey(selectedPos))
                        c = playerUnits[selectedPos];
                    else
                        c = enemyUnits[selectedPos];

                    //Send c to HUD
                    hud.characterSelect(c);

                    
                    board[x, y].IsSelected = true;
                }
            }

            //End turn
            else if (keyboard.IsKeyDown(Keys.E) && !buttonPressed)
            {
                playerTurn = TurnState.Enemy;
                System.Diagnostics.Debug.Print("Enemy's turn");
            }

            

        }
        //Selected State
        private void selectedStateUpdate(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState(PlayerIndex.One);

            if (keyboard.IsKeyDown(Keys.X) && !buttonPressed)
            {
                state = LevelState.CursorSelection;
                board[(int)selectedPos.X, (int)selectedPos.Y].IsSelected = false;
            }

            if (keyboard.IsKeyDown(Keys.M) && !buttonPressed && !playerUnits[selectedPos].MoveDisabled)
            {
                if (playerUnits.ContainsKey(cursor.CursorPos))
                {
                    toggleMoveRange(true, cursor.CursorPos, playerUnits[cursor.CursorPos].Movement);
                    state = LevelState.Movement;
                }
            }

            if (keyboard.IsKeyDown(Keys.A) && !buttonPressed && !playerUnits[selectedPos].ActionDisabled)
            {
                //Scan and mark potential targets
                scanForTargets(true, selectedPos, playerUnits[selectedPos].Attack.Range);
                state = LevelState.Action;
            }

            //Test AI for player unit
            else if (keyboard.IsKeyDown(Keys.K) && !buttonPressed)
            {
                playerUnits[cursor.CursorPos].AI.move();
                System.Diagnostics.Debug.Print("Player AI moved");
                foreach(KeyValuePair<Vector2, Character> c in playerUnits){
                    cursor.CursorPos = c.Key;
                }
            }

        }

        //Movement State
        private void movementStateUpdate(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState(PlayerIndex.One);

            //If already selected, confirm move and hide movement range.
            if (keyboard.IsKeyDown(Keys.Z))
            {
                if (playerUnits.ContainsKey(selectedPos))
                {
                    toggleMoveRange(false, selectedPos, playerUnits[selectedPos].Movement);
                }
                playerUnits[selectedPos].MoveDisabled = true;
                moveUnit(selectedPos, cursor.CursorPos, false);
                state = LevelState.CursorSelection;
                board[(int)selectedPos.X, (int)selectedPos.Y].IsSelected = false;
            }


            //Cancel button.  Undo's a move if the selected unit moved.
            if (keyboard.IsKeyDown(Keys.X) && !buttonPressed)
            {
                if (state == LevelState.Movement)
                {
                    state = LevelState.CursorSelection;
                    board[(int)selectedPos.X, (int)selectedPos.Y].IsSelected = false;
                    if (playerUnits.ContainsKey(selectedPos))
                    {
                        toggleMoveRange(false, selectedPos, playerUnits[selectedPos].Movement);
                        cursor.CursorPos = new Vector2(selectedPos.X, selectedPos.Y);
                    }
                }
               
            }

            if (moveTimeElapsed >= moveDelay)   // A unit is selected
            {
                //Move player character
                //moveUnit method used a character to determine direction (l = left, r = right, u = up, d = down)
                if (playerUnits.ContainsKey(selectedPos))
                {
                    //Cursor now selects move location and selectedPos keeps track of original position.  Refactoring needed.
                    int cX = (int)cursor.CursorPos.X;
                    int cY = (int)cursor.CursorPos.Y;
                    char dir = '0';
                    if (keyboard.IsKeyDown(Keys.Left) && cX > 0 && board[cX - 1, cY].IsHighlighted && !board[cX - 1, cY].IsOccupied)
                    {
                        dir = 'l';
                    }
                    else if (keyboard.IsKeyDown(Keys.Right) && cX < boardWidth - 1 && board[cX + 1, cY].IsHighlighted && (!board[cX + 1, cY].IsOccupied))
                    {
                        dir = 'r';
                    }
                    else if (keyboard.IsKeyDown(Keys.Up) && cY > 0 && board[cX, cY - 1].IsHighlighted && (!board[cX, cY - 1].IsOccupied))
                    {
                        dir = 'u';
                    }
                    else if (keyboard.IsKeyDown(Keys.Down) && cY < boardHeight - 1 && board[cX, cY + 1].IsHighlighted && (!board[cX, cY + 1].IsOccupied))
                    {
                        dir = 'd';
                    }

                    cursor.moveCursorDir(dir);
                }
                moveTimeElapsed = 0;
            }



        }

        //Action state
        private void actionStateUpdate(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState(PlayerIndex.One);

            //Listen for input to move cursor
            cursor.moveCursor(gameTime, this);

            //Confirm attack
            if (keyboard.IsKeyDown(Keys.Z) && !buttonPressed)
            {
                System.Diagnostics.Debug.Print("Confirmed attack.");
                if (enemyUnits.ContainsKey(cursor.CursorPos) && board[(int)cursor.CursorPos.X, (int)cursor.CursorPos.Y].IsSelected)
                {
                    Character c = playerUnits[selectedPos];
                    Character e = enemyUnits[cursor.CursorPos];

                    if (BattleController.isValidAction(c.Actions[0], c, selectedPos, cursor.CursorPos))
                    {
                        System.Diagnostics.Debug.Print("Enemy HP is " + e.CurrHp);
                        BattleController.performAction(c.Actions[0], c, e);
                        System.Diagnostics.Debug.Print("Enemy HP now is " + e.CurrHp);
                    }

                    if (enemyUnits[cursor.CursorPos].CurrHp < 1)
                    {
                        board[(int)cursor.CursorPos.X, (int)cursor.CursorPos.Y].IsOccupied = false;
                        enemyUnits.Remove(cursor.CursorPos);
                    }
                    playerUnits[selectedPos].ActionDisabled = true;
                    scanForTargets(false, selectedPos, playerUnits[selectedPos].Attack.Range);
                    state = LevelState.CursorSelection;
                    
                    //Check for win
                    if (enemyUnits.Count <= 0)
                    {
                        System.Diagnostics.Debug.Print("Player Won!");
                    }
                }
            }
            else if (keyboard.IsKeyDown(Keys.X) && !buttonPressed)
            {
                state = LevelState.CursorSelection;
                board[(int)selectedPos.X, (int)selectedPos.Y].IsSelected = false;
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
                    Texture2D land;
                    if(board[i,j].LandType.Equals("water"))
                    {
                        land = tileImages[1];
                    }
                    else if(board[i,j].LandType.Equals("stone_wall"))
                    {
                        land = tileImages[2];
                    }
                    else
                    {
                        land = tileImages[0];
                    }
                    spriteBatch.Draw(land, new Rectangle(startw + tileSize * i, starth + tileSize * j, tileSize, tileSize), Color.White);


                    //Draws a semi-transparent tile to show available spaces for movement/attacking
                    if (board[i, j].IsHighlighted)
                    {
                        spriteBatch.Draw(utilityImages[1], new Rectangle(startw + tileSize * i, starth + tileSize * j, tileSize, tileSize), Color.White);
                    }

                }
            }

            //Draw characters
            foreach (KeyValuePair<Vector2, Character> c in playerUnits)
            {
                //System.Diagnostics.Debug.Print("Drawing player character at position " + c.Pos.X + "," + c.Pos.Y + ". Texture = " + c.Texture);
                spriteBatch.Draw(characterImages[c.Value.Texture], new Rectangle(startw + tileSize * (int)c.Key.X, starth + tileSize * (int)c.Key.Y, tileSize, tileSize), Color.White);
                
                //Draw box around character if selected
                if (board[(int)c.Key.X, (int)c.Key.Y].IsSelected)
                {
                    spriteBatch.Draw(utilityImages[2], new Rectangle(startw + tileSize * (int)c.Key.X, starth + tileSize * (int)c.Key.Y, tileSize, tileSize), Color.White);
                }
            }
            foreach (KeyValuePair<Vector2, Character> c in enemyUnits)
            {
                //System.Diagnostics.Debug.Print("Drawing enemy character at position " + c.Pos.X + "," + c.Pos.Y + ". Texture = " + c.Texture);
                
                spriteBatch.Draw(characterImages[c.Value.Texture], new Rectangle(startw + tileSize * (int)c.Key.X, starth + tileSize * (int)c.Key.Y, tileSize, tileSize), Color.White);
                //Draw box around character if selected
                if (board[(int)c.Key.X, (int)c.Key.Y].IsSelected)
                {
                    spriteBatch.Draw(utilityImages[2], new Rectangle(startw + tileSize * (int)c.Key.X, starth + tileSize * (int)c.Key.Y, tileSize, tileSize), Color.White);
                }
            }

            //Draws a box around a selected tile
            if (board[(int)selectedPos.X, (int)selectedPos.Y].IsSelected)
            {
                spriteBatch.Draw(utilityImages[2], new Rectangle(startw + tileSize * (int)selectedPos.X, starth + tileSize * (int)selectedPos.Y, tileSize, tileSize), Color.White);
            }

            //Draw cursor on top of board.
            cursor.draw(spriteBatch, startw, starth, tileSize);

            //Draw HUD
            hud.Draw(spriteBatch);
            hud.mouseUpdate();

        }

        //Setters and Getters
        public int BoardWidth
        {
            get { return boardWidth; }
            set { boardWidth = value; }
        }
        public int BoardHeight
        {
            get { return boardHeight; }
            set { boardHeight = value; }
        }
        public Tile[,] Board
        {
            get { return board; }
            set { board = value; }
        }
        public Boolean ButtonPressed
        {
            get { return buttonPressed; }
            set { buttonPressed = value; }
        }
        public Vector2 SelectedPos
        {
            get { return selectedPos; }
            set { selectedPos = value; }
        }
        public Cursor Cursor
        {
            get { return cursor; }
            set { cursor = value; }
        }
        public float MoveDelay
        {
            get { return moveDelay; }
            set { moveDelay = value; }
        }
        public float MoveTimeElapsed
        {
            get { return moveTimeElapsed; }
            set { moveTimeElapsed = value; }
        }
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
        public HUD_Controller HUD
        {
            get { return hud; }
            set { hud = value; }
        }
        public LevelState State
        {
            get { return state; }
            set { state = value; }
        }
        public TurnState PlayerTurn
        {
            get { return playerTurn; }
            set { playerTurn = value; }
        }
    }
}