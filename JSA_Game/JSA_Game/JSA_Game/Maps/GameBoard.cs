using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game
{
    class GameBoard
    {
        private const int DEFAULT_SIZE = 10;

        private int height;
        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        private int width;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        private Tile[,] board;
        public Tile[,] Board
        {
            get { return board; }
            set { board = value; }
        }

        public GameBoard()
        {
            height = DEFAULT_SIZE;
            width = DEFAULT_SIZE;
            board = new Tile[width, height];
            instantiate();
        }

        public GameBoard(int h, int w)
        {
            height = h;
            width = w;
            board = new Tile[width, height];
            instantiate();
        }


        private void instantiate()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    board[i, j] = new Tile();
                }
            }
        }




    }
}
