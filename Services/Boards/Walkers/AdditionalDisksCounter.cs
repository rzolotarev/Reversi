using Contracts.Board.ViewModels;
using Contracts.Boards;
using Contracts.Boards.ViewModels;
using Services.Boards.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Boards.Walkers
{
    public class AdditionalDisksCounter
    {
        private readonly Board _board;
        
        public AdditionalDisksCounter(Board board)
        {
            _board = board;
        }

        public void Count(Point localPosition)
        {
            var actions = new List<Action<Point>>()
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

            foreach (var action in actions)
                action(localPosition);
        }

        public void OnlyGoDown(Point localPosition)
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

        public void OnlyGoUp(Point localPosition)
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

        public void OnlyGoLeft(Point localPosition)
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

        public void OnlyGoRight(Point localPosition)
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
        
        public void OnlyGoUpAndLeft(Point localPosition)
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

        public void OnlyGoUpAndRight(Point localPosition)
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

        public void OnlyGoDownAndLeft(Point localPosition)
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

        public void OnlyGoDownAndRight(Point localPosition)
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
