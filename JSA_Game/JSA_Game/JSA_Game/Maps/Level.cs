using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using GameStateManagement;
using JSA_Game.AI;
using JSA_Game.HUD;
using JSA_Game.Battle_Controller;
using JSA_Game.CharClasses;
using JSA_Game.Maps.State;
using JSA_Game.Maps.Tiles;

namespace JSA_Game.Maps
{
    public class Level
    {
        const int TILE_IMAGE_COUNT = 3;
        const int UTILITY_IMAGE_COUNT = 2;
        const int HIGHLIGHT_IMAGE_COUNT = 3;
        public const int TILE_SIZE = 50;

        int numTilesShowing;
        const int DEFAULT_NUM_TILES_SHOWING = 10; 

        //Draw related variables
        int showStartX, showStartY;
        public const int MAP_START_H = 0;
        public const int MAP_START_W = 0;
        private int itemIndex;

        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;

        int boardWidth, boardHeight;
        int maxPlayerUnits, MaxEnemyUnits, playerUnitCount, enemyUnitCount;
        private Boolean buttonPressed;

        int turn;

        private Tile[,] board;

        private Vector2 selectedPos;

        //Cursor Variables
        private Cursor cursor;
       
        //Timing variables
        private float cursorMoveDelay = 100;
        private float moveTimeElapsed;
       
        //Image Data Structures
        Texture2D[] tileImages;
        Texture2D[] utilityImages;
        Texture2D[] highlightImages;

        //HUD
        private HUD_Controller hud;
        ArrayList allCharacters = new ArrayList();

        //State
        private LevelState state;
        private TurnState playerTurn;
        private WinLossState winState;
        

       // Dictionary<Vector2, Character> playerUnits, enemyUnits;
        ArrayList pUnits, eUnits;
        Dictionary<String, Texture2D> characterImages;

        //Selected action
        private Battle_Controller.Action selectedAction;
        private Battle_Controller.Action prevSelectedAction;

        //Attack target list
        HashSet<Character> targetList;
       
        //Animation variables
        bool isAnimatingMove;
        Vector2 moveAnimCurrPos;
        Vector2 moveAnimDestPos;
        Vector2 moveAnimFinalPos;
        Stack moveAnimPath;
        float moveAnimTimeElapsed;
        float moveAnimDelay = 30;

        public bool isAnimatingAttack;


        //Unit placement variables
        bool placeableLevel = false;
        ScreenManager screenManager;
        int numPlaceableSpaces;



