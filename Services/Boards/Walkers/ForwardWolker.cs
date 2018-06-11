using Contracts.Board.ViewModels;
using Contracts.Boards;
using Contracts.Boards.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Boards.Walkers
{
    public class ForwardWolker
    {
        private readonly Board _board;

        public ForwardWolker(Board board)
        {
            _board = board;
        }

        public void OnlyGoDown(Point localBestPosition)
        {
            if (localBestPosition.X == _board.GetRows - 1)
                return;

            byte x = 0;
            for (x = (byte)(localBestPosition.X + 1); x < _board.GetRows; x++)
            {
                var color = _board.GetColor(x, localBestPosition.Y);
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
        }

        public void OnlyGoUp(Point localBestPosition)
        {
            if (localBestPosition.X == 0)
                return;
            byte x;
            for (x = (byte)(localBestPosition.X - 1); x >= 0; x--)
            {
                var color = _board.GetColor(x, localBestPosition.Y);
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
        }

        public void OnlyToLeft(Point localBestPosition)
        {
            if (localBestPosition.Y == 0)
                return;

            for (byte y = (byte)(localBestPosition.Y - 1); y >= 0; y--)
            {
                var color = _board.GetColor(localBestPosition.X, y);
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
        }

        public void OnlyToRight(Point localBestPosition)
        {
            if (localBestPosition.Y == _board.GetColumns - 1)
                return;

            for (byte y = (byte)(localBestPosition.Y + 1); y < _board.GetColumns; y++)
            {
                var color = _board.GetColor(localBestPosition.X, y);
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
        }   
        

        public void OnlyGoUpAndLeft(Point localBestPosition)
        {           
            if (localBestPosition.Y == 0 || localBestPosition.X == 0)
                return;

            byte y = (byte)(localBestPosition.Y - 1);
            byte x = (byte)(localBestPosition.X - 1);
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
        }

        public void OnlyGoUpAndRight(Point localBestPosition)
        {
            if (localBestPosition.Y == _board.GetColumns - 1 || localBestPosition.X == 0)
                return;

            byte y = (byte)(localBestPosition.Y + 1);
            byte x = (byte)(localBestPosition.X - 1);
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
        }

        public void OnlyGoDownAndLeft(Point localBestPosition)
        {
            if (localBestPosition.Y == 0 || localBestPosition.X == _board.GetRows - 1)
                return;

            byte y = (byte)(localBestPosition.Y - 1);
            byte x = (byte)(localBestPosition.X + 1);
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
        }

        public void OnlyGoDownAndRight(Point localBestPosition)
        {
            if (localBestPosition.Y == _board.GetColumns - 1 || localBestPosition.X == _board.GetRows - 1)
                return;

            byte y = (byte)(localBestPosition.Y + 1);
            byte x = (byte)(localBestPosition.X + 1);
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
        }
    }
}
