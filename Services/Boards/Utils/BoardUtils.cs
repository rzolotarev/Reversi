using Contracts.Board.ViewModels;
using Contracts.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Boards.Utils
{
    public static class BoardUtils
    {
        private static Board Board;

        public static void SetBoard(Board board)
        {
            Board = board;
        }        

        public static bool IsEmptyTile(byte x, byte y)
        {
            var color = Board.GetColor(x, y);
            return color == Color.Empty;
        }

        public static bool IsMyColor(byte x, byte y)
        {
            var color = Board.GetColor(x, y);
            return color == Color.Red;
        }
    }
}
