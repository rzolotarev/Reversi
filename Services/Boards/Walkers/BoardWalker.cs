using Contracts.Board.ViewModels;
using Contracts.Boards;
using Contracts.Boards.ViewModels;
using Services.Boards.Walkers;
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
        private readonly AdditionalDisksCounter _forwardWalker;

        public BoardWalker(Board board)
        {        
            _board = board;
            _forwardWalker = new AdditionalDisksCounter(_board);
        }

        public Point BestStep(Point currentPoint)
        {
            return new List<Point>() {
                GoDown(currentPoint),
                GoUp(currentPoint),
                GoLeft(currentPoint),
                GoRight(currentPoint),
                GoUpAndLeft(currentPoint),
                GoDownAndLeft(currentPoint),
                GoUpAndRight(currentPoint),
                GoDownAndRight(currentPoint),
            }
            .OrderByDescending(x => x.Scores).First();            
        }

        public Point GoDown(Point point)
        {
            var localBestPosition = new Point(0,0,0);
            if (point.X == _board.GetRows - 1)
                return localBestPosition;

            byte x = 0;
            for (x = (byte)(point.X + 1); x < _board.GetRows; x++)
            {
                var color = _board.GetColor(x, point.Y);

                if (IsEmpty(color))
                    break;

                if (!IsMyColor(color, localBestPosition))
                    return localBestPosition;

                localBestPosition.Scores++;
            }

            localBestPosition.X = x;
            localBestPosition.Y = point.Y;

            var except = new List<Action<Point>>() { _forwardWalker.OnlyGoUp };
            _forwardWalker.Count(localBestPosition, except);            

            return localBestPosition;
        }

        public Point GoUp(Point point)
        {
            var localBestPosition = new Point(0, 0, 0);

            if (point.X == 0)
                return localBestPosition;

            byte x;
            for (x = (byte)(point.X - 1); x >= 0; x--)
            {
                var color = _board.GetColor(x, point.Y);

                if (IsEmpty(color))
                    break;

                if (!IsMyColor(color, localBestPosition))
                    return localBestPosition;

                localBestPosition.Scores++;
            }

            localBestPosition.X = x;
            localBestPosition.Y = point.Y;
            var except = new List<Action<Point>>() { _forwardWalker.OnlyGoDown };
            _forwardWalker.Count(localBestPosition, except);            

            return localBestPosition;
        }

        public Point GoLeft(Point point)
        {
            var localBestPosition = new Point(0, 0, 0);

            if (point.Y == 0)
                return localBestPosition;

            byte y;
            for (y = (byte)(point.Y - 1); y >= 0; y--)
            {
                var color = _board.GetColor(point.X, y);

                if (IsEmpty(color))
                    break;

                if (!IsMyColor(color, localBestPosition))
                    return localBestPosition;

                localBestPosition.Scores++;
            }

            localBestPosition.X = point.X;
            localBestPosition.Y = y;

            var except = new List<Action<Point>>() { _forwardWalker.OnlyGoRight };
            _forwardWalker.Count(localBestPosition, except);

            return localBestPosition;
        }

        public Point GoRight(Point point)
        {
            var localBestPosition = new Point(0, 0, 0);

            if (point.Y == _board.GetColumns - 1)
                return localBestPosition;

            byte y;
            for (y = (byte)(point.Y + 1); y < _board.GetColumns; y++)
            {
                var color = _board.GetColor(point.X, y);

                if (IsEmpty(color))
                    break;

                if (!IsMyColor(color, localBestPosition))
                    return localBestPosition;

                localBestPosition.Scores++;
            }

            localBestPosition.X = point.X;
            localBestPosition.Y = y;
            var except = new List<Action<Point>>() { _forwardWalker.OnlyGoLeft };
            _forwardWalker.Count(localBestPosition, except);

            return localBestPosition;
        }

        public Point GoUpAndLeft(Point point)
        {
            var localBestPosition = new Point(0,0,0);

            if (point.Y == 0 || point.X == 0)
                return localBestPosition;            

            byte y = (byte)(point.Y - 1);
            byte x = (byte)(point.X - 1);
            while (y >= 0 && x >= 0)
            {
                var color = _board.GetColor(x, y);

                if (IsEmpty(color))
                    break;

                if (!IsMyColor(color, localBestPosition))
                    return localBestPosition;

                localBestPosition.Scores++;

                x--;
                y--;
            }

            localBestPosition.X = x;
            localBestPosition.Y = y;
            var except = new List<Action<Point>>() { _forwardWalker.OnlyGoDownAndRight };
            _forwardWalker.Count(localBestPosition, except);

            return localBestPosition;
        }

        public Point GoUpAndRight(Point point)
        {
            var localBestPosition = new Point(0, 0, 0);

            if (point.Y == _board.GetColumns - 1 || point.X == 0)
                return localBestPosition;            

            byte y = (byte)(point.Y + 1);
            byte x = (byte)(point.X - 1);
            while (y < _board.GetColumns && x >= 0)
            {
                var color = _board.GetColor(x, y);

                if (IsEmpty(color))
                    break;

                if (!IsMyColor(color, localBestPosition))
                    return localBestPosition;

                localBestPosition.Scores++;

                x--;
                y++;
            }

            localBestPosition.X = x;
            localBestPosition.Y = y;
            var except = new List<Action<Point>>() { _forwardWalker.OnlyGoDownAndLeft };
            _forwardWalker.Count(localBestPosition, except);

            return localBestPosition;
        }

        public Point GoDownAndLeft(Point point)
        {
            var localBestPosition = new Point(0,0,0);

            if (point.Y == 0 || point.X == _board.GetRows - 1)
                return localBestPosition;            

            byte y = (byte)(point.Y - 1);
            byte x = (byte)(point.X + 1);
            while (y >= 0 && x < _board.GetRows)
            {
                var color = _board.GetColor(x, y);
                if (IsEmpty(color))
                    break;

                if (!IsMyColor(color, localBestPosition))
                    return localBestPosition;

                localBestPosition.Scores++;

                x++;
                y--;
            }

            localBestPosition.X = x;
            localBestPosition.Y = y;
            var except = new List<Action<Point>>() { _forwardWalker.OnlyGoUpAndRight };
            _forwardWalker.Count(localBestPosition, except);

            return localBestPosition;
        }

        public Point GoDownAndRight(Point point)
        {
            var localBestPosition = new Point(0, 0, 0);

            if (point.Y == _board.GetColumns - 1 || point.X == _board.GetRows - 1)
                return localBestPosition;            

            byte y = (byte)(point.Y + 1);
            byte x = (byte)(point.X + 1);
            while (y < _board.GetColumns && x < _board.GetRows)
            {
                var color = _board.GetColor(x, y);

                if (IsEmpty(color))
                    break;

                if (!IsMyColor(color, localBestPosition))
                    return localBestPosition;

                localBestPosition.Scores++;

                x++;
                y++;
            }

            localBestPosition.X = x;
            localBestPosition.Y = y;
            var except = new List<Action<Point>>() { _forwardWalker.OnlyGoUpAndLeft };
            _forwardWalker.Count(localBestPosition, except);

            return localBestPosition;
        }

        private bool IsEmpty(Color color)
        {
            return color == Color.Empty;                
        }

        private bool IsMyColor(Color color, Point localBestPosition)
        {
            if (color == Color.Red)
            {
                localBestPosition.Scores = 0;
                return false;
            }
            
            return true;
        }
    }
}