        /// <summary>
        /// Generates a level from a given text file
        /// //Folder for files is located at \JSA_Game\bin\x86\Debug\Levels
        /// </summary>
        /// <param name="filename">Filename to read</param>
        public Level(string filename, ScreenManager sm)
        {
           
            System.Diagnostics.Debug.Print("Generating level from file: " + filename);
            string line;
            itemIndex = -1;

            Sound.PlaySound("battle");

            // Read the file and read it line by line.
            StreamReader file = new StreamReader("Levels/" + filename + ".txt");
            while ((line = file.ReadLine()) != null)
            {
                if (line.Length == 0)
                {
                    //Empty line
                }
                else if (line[0] == '/' || line[0] == '\n')
                {
                    //Comment
                }
                else if (line[0] == 'i')
                {
                    System.Diagnostics.Debug.Print("Initial Conditions: " + line);

                    //Split string starting after identifying character and colon.
                    string[] param = line.Substring(2).Split(',');

                    int i = 0;

                    boardWidth = Convert.ToInt32(param[i++]);
                    boardHeight = Convert.ToInt32(param[i++]);
                    maxPlayerUnits = Convert.ToInt32(param[i++]);
                    if (param.Length == 7)
                    {
                        numPlaceableSpaces = Convert.ToInt32(param[i++]);
                        placeableLevel = true;
                    }
                    MaxEnemyUnits = Convert.ToInt32(param[i++]);
                    showStartX = Convert.ToInt32(param[i++]);
                    showStartY = Convert.ToInt32(param[i++]);

                    board = new Tile[boardWidth, boardHeight];
                    initialize();

                }
                else if (line[0] == 'G')
                {
                    System.Diagnostics.Debug.Print("Primary Geography: " + line);
                    for (int i = 0; i < boardWidth; i++)
                        for (int j = 0; j < boardHeight; j++)
                            board[i, j] = tileChooser(line.Substring(2));
                            //board[i, j] = new Tile(line.Substring(2));
                }

                else if (line[0] == 'g')
                {
                    System.Diagnostics.Debug.Print("Specific Geography: " + line);

                    //Split string starting after identifying character and colon.
                    string[] param = line.Substring(2).Split(',');
                    int xStartLoop = 0, xEndLoop = 0;
                    int yStartLoop = 0, yEndLoop = 0;
                    if (param[0].Contains('-'))
                    {
                        string[] bounds = param[0].Split('-');
                        xStartLoop = Convert.ToInt32(bounds[0]);
                        xEndLoop = Convert.ToInt32(bounds[1]);
                    }
                    else
                    {
                        xStartLoop = Convert.ToInt32(param[0]);
                        xEndLoop = xStartLoop;
                    }
                    if (param[1].Contains('-'))
                    {
                        string[] bounds = param[1].Split('-');
                        yStartLoop = Convert.ToInt32(bounds[0]);
                        yEndLoop = Convert.ToInt32(bounds[1]);
                    }
                    else
                    {
                        yStartLoop = Convert.ToInt32(param[1]);
                        yEndLoop = yStartLoop;
                    }

                    for(int i = xStartLoop; i < xEndLoop+1; i++)
                    {
                        for (int j = yStartLoop; j < yEndLoop+1; j++)
                        {
                            board[i, j] = tileChooser(param[2]);
                            //board[i, j] = new Tile(param[2]);
                            //board[i,j].IsOccupied = true;
                        }
                    }

                    
                }

                else if (line[0] == 'o')
                {
                    System.Diagnostics.Debug.Print("Unit placeable: " + line);

                    //Split string starting after identifying character and colon.
                    string[] param = line.Substring(2).Split(',');
                    int xStartLoop = 0, xEndLoop = 0;
                    int yStartLoop = 0, yEndLoop = 0;
                    if (param[0].Contains('-'))
                    {
                        string[] bounds = param[0].Split('-');
                        xStartLoop = Convert.ToInt32(bounds[0]);
                        xEndLoop = Convert.ToInt32(bounds[1]);
                    }
                    else
                    {
                        xStartLoop = Convert.ToInt32(param[0]);
                        xEndLoop = xStartLoop;
                    }
                    if (param[1].Contains('-'))
                    {
                        string[] bounds = param[1].Split('-');
                        yStartLoop = Convert.ToInt32(bounds[0]);
                        yEndLoop = Convert.ToInt32(bounds[1]);
                    }
                    else
                    {
                        yStartLoop = Convert.ToInt32(param[1]);
                        yEndLoop = yStartLoop;
                    }

                    for (int i = xStartLoop; i < xEndLoop + 1; i++)
                    {
                        for (int j = yStartLoop; j < yEndLoop + 1; j++)
                        {
                            board[i, j].HlState = HighlightState.MOVE;
                        }
                    }


                }



                //Placing units
                else if (line[0] == 'p' || line[0] == 'e')
                {
                    int allyFlag = line[0] == 'p' ? 1 : 0;
                    Character c = new Character();
                    System.Diagnostics.Debug.Print("Player/Enemy unit: " + line);

                    //Split string starting after identifying character and colon.
                    string[] param = line.Substring(2).Split(',');
                    int x, y;
                    x = Convert.ToInt32(param[0]);
                    y = Convert.ToInt32(param[1]);

                    if(param[2].Equals("Warrior"))
                    {
                        c = new Warrior(this, Convert.ToInt32(param[3]));
                    }
                    else if (param[2].Equals("Mage"))
                    {
                        c = new Mage(this, Convert.ToInt32(param[3]));
                    }
                    else if (param[2].Equals("Archer"))
                    {
                        c = new Archer(this, Convert.ToInt32(param[3]));
                    }
                    else if (param[2].Equals("Cleric"))
                    {
                        c = new Cleric(this, Convert.ToInt32(param[3]));
                    }

                    //More


                    //Setting AI
                    if (param[4].Equals("Aggressive"))
                    {
                        c.AI = new AggressiveAI(c, this);
                    }
                    else if (param[4].Equals("Defensive"))
                    {
                        c.AI = new DefensiveAI(c, this);
                    }
                    else if (param[4].Equals("Stationary"))
                    {
                        c.AI = new StationaryAI(c, this);
                    }
                    

                    addUnit(allyFlag, c, new Vector2(x, y));
                }

            }
                file.Close();
                foreach (Character p in pUnits)
                {
                    allCharacters.Add(p);
                }
                foreach (Character e in eUnits)
                {
                    allCharacters.Add(e);
                }

                hud.setTargetList(allCharacters);
                screenManager = sm;
                
        }

        private Tile tileChooser(string tileName)
        {
            if(tileName.Equals("plain")){
                return new PlainsTile();
            }
            else if( tileName.Equals("water")){
                return new WaterTile();
            }
            else if( tileName.Equals("stone_wall")){
                return new StoneWallTile();
            }
            else
                return new PlainsTile();
        }

