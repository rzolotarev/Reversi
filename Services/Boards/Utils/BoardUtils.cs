using Contracts.Board.ViewModels;
using Contracts.Boards;
using Contracts.Boards.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Boards.Utils
{
    public static class BoardUtils
    {
        private static Board tileBoard;

        private static List<Point> possiblePoints;

        public static void SetBoard(Board board)
        {
            tileBoard = board;
        }

        public static bool IsEmptyTile(byte x, byte y)
        {
            var color = tileBoard.GetColor(x, y);
            return color == Color.Empty;
        }

        public static bool IsMyColor(byte x, byte y)
        {
            var color = tileBoard.GetColor(x, y);
            return color == Color.Red;
        }

        public static List<Point> GetPossiblePosition()
        {
            possiblePoints = new List<Point>();

            for (byte row = 0; row < tileBoard.GetRows; row++)
                for (byte col = 0; col < tileBoard.GetColumns; col++)
                {
                    if (tileBoard.GetColor(row, col) != Color.Black)
                        continue;

                    Point point;
                    if (TryToGetLow(row, col, out point))
                        TryToAddPossiblePoint(point);

                    if (TryToGetUp(row, col, out point))
                        TryToAddPossiblePoint(point);

                    if(TryToLeft(row, col, out point))
                        TryToAddPossiblePoint(point);

                    if(TryToRight(row, col, out point))
                        TryToAddPossiblePoint(point);

                    if (TryToGetRightLow(row, col, out point))
                        TryToAddPossiblePoint(point);

                    if (TryToGetLeftLow(row, col, out point))
                        TryToAddPossiblePoint(point);                    

                    if (TryToGetRightUp(row, col, out point))
                        TryToAddPossiblePoint(point);

                    if (TryToGetLeftUp(row, col, out point))
                        TryToAddPossiblePoint(point);          
                }

            return possiblePoints;
        }

        private static bool TryToLeft(byte row, byte col, out Point point)
        {
            point = null;

            if (col > 0)
            {
                if (tileBoard.GetColor(row, (byte)(col - 1)) == Color.Empty)
                {
                    point = new Point(row, (byte)(col - 1));
                    return true;
                }
            }

            return false;
        }

        private static bool TryToRight(byte row, byte col, out Point point)
        {
            point = null;

            if (col < tileBoard.GetColumns - 1)
            {
                if (tileBoard.GetColor(row, (byte)(col + 1)) == Color.Empty)
                {
                    point = new Point(row, (byte)(col + 1));
                    return true;
                }
            }

            return false;
        }

        private static bool TryToGetRightUp(byte row, byte col, out Point point)
        {
            point = null;

            if (row > 0 && col < tileBoard.GetColumns - 1)
            {
                if (tileBoard.GetColor((byte)(row - 1), (byte)(col + 1)) == Color.Empty)
                {
                    point = new Point((byte)(row - 1), (byte)(col + 1));
                    return true;
                }
            }

            return false;
        }

        private static bool TryToGetLeftUp(byte row, byte col, out Point point)
        {
            point = null;

            if (row > 0 && col > 0)
            {
                if (tileBoard.GetColor((byte)(row - 1), (byte)(col - 1)) == Color.Empty)
                {
                    point = new Point((byte)(row - 1), (byte)(col - 1));
                    return true;
                }
            }

            return false;
        }

        private static bool TryToGetUp(byte row, byte col, out Point point)
        {
            point = null;

            if (row > 0)
            {
                if (tileBoard.GetColor((byte)(row - 1), col) == Color.Empty)
                {
                    point = new Point((byte)(row - 1), col);
                    return true;
                }
            }

            return false;
        }

        private static bool TryToGetLow(byte row, byte col, out Point point)
        {
            point = null;

            if (row < tileBoard.GetRows - 1)
            {
                if (tileBoard.GetColor((byte)(row + 1), col) == Color.Empty)
                {
                    point = new Point((byte)(row + 1), col);
                    return true;
                }
            }

            return false;
        }

        private static bool TryToGetLeftLow(byte row, byte col, out Point point)
        {
            point = null;

            if (row < tileBoard.GetRows - 1 && col > 0)
            {
                if (tileBoard.GetColor((byte)(row + 1), (byte)(col - 1)) == Color.Empty)
                {
                    point = new Point((byte)(row + 1), (byte)(col - 1));
                    return true;
                }
            }

            return false;
        }

        private static bool TryToGetRightLow(byte row, byte col, out Point point)
        {
            point = null;

            if (row < tileBoard.GetRows - 1 && col < tileBoard.GetColumns - 1)
            {
                if (tileBoard.GetColor((byte)(row + 1), (byte)(col + 1)) == Color.Empty)
                {
                    point = new Point((byte)(row + 1), (byte)(col + 1));
                    return true;
                }
            }

            return false;
        }

        private static void TryToAddPossiblePoint(Point point)
        {
            var existingElement = possiblePoints.FirstOrDefault(p => p.X == point.X && p.Y == point.Y);
            if (existingElement != null)
                return;

            possiblePoints.Add(point);
        }
    }
}
