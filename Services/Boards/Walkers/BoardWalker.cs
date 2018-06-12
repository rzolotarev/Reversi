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
        private List<Point> visitedPoints;
        public int counter = 0;

        public BoardWalker(Board board, List<Point> visitedPoints)
        {                    
            _board = board;
            this.visitedPoints = visitedPoints;
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
            var localPosition = new Point(0,0,0);
            if (point.X == _board.GetRows - 1)
                return localPosition;

            byte x = 0;
            for (x = (byte)(point.X + 1); x < _board.GetRows; x++)
            {
                if (IsAlreadyPassed(x, point.Y, out Point visitedPosition))
                    return visitedPosition;

                var color = _board.GetColor(x, point.Y);

                if (IsEmpty(color))
                    break;

                if (!IsMyColor(color, localPosition))
                    return localPosition;

                localPosition.Scores++;
            }

            localPosition.X = x;
            localPosition.Y = point.Y;

            var except = new List<Action<Point>>() { _forwardWalker.OnlyGoUp };
            _forwardWalker.Count(localPosition, except);

            visitedPoints.Add(localPosition);
            return localPosition;
        }

        public Point GoUp(Point point)
        {
            var localPosition = new Point(0, 0, 0);

            if (point.X == 0)
                return localPosition;

            byte x;
            for (x = (byte)(point.X - 1); x >= 0; x--)
            {
                if (IsAlreadyPassed(x, point.Y, out Point visitedPosition))
                    return visitedPosition;

                var color = _board.GetColor(x, point.Y);

                if (IsEmpty(color))
                    break;

                if (!IsMyColor(color, localPosition))
                    return localPosition;

                localPosition.Scores++;
            }

            localPosition.X = x;
            localPosition.Y = point.Y;
            var except = new List<Action<Point>>() { _forwardWalker.OnlyGoDown };
            _forwardWalker.Count(localPosition, except);

            visitedPoints.Add(localPosition);
            return localPosition;
        }

        public Point GoLeft(Point point)
        {
            var localPosition = new Point(0, 0, 0);

            if (point.Y == 0)
                return localPosition;

            byte y;
            for (y = (byte)(point.Y - 1); y >= 0; y--)
            {
                if (IsAlreadyPassed(point.X, y, out Point visitedPosition))
                    return visitedPosition;

                var color = _board.GetColor(point.X, y);

                if (IsEmpty(color))
                    break;

                if (!IsMyColor(color, localPosition))
                    return localPosition;

                localPosition.Scores++;
            }

            localPosition.X = point.X;
            localPosition.Y = y;

            var except = new List<Action<Point>>() { _forwardWalker.OnlyGoRight };
            _forwardWalker.Count(localPosition, except);

            visitedPoints.Add(localPosition);
            return localPosition;
        }

        public Point GoRight(Point point)
        {
            var localPosition = new Point(0, 0, 0);

            if (point.Y == _board.GetColumns - 1)
                return localPosition;

            byte y;
            for (y = (byte)(point.Y + 1); y < _board.GetColumns; y++)
            {
                if (IsAlreadyPassed(point.X, y, out Point visitedPosition))
                    return visitedPosition;

                var color = _board.GetColor(point.X, y);

                if (IsEmpty(color))
                    break;

                if (!IsMyColor(color, localPosition))
                    return localPosition;

                localPosition.Scores++;
            }

            localPosition.X = point.X;
            localPosition.Y = y;
            var except = new List<Action<Point>>() { _forwardWalker.OnlyGoLeft };
            _forwardWalker.Count(localPosition, except);

            visitedPoints.Add(localPosition);
            return localPosition;
        }

        public Point GoUpAndLeft(Point point)
        {
            var localPosition = new Point(0,0,0);

            if (point.Y == 0 || point.X == 0)
                return localPosition;            

            byte y = (byte)(point.Y - 1);
            byte x = (byte)(point.X - 1);
            while (y >= 0 && x >= 0)
            {
                if (IsAlreadyPassed(x, y, out Point visitedPosition))
                    return visitedPosition;

                var color = _board.GetColor(x, y);

                if (IsEmpty(color))
                    break;

                if (!IsMyColor(color, localPosition))
                    return localPosition;

                localPosition.Scores++;

                x--;
                y--;
            }

            localPosition.X = x;
            localPosition.Y = y;
            var except = new List<Action<Point>>() { _forwardWalker.OnlyGoDownAndRight };
            _forwardWalker.Count(localPosition, except);

            visitedPoints.Add(localPosition);
            return localPosition;
        }

        public Point GoUpAndRight(Point point)
        {
            var localPosition = new Point(0, 0, 0);

            if (point.Y == _board.GetColumns - 1 || point.X == 0)
                return localPosition;            

            byte y = (byte)(point.Y + 1);
            byte x = (byte)(point.X - 1);
            while (y < _board.GetColumns && x >= 0)
            {
                if (IsAlreadyPassed(x, y, out Point visitedPosition))
                    return visitedPosition;

                var color = _board.GetColor(x, y);

                if (IsEmpty(color))
                    break;

                if (!IsMyColor(color, localPosition))
                    return localPosition;

                localPosition.Scores++;

                x--;
                y++;
            }

            localPosition.X = x;
            localPosition.Y = y;
            var except = new List<Action<Point>>() { _forwardWalker.OnlyGoDownAndLeft };
            _forwardWalker.Count(localPosition, except);
            
            visitedPoints.Add(localPosition);
            return localPosition;
        }

        public Point GoDownAndLeft(Point point)
        {
            var localPosition = new Point(0,0,0);

            if (point.Y == 0 || point.X == _board.GetRows - 1)
                return localPosition;            

            byte y = (byte)(point.Y - 1);
            byte x = (byte)(point.X + 1);
            while (y >= 0 && x < _board.GetRows)
            {
                if (IsAlreadyPassed(x, y, out Point visitedPosition))
                    return visitedPosition;

                var color = _board.GetColor(x, y);
                if (IsEmpty(color))
                    break;

                if (!IsMyColor(color, localPosition))
                    return localPosition;

                localPosition.Scores++;

                x++;
                y--;
            }

            localPosition.X = x;
            localPosition.Y = y;
            var except = new List<Action<Point>>() { _forwardWalker.OnlyGoUpAndRight };
            _forwardWalker.Count(localPosition, except);

            visitedPoints.Add(localPosition);
            return localPosition;
        }

        public Point GoDownAndRight(Point point)
        {
            var localPosition = new Point(0, 0, 0);

            if (point.Y == _board.GetColumns - 1 || point.X == _board.GetRows - 1)
                return localPosition;            

            byte y = (byte)(point.Y + 1);
            byte x = (byte)(point.X + 1);
            while (y < _board.GetColumns && x < _board.GetRows)
            {
                if (IsAlreadyPassed(x, y, out Point visitedPosition))
                    return visitedPosition;

                var color = _board.GetColor(x, y);

                if (IsEmpty(color))
                    break;

                if (!IsMyColor(color, localPosition))
                    return localPosition;

                localPosition.Scores++;

                x++;
                y++;
            }

            localPosition.X = x;
            localPosition.Y = y;
            var except = new List<Action<Point>>() { _forwardWalker.OnlyGoUpAndLeft };
            _forwardWalker.Count(localPosition, except);

            visitedPoints.Add(localPosition);
            return localPosition;
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

        private bool IsAlreadyPassed(byte x, byte y, out Point position)
        {
            position = visitedPoints.FirstOrDefault(p => p.X == x && p.Y == y);
            if (position == null)
                return false;

            counter++;
            return true;
        }
    }
}