        /// <summary>
        /// Initializes the level. Sets state, initializes containers, and creates a Cursor.
        /// </summary>
        private void initialize()
        {

            state = placeableLevel ? LevelState.Placement : LevelState.CursorSelection;
            playerTurn = TurnState.Player;
            winState = WinLossState.InProgess;
            turn = 0;

            isAnimatingMove = false;
            isAnimatingAttack = false;
           // showStartX = 0;
           // showStartY = 0;
            numTilesShowing = DEFAULT_NUM_TILES_SHOWING;

            playerUnitCount = 0;
            enemyUnitCount = 0;

            buttonPressed = false;
            cursor = new Cursor(this);

            pUnits = new ArrayList(maxPlayerUnits);
            eUnits = new ArrayList(MaxEnemyUnits);
            targetList = new HashSet<Character>();

            characterImages = new Dictionary<string, Texture2D>();
            tileImages = new Texture2D[TILE_IMAGE_COUNT];
            utilityImages = new Texture2D[UTILITY_IMAGE_COUNT];
            highlightImages = new Texture2D[HIGHLIGHT_IMAGE_COUNT];

            selectedAction = null;
            
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
            
            if (!unit.IsEnemy && pUnits.Contains(unit))
            {
                System.Diagnostics.Debug.Print("Level.addUnit(): Duplicate found");
                board[(int)unit.Pos.X, (int)unit.Pos.Y].Occupant = null;
                board[(int)unit.Pos.X, (int)unit.Pos.Y].IsOccupied = false;
                pUnits.Remove(unit);
                playerUnitCount--;

            }

            if (!unit.IsEnemy && playerUnitCount != maxPlayerUnits)
            {
                playerUnitCount++;
                board[xPos, yPos].IsOccupied = true;
                pUnits.Add(unit);
                board[xPos, yPos].Occupant = unit;
                unit.Pos = pos;
            }

            else if (unit.IsEnemy && enemyUnitCount != MaxEnemyUnits)
            {
                enemyUnitCount++;
                board[xPos, yPos].IsOccupied = true;
                eUnits.Add(unit);
                board[xPos, yPos].Occupant = unit;
                unit.Pos = pos;
            }
            else
            {
                System.Diagnostics.Debug.Print("Level.addUnit() : Not enough space in level to place unit.");
            }
        }



        /// <summary>
        /// Moves unit given a path, provided by A*
        /// </summary>
        /// <param name="startPos">Starting position of the unit</param>
        /// <param name="endPos">Destination position for the unit</param>
        /// <param name="AImoved">If an AI called this function</param>
        /// <param name="moveIfInRange">If the AI only wants to move if the target is in range</param>
        public void moveUnit(GameTime gameTime, Vector2 startPos, Vector2 endPos, Boolean AImoved, Boolean moveIfInRange)
        {
            moveAnimPath = AStar.findPath(this, startPos, endPos);
            //if path exists
            if (moveAnimPath.Count > 0)
            {
                //If character will only move if a target is in range
                if (!moveIfInRange || moveAnimPath.Count <= board[(int)startPos.X, (int)startPos.Y].Occupant.Movement + 2)
                {
                    int remMovement;
                    Character unit;
                    //stop variable stops movement for enemy units 1 early since
                    //the postion of a computer's target is their actual position.
                    int stop;
                    Boolean isEnemy = board[(int)startPos.X, (int)startPos.Y].Occupant.IsEnemy;
                    if (isEnemy)
                    {
                        unit = board[(int)startPos.X, (int)startPos.Y].Occupant;
                        remMovement = unit.Movement;
                        stop = 1;
                    }
                    else
                    {

                        unit = board[(int)startPos.X, (int)startPos.Y].Occupant;
                        remMovement = unit.Movement;
                        stop = AImoved ? 1 : 0;
                    }

                    //Starting position
                    Vector2 pos = (Vector2)moveAnimPath.Pop();
                    while (remMovement > 0 && moveAnimPath.Count > stop)
                    {
                        int xPos = (int)pos.X;
                        int yPos = (int)pos.Y;
                        //Character c;
                        Vector2 next;
                        next = (Vector2)moveAnimPath.Pop();
                        if (moveAnimPath.Count == stop || remMovement == 1)
                        {
                            moveAnimFinalPos = next;
                        }

                        moveAnimCurrPos = pos;
                        moveAnimDestPos = next;
                        isAnimatingMove = true;
                       // System.Diagnostics.Debug.Print("Animating Movement...");
                        animateMove(gameTime);
                        pos = next;
                        remMovement--;
                    }
                    //board[(int)unit.Pos.X, (int)unit.Pos.Y].Occupant = null;
                    unit.Pos = pos;
                    
                }
            }
        }

