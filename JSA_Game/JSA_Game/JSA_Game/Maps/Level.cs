using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace JSA_Game.Maps
{
    class Level
    {
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
        GameBoard board;
        public GameBoard Board
        {
            get { return board; }
            set { board = value; }
        }

        int maxPlayerUnits, MaxEnemyUnits, playerUnitCount, enemyUnitCount;

        Character[] playerUnits, enemyUnits;
        public Character[] PlayerUnits
        {
            get { return playerUnits; }
            set { playerUnits = value; }
        }
        public Character[] EnemyUnits
        {
            get { return enemyUnits; }
            set { enemyUnits = value; }
        }

        public Level(int width, int height, int numPlayerUnits, int numEnemyUnits)
        {
            boardWidth = width;
            boardHeight = height;
            board = new GameBoard(boardWidth, boardHeight);

            playerUnitCount = 0;
            enemyUnitCount = 0;
            maxPlayerUnits = numPlayerUnits;
            MaxEnemyUnits = numEnemyUnits;
            playerUnits = new Character[maxPlayerUnits];
            enemyUnits = new Character[MaxEnemyUnits];

        }

        public void addUnit(int allyFlag, Character unit, int xPos, int yPos)
        {
            unit.IsEnemy = allyFlag==1 ? true : false;
            if (!unit.IsEnemy && playerUnitCount != maxPlayerUnits)
            {
                unit.Pos = new Vector2(xPos, yPos);
                playerUnits[playerUnitCount] = unit;
                playerUnitCount++;
                board.Board[xPos, yPos].IsOccupied = true;
            }

            else if (unit.IsEnemy && enemyUnitCount != MaxEnemyUnits)
            {
                unit.Pos = new Vector2(xPos, yPos);
                enemyUnits[enemyUnitCount] = unit;
                enemyUnitCount++;
                board.Board[xPos, yPos].IsOccupied = true;
            }
            else
            {
                System.Diagnostics.Debug.Print("Level.addUnit() : Not enough space in level to place unit.");
            }
        }
    }
}
