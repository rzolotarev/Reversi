
using Contracts.Board.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Boards
{
    public class Board
    {
        private readonly Color[,] _board;

        private readonly byte _width;
        private readonly byte _height;

        public Board(byte width, byte height)
        {
            _width = width;
            _height = height;
            _board = new Color[height, width];
        }

        public void SetColor(byte x, byte y, Color value)
        {
            _board[x, y] = value;
        }

        public Color GetColor(byte x, byte y)
        {
            return _board[x, y];
        }

        public byte GetRows => _height;        

        public byte GetColumns => _width;        
    }
}
