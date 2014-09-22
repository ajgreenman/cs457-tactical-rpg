using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSA_Game
{
    class GameBoard
    {
        private const int DEFAULT_SIZE = 20;
        private int height;

        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        
        private int width {get; set;}
        private Tile[,] board {get; set;}


        public GameBoard()
        {
            height = DEFAULT_SIZE;
            width = DEFAULT_SIZE;
            board = new Tile[height, width];
            instantiate();
        }

        public GameBoard(int h, int w)
        {
            height = h;
            width = w;
            board = new Tile[height, width];
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