        //yuck.... Possibly try using another thread, forcing the main game to wait
        //   for the animation to complete.
        private void animateMove(GameTime gameTime)
        {
            moveAnimTimeElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            //if (moveAnimTimeElapsed >= moveAnimDelay)
            //{
                //Move the character image a little
                //char dir = '0';
                //if (moveAnimDestPos.X < moveAnimCurrPos.X)
                //    dir = 'l';
                //else if (moveAnimDestPos.X > moveAnimCurrPos.X)
                //    dir = 'r';
                //else if (moveAnimDestPos.Y < moveAnimCurrPos.Y)
                //    dir = 'u';
                //else if (moveAnimDestPos.Y > moveAnimCurrPos.Y)
                //    dir = 'd';

                if (moveAnimDestPos.X < moveAnimCurrPos.X)
                {

                }

                moveAnimTimeElapsed = 0;
           // }
                board[(int)moveAnimDestPos.X, (int)moveAnimDestPos.Y].Occupant = board[(int)moveAnimCurrPos.X, (int)moveAnimCurrPos.Y].Occupant;
                board[(int)moveAnimCurrPos.X, (int)moveAnimCurrPos.Y].Occupant = null;

                board[(int)moveAnimCurrPos.X, (int)moveAnimCurrPos.Y].IsOccupied = false;
                board[(int)moveAnimDestPos.X, (int)moveAnimDestPos.Y].IsOccupied = true;
           if (moveAnimDestPos.Equals(moveAnimFinalPos))
            {



                isAnimatingMove = false;
               // System.Diagnostics.Debug.Print("Destination Reached");
                moveAnimFinalPos = new Vector2(-1, -1);
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
                board[(int)former.X, (int)former.Y].Occupant = board[(int)current.X, (int)current.Y].Occupant;
                board[(int)current.X, (int)current.Y].Occupant = null;
                board[(int)current.X, (int)current.Y].IsOccupied = false;
                board[(int)former.X, (int)former.Y].IsOccupied = true;
                toggleMoveRange(false, former, board[(int)former.X, (int)former.Y].Occupant.Movement);
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
            //board[(int)pos.X, (int)pos.Y].IsHighlighted = show;
            board[(int)pos.X, (int)pos.Y].HlState = show? HighlightState.MOVE : HighlightState.NONE;
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
            board[x, y].HlState = show ? HighlightState.MOVE : HighlightState.NONE;
            if (x > 0)
            {
                if ((show && board[x - 1, y].IsWalkable && !board[x - 1, y].IsOccupied) || !show)
                {
                    board[x - 1, y].HlState = show ? HighlightState.MOVE : HighlightState.NONE;
                    toggleMoveRange(show, x - 1, y, remMove - 1);
                }
            }
            if (x < boardWidth - 1)
            {
                if ((show && board[x + 1, y].IsWalkable && !board[x + 1, y].IsOccupied) || !show)
                {
                    board[x + 1, y].HlState = show ? HighlightState.MOVE : HighlightState.NONE;
                    toggleMoveRange(show, x + 1, y, remMove - 1);
                }
            }
            if (y > 0)
            {
                if ((show && board[x, y - 1].IsWalkable && !board[x, y - 1].IsOccupied) || !show)
                {
                    board[x, y - 1].HlState = show ? HighlightState.MOVE : HighlightState.NONE;
                    toggleMoveRange(show, x, y - 1, remMove - 1);
                }
            }
            if (y < boardHeight - 1)
            {
                if ((show && board[x, y + 1].IsWalkable && !board[x, y + 1].IsOccupied) || !show)
                {
                    board[x, y + 1].HlState = show ? HighlightState.MOVE : HighlightState.NONE;
                    toggleMoveRange(show, x, y + 1, remMove - 1);
                }
            }
        }

        /// <summary>
        /// Scan current location for attackable targets.
        /// </summary>
        /// <param name="show">Boolean determining whether to select or deselect targets</param>
        /// <param name="pos">Position of the selected unit</param>
        /// <param name="range">Attack range of the unit's action</param>
        //public void scanForTargets(Boolean show, Vector2 pos, int range)
        //{
        //    targetList.Clear();
        //    board[(int)pos.X, (int)pos.Y].IsSelected = show;
        //    scanForTargets(show, (int)pos.X, (int)pos.Y, range, HighlightState.ACTION);
        //}

        /// <summary>
        /// Scan current location for attackable targets.
        /// </summary>
        /// <param name="show">Boolean determining whether to select or deselect targets</param>
        /// <param name="pos">Position of the selected unit</param>
        /// <param name="range">Attack range of the unit's action</param>
        /// <param name="showAoe">Shows the aoe range</param>
        public void scanForTargets(Boolean show, Vector2 pos, int range, bool isAoe, bool friendly)
        {
            int x = (int)pos.X;
            int y = (int)pos.Y;
            HighlightState hlState = HighlightState.ACTION;
            targetList.Clear();
            //If aoe, show red. else show orange
            if (isAoe)
            {
                hlState = HighlightState.AOE;
                if (board[x,y].Occupant != null)
                {
                    if (board[x, y].Occupant.IsEnemy && !selectedAction.Friendly)
                    {
                        targetList.Add(board[x, y].Occupant);
                    }
                    if (!board[x, y].Occupant.IsEnemy && selectedAction.Friendly)
                    {
                        targetList.Add(board[x, y].Occupant);
                    }
                }
            }
           
            board[x, y].IsSelected = show;
            if (board[x, y].IsWalkable)
            {
                board[x, y].HlState = show ? hlState : HighlightState.NONE;
            }
            scanForTargets(show, x,y, range, hlState, friendly);
        }

        /// <summary>
        /// Helper method to recursively find attackable targets on the board that
        /// </summary>
        /// <param name="show">Boolean determining whether to show or hide the range</param>
        /// <param name="x">X position of the selected unit</param>
        /// <param name="y">Y position of the selected unit</param>
        /// <param name="remRange">Remaining attack range of the unit's action</param>
        /// <param name="hlState">Highlight state to change tile into/hide</param>
        private void scanForTargets(Boolean show, int x, int y, int remRange, HighlightState hlState, bool friendly)
        {
            if (remRange <= 0) return;
            if (x > 0)
            {
                if (board[x - 1, y].IsWalkable)
                {
                    board[x - 1, y].HlState = show ? hlState : HighlightState.NONE;
                }
                if ((show && board[x - 1, y].Occupant != null) || !show)
                {
                    board[x - 1, y].IsSelected = show;
                    if (show && hlState == HighlightState.AOE && board[x - 1, y].Occupant.IsEnemy && !friendly &&
                        AStar.calcDist(selectedPos, new Vector2(x-1,y)) <= selectedAction.Range + selectedAction.AoeRange)
                    {
                        targetList.Add(board[x - 1, y].Occupant);
                    }
                    if (show && hlState == HighlightState.AOE && !board[x - 1, y].Occupant.IsEnemy && friendly &&
                        AStar.calcDist(selectedPos, new Vector2(x - 1, y)) <= selectedAction.Range + selectedAction.AoeRange)
                    {
                        targetList.Add(board[x - 1, y].Occupant);
                    }
                    
                }
                if (board[x - 1, y].IsAttackThroughable || !show)
                {
                    scanForTargets(show, x - 1, y, remRange - 1, hlState, friendly);
                }
            }
            if (x < boardWidth - 1)
            {
                if (board[x + 1, y].IsWalkable)
                {
                     board[x + 1, y].HlState = show ? hlState : HighlightState.NONE;
                }
                if ((show && board[x + 1, y].Occupant != null) || !show)
                {
                    board[x + 1, y].IsSelected = show;

                    if (show && hlState == HighlightState.AOE && board[x + 1, y].Occupant.IsEnemy && !friendly &&
                        AStar.calcDist(selectedPos, new Vector2(x + 1, y)) <= selectedAction.Range + selectedAction.AoeRange)
                    {
                        targetList.Add(board[x + 1, y].Occupant);
                    }
                    if (show && hlState == HighlightState.AOE && !board[x + 1, y].Occupant.IsEnemy && friendly &&
                        AStar.calcDist(selectedPos, new Vector2(x + 1, y)) <= selectedAction.Range + selectedAction.AoeRange)
                    {
                        targetList.Add(board[x + 1, y].Occupant);
                    }
                   
                }
                if (board[x + 1, y].IsAttackThroughable || !show)
                {
                    scanForTargets(show, x + 1, y, remRange - 1, hlState, friendly);
                }
            }
            if (y > 0)
            {
                if (board[x, y - 1].IsWalkable)
                { 
                    board[x, y - 1].HlState = show ? hlState : HighlightState.NONE;
                }
                if ((show && board[x, y - 1].Occupant != null) || !show)
                {
                    board[x, y - 1].IsSelected = show;
                    if (show && hlState == HighlightState.AOE && board[x, y - 1].Occupant.IsEnemy && !friendly &&
                        AStar.calcDist(selectedPos, new Vector2(x, y - 1)) <= selectedAction.Range + selectedAction.AoeRange)
                    {
                        targetList.Add(board[x, y - 1].Occupant);
                    }
                    if (show && hlState == HighlightState.AOE && !board[x, y - 1].Occupant.IsEnemy && friendly &&
                        AStar.calcDist(selectedPos, new Vector2(x, y - 1)) <= selectedAction.Range + selectedAction.AoeRange)

                    {
                        targetList.Add(board[x, y - 1].Occupant);
                    }
                  
                }
                if (board[x, y - 1].IsAttackThroughable || !show)
                {
                    scanForTargets(show, x, y - 1, remRange - 1, hlState, friendly);
                }
            }
            if (y < boardHeight - 1)
            {
                if (board[x, y + 1].IsWalkable)
                {
                    board[x, y + 1].HlState = show ? hlState : HighlightState.NONE;
                }
                if ((show && board[x, y + 1].Occupant != null) || !show)
                {
                    board[x, y + 1].IsSelected = show;
                    if (show && hlState == HighlightState.AOE && board[x, y + 1].Occupant.IsEnemy && !friendly &&
                        AStar.calcDist(selectedPos, new Vector2(x, y + 1)) <= selectedAction.Range + selectedAction.AoeRange)
                    {
                        targetList.Add(board[x, y + 1].Occupant);
                    }
                    if (show && hlState == HighlightState.AOE && !board[x, y + 1].Occupant.IsEnemy && friendly &&
                        AStar.calcDist(selectedPos, new Vector2(x, y + 1)) <= selectedAction.Range + selectedAction.AoeRange)

                    {
                        targetList.Add(board[x, y + 1].Occupant);
                    }
                    
                }
                if (board[x, y + 1].IsAttackThroughable || !show)
                {
                    scanForTargets(show, x, y + 1, remRange - 1, hlState, friendly);
                }

                
            }
        }


        public void attackTarget(Vector2 currPos, Vector2 targetPos, JSA_Game.Battle_Controller.Action action)
        {
            Character c = board[(int)currPos.X, (int)currPos.Y].Occupant;
            Character t = board[(int)targetPos.X, (int)targetPos.Y].Occupant;
            System.Diagnostics.Debug.Print("Target HP is " + t.CurrHp);
                if (itemIndex != -1)
                {
                
                    if (!BattleController.performItem(c, t, itemIndex))
                        System.Diagnostics.Debug.Print("Missed!");
                    itemIndex = -1;
                }
                else {
                    if (BattleController.isValidAction(action, c, currPos, targetPos))
                    {
                        if (!BattleController.performAction(action, c, t))
                            System.Diagnostics.Debug.Print("Missed!");

                        System.Diagnostics.Debug.Print("Target HP now is " + t.CurrHp);
                        c.ActionDisabled = true;
                    }
                    else
                    {
                        c.ActionDisabled = false;
                    }
                }
            

            if (t.CurrHp < 1)
            {
                board[(int)targetPos.X, (int)targetPos.Y].IsOccupied = false;
                board[(int)targetPos.X, (int)targetPos.Y].Occupant = null;
                if (t.IsEnemy)
                {
                    eUnits.Remove(t);
                }
                else
                {
                    pUnits.Remove(t);
                }
            }
           // scanForTargets(false, currPos, action.Range);

            //Check for win
            if (t.IsEnemy && eUnits.Count <= 0)
            {
                System.Diagnostics.Debug.Print("Player Won!");
                winState = WinLossState.Win;
            }
            else if (!t.IsEnemy && pUnits.Count <= 0)
            {
                System.Diagnostics.Debug.Print("Player Lost!");
                winState = WinLossState.Loss;
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
            utilityImages[1] = content.Load<Texture2D>("target_square");

            highlightImages[0] = content.Load<Texture2D>("blue_highlight");
            highlightImages[1] = content.Load<Texture2D>("orange_highlight");
            highlightImages[2] = content.Load<Texture2D>("red_highlight");
            foreach (Character c in Game1.getPlayerChars())
            {
                System.Diagnostics.Debug.Print("Created a player unit");
                if (!characterImages.ContainsKey("player" + c.Texture))
                {
                    characterImages.Add("player" + c.Texture, content.Load<Texture2D>("player" + c.Texture));

                }
            }

            foreach (Character c in eUnits)
            {
                System.Diagnostics.Debug.Print("Created an enemy unit");
                if (!characterImages.ContainsKey("enemy" + c.Texture))
                {
                    characterImages.Add("enemy" + c.Texture, content.Load<Texture2D>("enemy" + c.Texture));
                }
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
            if (isAnimatingMove)
            {
                //Continue to animate
            }
            //otherwise listen for input

            else
            {
                keyboardState = Keyboard.GetState();
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
                    else if (state == LevelState.Placement)
                        CharacterPlacement.update(this, screenManager, gameTime);




                    hud.Update(gameTime);
                    hud.Hidden = state != LevelState.CursorSelection && state != LevelState.Placement;
                    if (hud.Hidden)
                    {
                        hud.ButtonSelect(keyboard);
                    }
                    moveTimeElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;


                    //Prevents holding a button to continuously activate events
                    if (!buttonPressed && !(keyboard.IsKeyUp(Keys.Left) && keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.Up) && keyboard.IsKeyUp(Keys.Down)))
                    {
                        moveTimeElapsed = cursorMoveDelay - 20;
                    }
                    if (keyboard.IsKeyUp(Keys.Z) || keyboard.IsKeyUp(Keys.X) || keyboard.IsKeyUp(Keys.K) || keyboard.IsKeyUp(Keys.M) ||
                        keyboard.IsKeyUp(Keys.A) || keyboard.IsKeyUp(Keys.E) || keyboard.IsKeyUp(Keys.F1) || keyboard.IsKeyUp(Keys.F2) || keyboard.IsKeyUp(Keys.F3))
                    {
                        buttonPressed = false;
                    }
                    if (keyboard.IsKeyDown(Keys.Z) || keyboard.IsKeyDown(Keys.X) || keyboard.IsKeyDown(Keys.K) || keyboard.IsKeyDown(Keys.M) ||
                        keyboard.IsKeyDown(Keys.A) || keyboard.IsKeyDown(Keys.E) || keyboard.IsKeyDown(Keys.F1) || keyboard.IsKeyDown(Keys.F2) || keyboard.IsKeyDown(Keys.F3) ||
                        keyboard.IsKeyDown(Keys.Left) || keyboard.IsKeyDown(Keys.Right) || keyboard.IsKeyDown(Keys.Up) || keyboard.IsKeyDown(Keys.Down))
                    {
                        buttonPressed = true;
                    }

                }

                //Enemy turn
                else
                {
                    //Each enemy turn
                    foreach (Character c in eUnits)
                    {
                        c.AI.move(gameTime);
                        c.AI.action();
                    }


                    playerTurn = TurnState.Player;
                    foreach (Character c in pUnits)
                    {
                        c.MoveDisabled = false;
                        c.ActionDisabled = false;
                    }
                    System.Diagnostics.Debug.Print("Player's turn");

                    playerTurn = TurnState.Player;
                    foreach (Character c in pUnits)
                    {
                        c.MoveDisabled = false;
                        c.ActionDisabled = false;
                    }
                    System.Diagnostics.Debug.Print("Player's turn");
                    Battle_Controller.BattleController.newTurn(this);


                    //Check for loss
                    if (pUnits.Count <= 0)
                    {
                        System.Diagnostics.Debug.Print("Player Lost!");
                        winState = WinLossState.Loss;
                    }
                }

                oldKeyboardState = keyboardState;
            }
        }
        

        /// <summary>
        /// Draws the level to the screen.
        /// </summary>
        /// <param name="spriteBatch">SpriteBatch from main class.</param>
        public void draw(SpriteBatch spriteBatch)
        {

            for (int i = showStartX; i < showStartX + numTilesShowing; i++)
            {
                for (int j = showStartY; j < showStartY + numTilesShowing; j++)
                {
                    Texture2D land;
                    if (board[i, j].LandType.Equals("water"))
                    {
                        land = tileImages[1];
                    }
                    else if (board[i, j].LandType.Equals("stone_wall"))
                    {
                        land = tileImages[2];
                    }
                    else
                    {
                        land = tileImages[0];
                    }
                    spriteBatch.Draw(land, new Rectangle(MAP_START_W + TILE_SIZE * (i-showStartX), MAP_START_H + TILE_SIZE * (j-showStartY), TILE_SIZE, TILE_SIZE), Color.White);


                    //Draws a semi-transparent tile to show available spaces for movement/attacking/aoe
                    if (board[i, j].HlState == HighlightState.MOVE)
                    {
                        spriteBatch.Draw(highlightImages[0], new Rectangle(MAP_START_W + TILE_SIZE * (i - showStartX), MAP_START_H + TILE_SIZE * (j - showStartY), TILE_SIZE, TILE_SIZE), Color.White);
                    }
                    else if(board[i, j].HlState == HighlightState.ACTION)
                    {
                        spriteBatch.Draw(highlightImages[1], new Rectangle(MAP_START_W + TILE_SIZE * (i - showStartX), MAP_START_H + TILE_SIZE * (j - showStartY), TILE_SIZE, TILE_SIZE), Color.White);
                    }
                    else if (board[i, j].HlState == HighlightState.AOE)
                    {
                        spriteBatch.Draw(highlightImages[2], new Rectangle(MAP_START_W + TILE_SIZE * (i - showStartX), MAP_START_H + TILE_SIZE * (j - showStartY), TILE_SIZE, TILE_SIZE), Color.White);
                    }

                    //if (board[i, j].IsHighlighted)
                    //{
                    //    spriteBatch.Draw(utilityImages[1], new Rectangle(MAP_START_W + TILE_SIZE * (i - showStartX), MAP_START_H + TILE_SIZE * (j - showStartY), TILE_SIZE, TILE_SIZE), Color.White);
                    //}

                    //if (board[i, j].Occupant != null)
                    //{
                    //    Character c = board[i, j].Occupant;
                    //    String charTexture = board[i, j].Occupant.IsEnemy ? "enemy" + c.Texture : "player" + c.Texture;
                    //    spriteBatch.Draw(characterImages[charTexture], new Rectangle(MAP_START_W + TILE_SIZE * ((int)c.Pos.X - showStartX), MAP_START_H + TILE_SIZE * ((int)c.Pos.Y - showStartY), TILE_SIZE, TILE_SIZE), Color.White);

                    //    //Draw box around character if selected
                    //    if (board[(int)c.Pos.X, (int)c.Pos.Y].IsSelected)
                    //    {
                    //        spriteBatch.Draw(utilityImages[1], new Rectangle(MAP_START_W + TILE_SIZE * ((int)c.Pos.X - showStartX), MAP_START_H + TILE_SIZE * ((int)c.Pos.Y - showStartY), TILE_SIZE, TILE_SIZE), Color.White);
                    //    }
                    //}

                }
            }
            
            //Draw characters
            //This is no longer done in the loop to allow the movement animation to work.
            foreach (Character p in pUnits)
            {
                spriteBatch.Draw(characterImages["player" + p.Texture], new Rectangle(MAP_START_W + TILE_SIZE * ((int)p.Pos.X - showStartX), MAP_START_H + TILE_SIZE * ((int)p.Pos.Y - showStartY), TILE_SIZE, TILE_SIZE), Color.White);
                //Draw box around character if selected
                if (board[(int)p.Pos.X, (int)p.Pos.Y].IsSelected)
                {
                    spriteBatch.Draw(utilityImages[1], new Rectangle(MAP_START_W + TILE_SIZE * ((int)p.Pos.X - showStartX), MAP_START_H + TILE_SIZE * ((int)p.Pos.Y - showStartY), TILE_SIZE, TILE_SIZE), Color.White);
                }
            }
            foreach(Character e in eUnits)
            {
                spriteBatch.Draw(characterImages["enemy" + e.Texture], new Rectangle(MAP_START_W + TILE_SIZE * ((int)e.Pos.X - showStartX), MAP_START_H + TILE_SIZE * ((int)e.Pos.Y - showStartY), TILE_SIZE, TILE_SIZE), Color.White);
                //Draw box around character if selected
                if (board[(int)e.Pos.X, (int)e.Pos.Y].IsSelected)
                {
                    spriteBatch.Draw(utilityImages[1], new Rectangle(MAP_START_W + TILE_SIZE * ((int)e.Pos.X - showStartX), MAP_START_H + TILE_SIZE * ((int)e.Pos.Y - showStartY), TILE_SIZE, TILE_SIZE), Color.White);
                }
            }


            //Draws a box around a selected tile
            if (board[(int)selectedPos.X, (int)selectedPos.Y].IsSelected)
            {
                spriteBatch.Draw(utilityImages[1], new Rectangle(MAP_START_W + TILE_SIZE * ((int)selectedPos.X - showStartX), MAP_START_H + TILE_SIZE * ((int)selectedPos.Y - showStartY), TILE_SIZE, TILE_SIZE), Color.White);
            }

            //Draw cursor on top of board.
            cursor.draw(spriteBatch);

            //Draw HUD
            hud.Draw(spriteBatch);

        }

        public static void KillUnit(Character c)
        {
            Sound.PlaySound("death");
        }

        private bool CheckKey(Keys theKey)
        {
            return keyboardState.IsKeyUp(theKey) &&
                oldKeyboardState.IsKeyDown(theKey);
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
        public int ShowStartX
        {
            get { return showStartX; }
            set { showStartX = value; }
        }
        public int ShowStartY
        {
            get { return showStartY; }
            set { showStartY = value; }
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
            get { return cursorMoveDelay; }
            set { cursorMoveDelay = value; }
        }
        public float MoveTimeElapsed
        {
            get { return moveTimeElapsed; }
            set { moveTimeElapsed = value; }
        }
        public ArrayList PUnits
        {
            get { return pUnits; }
            set { pUnits = value; }
        }
        public ArrayList EUnits
        {
            get { return eUnits; }
            set { eUnits = value; }
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
        public WinLossState WinState
        {
            get { return winState; }
            set { winState = value; }
        }
        public int NumTilesShowing
        {
            get { return numTilesShowing; }
            set { numTilesShowing = value; }
        }
        public Battle_Controller.Action SelectedAction
        {
            get { return selectedAction; }
            set { selectedAction = value; }
        }
        public Battle_Controller.Action PrevselectedAction
        {
            get { return prevSelectedAction; }
            set { prevSelectedAction = value; }
        }
        public HashSet<Character> TargetList
        {
            get { return targetList; }
            set { targetList = value; }
        }
        public int Turn
        {
            get { return turn; }
            set { turn = value; }
        }

        public int PlayerUnitCount
        {
            get { return playerUnitCount; }
            set { playerUnitCount = value; }
        }
        public int NumPlaceableSpaces
        {
            get { return numPlaceableSpaces; }
            set { numPlaceableSpaces = value; }
        }
        public int ItemIndex
        {
            get { return itemIndex; }
            set { itemIndex = value; }
        }
        public ArrayList AllCharacters
        {
            get { return allCharacters; }
            set { allCharacters = value; }
        }
    }
}