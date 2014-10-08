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

namespace JSA_Game.Maps
{
    class Level
    {

        const int TILE_IMAGE_COUNT = 3;
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
            * */

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

        /*
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
        */


        //Moves unit given a path, provided by A*
        public void moveUnit(Vector2 startPos, Vector2 endPos)
        {
            Stack path = findPath(startPos, endPos);
            //if path exists
            if (path.Count > 0)
            {
                int remMovement;
                //stop variable stops movement for enemy units 1 early since
                //the postion of a computer's target is their actual position.
                int stop;
                Boolean isEnemy = enemyUnits.ContainsKey(startPos);
                if (isEnemy)
                {
                    remMovement = enemyUnits[startPos].Movement;
                    stop = 1;
                }
                else
                {
                    remMovement = playerUnits[startPos].Movement;
                    stop = 0;
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

        // H score: Number of tiles from destination tile.
        private int calcDist(Vector2 pos, Vector2 target)
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

        /// <summary>
        /// Loads content for use in the level.
        /// </summary>
        /// <param name="content">ContentManager sent from the main class</param>
        public void loadContent(ContentManager content)
        {
            tileImages[0] = content.Load<Texture2D>("grass_tile");
            tileImages[1] = content.Load<Texture2D>("water");
            tileImages[2] = content.Load<Texture2D>("stone_wall");
            //highlightImages[0] = content.Load<Texture2D>("no_highlight");
            highlightImages[0] = content.Load<Texture2D>("tempMove");   //Change later!
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

            //Test A*   As enemy move AI
            if (keyboard.IsKeyDown(Keys.E) && !buttonPressed && state == LevelState.CursorSelection)
            {
                //Each enemy
                //Need to make copy since moving alters dictionary
                int size = enemyUnits.Count;
                //Character[] chars = new Character[size];
                Vector2[] vecs = new Vector2[size];
                int count = 0;
                foreach (KeyValuePair<Vector2, Character> e in enemyUnits)
                {
                 //   chars[count] = e.Value;
                    vecs[count] = e.Key;
                    count++;
                }

                for (int i = 0; i < size;i++)
                {
                    //Picks closest target
                    int dist;
                    int shortestDist = 64;
                    Vector2 target = new Vector2(-1,-1);
                    foreach (KeyValuePair<Vector2, Character> p in playerUnits)
                    {
                        dist = calcDist(vecs[i], p.Key);
                        if (dist < shortestDist)
                        {
                            shortestDist = dist;
                            target = p.Key;
                        } 

                    }

                    //Move towards target if found
                    if (!target.Equals(new Vector2(-1, -1)))
                    {
                        //System.Diagnostics.Debug.Print("Enemy at (" + vecs[i].X + ", " + vecs[i].Y + ") moving");
                        moveUnit(vecs[i], target);
                    }
                }
                
            }


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
                        }
                    }
                    //If already selected, confirm move and hide movement range.
                    else if (state == LevelState.Movement)
                    {
                        selected = false;

                        if (playerUnits.ContainsKey(selectedPos))
                        {
                            toggleMoveRange(false, selectedPos, playerUnits[selectedPos].Movement);
                        }
                        
                        moveUnit(selectedPos, cursor.CursorPos);
                        clearMoveArrows();
                        state = LevelState.CursorSelection;
                    }
                }
                

                //Cancel button.  Undo's a move if the selected unit moved.
                if (keyboard.IsKeyDown(Keys.X) && !buttonPressed)
                {
                    if (state == LevelState.Movement)
                    {
                        selected = false;
                        state = LevelState.CursorSelection;
                        if (playerUnits.ContainsKey(selectedPos))
                        {
                            toggleMoveRange(false, selectedPos, playerUnits[selectedPos].Movement);
                            cursor.CursorPos = new Vector2(selectedPos.X, selectedPos.Y);
                            clearMoveArrows();
                        }
                    }
                    else
                    {
                        selected = false;
                        state = LevelState.CursorSelection;
                    }
                }

                HUD.Hidden = state != LevelState.CursorSelection;
                moveTimeElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (selected && moveTimeElapsed >= moveDelay && state == LevelState.Movement)   // A unit is selected
                {
                    //Move player character
                    //moveUnit method used a character to determine direction (l = left, r = right, u = up, d = down)
                    if (playerUnits.ContainsKey(selectedPos))
                    {
                      
                        //Cursor now selects move location and selectedPos keeps track of original position.  Refactoring needed.
                        Stack path = new Stack();
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
                        if (dir != '0')
                        {
                            clearMoveArrows();
                            cursor.moveCursorDir(dir);
                            path = findPath(selectedPos, cursor.CursorPos);
                        }

                        if (path.Count > 0)
                        {
                            //Draw out path
                            Vector2 v;
                            while (path.Count > 0)
                            {
                                v = (Vector2)path.Pop();
                                board[(int)v.X, (int)v.Y].MoveImage = "tempMove";
                            }
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
                            state = LevelState.Movement;
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
                        //Vector2 target = new Vector2(cursor.CursorPos.X + 1, cursor.CursorPos.Y);
                        //Character enemy = enemyUnits[target];
                        Character enemy = new Character();
                        Vector2 epos = new Vector2(-1,-1);
    //!!!!                    //Temporary!  Only attacks the one enemy!                              !!!!
                        foreach (KeyValuePair<Vector2, Character> e in enemyUnits)
                        {
                            epos = e.Key;
                            enemy = e.Value;
                        }
                        //if (BattleController.isValidAction(c.Actions[0], c, cursor.CursorPos, target))
                        if (BattleController.isValidAction(c.Actions[0], c, cursor.CursorPos, epos) && /*temp*/ calcDist(cursor.CursorPos, epos) == 1)
                        {
                            System.Diagnostics.Debug.Print("Enemy HP is " + enemy.CurrHp);
                            BattleController.performAction(c.Actions[0], c, enemy);
                            System.Diagnostics.Debug.Print("Enemy HP now is " + enemy.CurrHp);
                        }

                        if (enemyUnits[epos].CurrHp < 1)
                        {
                            board[(int)epos.X, (int)epos.Y].IsOccupied = false;
                            enemyUnits.Remove(epos);
                        }

                        state = LevelState.Selected;

                    }
                  
                }



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
                        spriteBatch.Draw(highlightImages[1], new Rectangle(startw + tileSize * i, starth + tileSize * j, tileSize, tileSize), Color.White);
                    }

                    //If moving, show arrows
                    if (state == LevelState.Movement && board[i, j].MoveImage.Equals("tempMove"))
                    {
                        //Definitely need to change this
                        spriteBatch.Draw(highlightImages[0], new Rectangle(startw + tileSize * i, starth + tileSize * j, tileSize, tileSize), Color.White);
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

        //Temporary function
        private void clearMoveArrows()
        {
            for (int i = 0; i < boardWidth; i++)
            {
                for (int j = 0; j < boardHeight; j++)
                {
                    board[i, j].MoveImage = "";
                }
            }
        }
    }
}