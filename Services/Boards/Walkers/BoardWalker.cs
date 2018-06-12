using Contracts.Board.ViewModels;
using Contracts.Boards;
using Contracts.Boards.ViewModels;
using Services.Boards.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Boards
{
    public class BoardWalker
    {
        private readonly Board _board;
        private readonly List<Action<Point>> _actions;                      

        public BoardWalker(Board board, List<Point> visitedPoints)
        {                    
            _board = board;
            _actions = new List<Action<Point>>()
            {
                OnlyGoDown,
                OnlyGoUp,
                OnlyGoLeft,
                OnlyGoRight,
                OnlyGoUpAndLeft,
                OnlyGoUpAndRight,
                OnlyGoDownAndLeft,
                OnlyGoDownAndRight
            };
        }

        public Point CountScores(Point currentPoint)
        {
            var scoredPoint = new Point(currentPoint.X, currentPoint.Y);

            foreach (var action in _actions)
                action(scoredPoint);

            return scoredPoint;
        }

        private void OnlyGoDown(Point localPosition)
        {
            if (localPosition.X == _board.GetRows - 1)
                return;

            for (var x = (byte)(localPosition.X + 1); x < _board.GetRows; x++)
            {
                if (BoardUtils.IsEmptyTile(x, localPosition.Y))
                {
                    localPosition.AdditionalScores = 0;
                    return;
                }

                if (BoardUtils.IsMyColor(x, localPosition.Y))
                    break;

                localPosition.AdditionalScores++;
            }

            localPosition.Scores += localPosition.AdditionalScores;
            localPosition.AdditionalScores = 0;
        }

        private void OnlyGoUp(Point localPosition)
        {
            if (localPosition.X == 0)
                return;

            for (var x = (byte)(localPosition.X - 1); x >= 0; x--)
            {
                if (BoardUtils.IsEmptyTile(x, localPosition.Y))
                {
                    localPosition.AdditionalScores = 0;
                    return;
                }

                if (BoardUtils.IsMyColor(x, localPosition.Y))
                    break;

                localPosition.AdditionalScores++;
            }

            localPosition.Scores += localPosition.AdditionalScores;
            localPosition.AdditionalScores = 0;
        }

        private void OnlyGoLeft(Point localPosition)
        {
            if (localPosition.Y == 0)
                return;

            for (var y = (byte)(localPosition.Y - 1); y >= 0; y--)
            {
                var color = _board.GetColor(localPosition.X, y);

                if (BoardUtils.IsEmptyTile(localPosition.X, y))
                {
                    localPosition.AdditionalScores = 0;
                    return;
                }

                if (BoardUtils.IsMyColor(localPosition.X, y))
                    break;

                localPosition.AdditionalScores++;
            }

            localPosition.Scores += localPosition.AdditionalScores;
            localPosition.AdditionalScores = 0;
        }

        private void OnlyGoRight(Point localPosition)
        {
            if (localPosition.Y == _board.GetColumns - 1)
                return;

            for (var y = (byte)(localPosition.Y + 1); y < _board.GetColumns; y++)
            {
                if (BoardUtils.IsEmptyTile(localPosition.X, y))
                {
                    localPosition.AdditionalScores = 0;
                    return;
                }

                if (BoardUtils.IsMyColor(localPosition.X, y))
                    break;

                localPosition.AdditionalScores++;
            }

            localPosition.Scores += localPosition.AdditionalScores;
            localPosition.AdditionalScores = 0;
        }

        private void OnlyGoUpAndLeft(Point localPosition)
        {
            if (localPosition.Y == 0 || localPosition.X == 0)
                return;

            byte y = (byte)(localPosition.Y - 1);
            byte x = (byte)(localPosition.X - 1);
            while (y >= 0 && x >= 0)
            {
                if (BoardUtils.IsEmptyTile(x, y))
                {
                    localPosition.AdditionalScores = 0;
                    return;
                }

                if (BoardUtils.IsMyColor(x, y))
                    break;

                localPosition.AdditionalScores++;

                x--; y--;
            }

            localPosition.Scores += localPosition.AdditionalScores;
            localPosition.AdditionalScores = 0;
        }

        private void OnlyGoUpAndRight(Point localPosition)
        {
            if (localPosition.Y == _board.GetColumns - 1 || localPosition.X == 0)
                return;

            byte y = (byte)(localPosition.Y + 1);
            byte x = (byte)(localPosition.X - 1);
            while (y < _board.GetColumns && x >= 0)
            {
                if (BoardUtils.IsEmptyTile(x, y))
                {
                    localPosition.AdditionalScores = 0;
                    return;
                }

                if (BoardUtils.IsMyColor(x, y))
                    break;

                localPosition.AdditionalScores++;

                x--; y++;
            }

            localPosition.Scores += localPosition.AdditionalScores;
            localPosition.AdditionalScores = 0;
        }

        private void OnlyGoDownAndLeft(Point localPosition)
        {
            if (localPosition.Y == 0 || localPosition.X == _board.GetRows - 1)
                return;

            byte y = (byte)(localPosition.Y - 1);
            byte x = (byte)(localPosition.X + 1);
            while (y >= 0 && x < _board.GetRows)
            {
                if (BoardUtils.IsEmptyTile(x, y))
                {
                    localPosition.AdditionalScores = 0;
                    return;
                }

                if (BoardUtils.IsMyColor(x, y))
                    break;

                localPosition.AdditionalScores++;

                x++; y--;
            }

            localPosition.Scores += localPosition.AdditionalScores;
            localPosition.AdditionalScores = 0;
        }

        private void OnlyGoDownAndRight(Point localPosition)
        {
            if (localPosition.Y == _board.GetColumns - 1 || localPosition.X == _board.GetRows - 1)
                return;

            byte y = (byte)(localPosition.Y + 1);
            byte x = (byte)(localPosition.X + 1);

            while (y < _board.GetColumns && x < _board.GetRows)
            {
                if (BoardUtils.IsEmptyTile(x, y))
                {
                    localPosition.AdditionalScores = 0;
                    return;
                }

                if (BoardUtils.IsMyColor(x, y))
                    break;

                localPosition.AdditionalScores++;

                x++; y++;
            }

            localPosition.Scores += localPosition.AdditionalScores;
            localPosition.AdditionalScores = 0;
        }
    }
}
