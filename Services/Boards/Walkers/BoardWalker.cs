using Contracts.Board.ViewModels;
using Contracts.Boards;
using Contracts.Boards.ViewModels;
using Services.Boards.Utils;
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
        private readonly AdditionalDisksCounter _additionalDisksCounter;        

        private List<Point> visitedPoints;        

        public BoardWalker(Board board, List<Point> visitedPoints)
        {                    
            _board = board;
            this.visitedPoints = visitedPoints;
            _additionalDisksCounter = new AdditionalDisksCounter(_board);            
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
                if (IsAlreadyVisited(x, point.Y, out Point visitedPosition))
                    return visitedPosition;                

                if (BoardUtils.IsEmptyTile(x, point.Y))
                    break;

                if (BoardUtils.IsMyColor(x, point.Y))
                {
                    localPosition.Scores = 0;
                    return localPosition;
                }

                localPosition.Scores++;
            }

            localPosition.X = x;
            localPosition.Y = point.Y;

            var except = new List<Action<Point>>() { _additionalDisksCounter.OnlyGoUp };
            _additionalDisksCounter.Count(localPosition, except);

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
                if (IsAlreadyVisited(x, point.Y, out Point visitedPosition))
                    return visitedPosition;                

                if (BoardUtils.IsEmptyTile(x, point.Y))
                    break;

                if (BoardUtils.IsMyColor(x, point.Y))
                {
                    localPosition.Scores = 0;
                    return localPosition;
                }

                localPosition.Scores++;
            }

            localPosition.X = x;
            localPosition.Y = point.Y;
            var except = new List<Action<Point>>() { _additionalDisksCounter.OnlyGoDown };
            _additionalDisksCounter.Count(localPosition, except);

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
                if (IsAlreadyVisited(point.X, y, out Point visitedPosition))
                    return visitedPosition;                

                if (BoardUtils.IsEmptyTile(point.X, y))
                    break;

                if (BoardUtils.IsMyColor(point.X, y))
                {
                    localPosition.Scores = 0;
                    return localPosition;
                }

                localPosition.Scores++;
            }

            localPosition.X = point.X;
            localPosition.Y = y;

            var except = new List<Action<Point>>() { _additionalDisksCounter.OnlyGoRight };
            _additionalDisksCounter.Count(localPosition, except);

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
                if (IsAlreadyVisited(point.X, y, out Point visitedPosition))
                    return visitedPosition;                

                if (BoardUtils.IsEmptyTile(point.X, y))
                    break;

                if (BoardUtils.IsMyColor(point.X, y))
                {
                    localPosition.Scores = 0;
                    return localPosition;
                }

                localPosition.Scores++;
            }

            localPosition.X = point.X;
            localPosition.Y = y;
            var except = new List<Action<Point>>() { _additionalDisksCounter.OnlyGoLeft };
            _additionalDisksCounter.Count(localPosition, except);

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
                if (IsAlreadyVisited(x, y, out Point visitedPosition))
                    return visitedPosition;                

                if (BoardUtils.IsEmptyTile(x, y))
                    break;

                if (BoardUtils.IsMyColor(x, y))
                {
                    localPosition.Scores = 0;
                    return localPosition;
                }

                localPosition.Scores++;

                x--;
                y--;
            }

            localPosition.X = x;
            localPosition.Y = y;
            var except = new List<Action<Point>>() { _additionalDisksCounter.OnlyGoDownAndRight };
            _additionalDisksCounter.Count(localPosition, except);

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
                if (IsAlreadyVisited(x, y, out Point visitedPosition))
                    return visitedPosition;                

                if (BoardUtils.IsEmptyTile(x, y))
                    break;

                if (BoardUtils.IsMyColor(x, y))
                {
                    localPosition.Scores = 0;
                    return localPosition;
                }

                localPosition.Scores++;

                x--;
                y++;
            }

            localPosition.X = x;
            localPosition.Y = y;
            var except = new List<Action<Point>>() { _additionalDisksCounter.OnlyGoDownAndLeft };
            _additionalDisksCounter.Count(localPosition, except);
            
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
                if (IsAlreadyVisited(x, y, out Point visitedPosition))
                    return visitedPosition;
                
                if (BoardUtils.IsEmptyTile(x, y))
                    break;

                if (BoardUtils.IsMyColor(x, y))
                {
                    localPosition.Scores = 0;
                    return localPosition;
                }

                localPosition.Scores++;

                x++;
                y--;
            }

            localPosition.X = x;
            localPosition.Y = y;
            var except = new List<Action<Point>>() { _additionalDisksCounter.OnlyGoUpAndRight };
            _additionalDisksCounter.Count(localPosition, except);

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
                if (IsAlreadyVisited(x, y, out Point visitedPosition))
                    return visitedPosition;                

                if (BoardUtils.IsEmptyTile(x, y))
                    break;

                if (BoardUtils.IsMyColor(x, y))
                {
                    localPosition.Scores = 0;
                    return localPosition;
                }

                localPosition.Scores++;

                x++;
                y++;
            }

            localPosition.X = x;
            localPosition.Y = y;
            var except = new List<Action<Point>>() { _additionalDisksCounter.OnlyGoUpAndLeft };
            _additionalDisksCounter.Count(localPosition, except);

            visitedPoints.Add(localPosition);
            return localPosition;
        }   

        private bool IsAlreadyVisited(byte x, byte y, out Point position)
        {
            position = visitedPoints.FirstOrDefault(p => p.X == x && p.Y == y);
            if (position == null)
                return false;
            
            return true;
        }
    }
}
