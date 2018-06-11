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
        private readonly ForwardWolker _forwardWalker;

        public BoardWalker(Board board)
        {        
            _board = board;
            _forwardWalker = new ForwardWolker(_board);
        }

        public Point BestStep(Point currentPoint)
        {            
            var downBestPosition = new Point(0, 0, 0);
            GoDown(currentPoint, downBestPosition);

            var upBestPosition = new Point(0, 0, 0);
            GoUp(currentPoint, upBestPosition);

            var leftBestPosition = new Point(0, 0, 0);
            GoLeft(currentPoint, leftBestPosition);

            var rightBestPosition = new Point(0, 0, 0);
            GoRight(currentPoint, rightBestPosition);

            var upAndLeftBestPosition = new Point(0, 0, 0);
            GoUpAndLeft(currentPoint, upAndLeftBestPosition);

            var downAndLeftBestPosition = new Point(0, 0, 0);
            GoDownAndLeft(currentPoint, downAndLeftBestPosition);

            var upAndRightBestPosition = new Point(0, 0, 0);
            GoUpAndRight(currentPoint, upAndRightBestPosition);

            var downAndRightBestPosition = new Point(0, 0, 0);
            GoDownAndRight(currentPoint, downAndRightBestPosition);

            return new List<Point>() { downBestPosition, upBestPosition, leftBestPosition, rightBestPosition,
                upAndLeftBestPosition, upAndRightBestPosition, downAndLeftBestPosition, downAndRightBestPosition}
            .OrderByDescending(x => x.Scores).First();            
        }

        public void GoDown(Point point, Point localBestPosition)
        {
            if (point.X == _board.GetRows - 1)
                return;

            byte x = 0;
            for (x = (byte)(point.X + 1); x < _board.GetRows; x++)
            {
                var color = _board.GetColor(x, point.Y);
                if (color == Color.Red)                                    
                    return;                
                else
                {
                    if (color == Color.Black)
                        localBestPosition.Scores++;
                    else                  
                        break;                    
                }
            }

            localBestPosition.X = x;
            localBestPosition.Y = point.Y;
            _forwardWalker.OnlyToLeft(localBestPosition);
            _forwardWalker.OnlyToRight(localBestPosition);
            _forwardWalker.OnlyGoUpAndLeft(localBestPosition);
            _forwardWalker.OnlyGoUpAndRight(localBestPosition);
        }

        public void GoUp(Point point, Point localBestPosition)
        {
            if (point.X == 0)
                return;
            byte x;
            for (x = (byte)(point.X - 1); x >= 0; x--)
            {
                var color = _board.GetColor(x, point.Y);
                if (color == Color.Red)
                    return;
                else
                {
                    if (color == Color.Black)
                        localBestPosition.Scores++;
                    else                  
                        break;                                           
                }
            }

            localBestPosition.X = x;
            localBestPosition.Y = point.Y;
            _forwardWalker.OnlyToLeft(localBestPosition);
            _forwardWalker.OnlyToRight(localBestPosition);
            _forwardWalker.OnlyGoDownAndLeft(localBestPosition);
            _forwardWalker.OnlyGoDownAndRight(localBestPosition);
        }

        public void GoLeft(Point point, Point localBestPosition)
        {
            if (point.Y == 0)
                return;

            byte y;
            for (y = (byte)(point.Y - 1); y >= 0; y--)
            {
                var color = _board.GetColor(point.X, y);
                if (color == Color.Red)
                    return;
                else
                {
                    if (color == Color.Black)
                        localBestPosition.Scores++;
                    else                                                                 
                        break;                                             
                }
            }

            localBestPosition.X = point.X;
            localBestPosition.Y = y;
            _forwardWalker.OnlyGoUp(localBestPosition);
            _forwardWalker.OnlyGoDown(localBestPosition);
            _forwardWalker.OnlyGoDownAndLeft(localBestPosition);
            _forwardWalker.OnlyGoDownAndRight(localBestPosition);
        }

        public void GoRight(Point point, Point localBestPosition)
        {
            if (point.Y == _board.GetColumns - 1)
                return;

            byte y;
            for (y = (byte)(point.Y + 1); y < _board.GetColumns; y++)
            {
                var color = _board.GetColor(point.X, y);
                if (color == Color.Red)
                    break;
                else
                {
                    if (color == Color.Black)
                        localBestPosition.Scores++;
                    else                 
                        break;                                         
                }
            }

            localBestPosition.X = point.X;
            localBestPosition.Y = y;
            _forwardWalker.OnlyGoUp(localBestPosition);
            _forwardWalker.OnlyGoDown(localBestPosition);
            _forwardWalker.OnlyGoDownAndLeft(localBestPosition);
            _forwardWalker.OnlyGoUpAndLeft(localBestPosition);
        }

        public void GoUpAndLeft(Point point, Point localBestPosition)
        {
            if (point.Y == 0 || point.X == 0)
                return;            

            byte y = (byte)(point.Y - 1);
            byte x = (byte)(point.X - 1);
            while (y >= 0 && x >= 0)
            {
                var color = _board.GetColor(x, y);
                if (color == Color.Red)
                    return;
                else
                {
                    if (color == Color.Black)
                        localBestPosition.Scores++;
                    else
                        break;
                }
                x--;
                y--;
            }

            localBestPosition.X = x;
            localBestPosition.Y = y;
            _forwardWalker.OnlyToRight(localBestPosition);
            _forwardWalker.OnlyGoDown(localBestPosition);            
        }

        public void GoUpAndRight(Point point, Point localBestPosition)
        {
            if (point.Y == _board.GetColumns - 1 || point.X == 0)
                return;            

            byte y = (byte)(point.Y + 1);
            byte x = (byte)(point.X - 1);
            while (y < _board.GetColumns && x >= 0)
            {
                var color = _board.GetColor(x, y);
                if (color == Color.Red)
                    return;
                else
                {
                    if (color == Color.Black)
                        localBestPosition.Scores++;
                    else                    
                        break;                    
                }
                x--;
                y++;
            }

            localBestPosition.X = x;
            localBestPosition.Y = y;
            _forwardWalker.OnlyToLeft(localBestPosition);
            _forwardWalker.OnlyGoDown(localBestPosition);
        }

        public void GoDownAndLeft(Point point, Point localBestPosition)
        {
            if (point.Y == 0 || point.X == _board.GetRows - 1)
                return;            

            byte y = (byte)(point.Y - 1);
            byte x = (byte)(point.X + 1);
            while (y >= 0 && x < _board.GetRows)
            {
                var color = _board.GetColor(x, y);
                if (color == Color.Red)
                    return;
                else
                {
                    if (color == Color.Black)
                        localBestPosition.Scores++;
                    else                                          
                        break;                    
                }
                x++;
                y--;
            }

            localBestPosition.X = x;
            localBestPosition.Y = y;
            _forwardWalker.OnlyGoUp(localBestPosition);
            _forwardWalker.OnlyToRight(localBestPosition);
        }

        public void GoDownAndRight(Point point, Point localBestPosition)
        {
            if (point.Y == _board.GetColumns - 1 || point.X == _board.GetRows - 1)
                return;            

            byte y = (byte)(point.Y + 1);
            byte x = (byte)(point.X + 1);
            while (y < _board.GetColumns && x < _board.GetRows)
            {
                var color = _board.GetColor(x, y);
                if (color == Color.Red)
                    return;
                else
                {
                    if (color == Color.Black)
                        localBestPosition.Scores++;
                    else                                           
                        break;                    
                }
                x++;
                y++;
            }

            localBestPosition.X = x;
            localBestPosition.Y = y;
            _forwardWalker.OnlyGoUp(localBestPosition);
            _forwardWalker.OnlyToLeft(localBestPosition);
        }
    }
}
