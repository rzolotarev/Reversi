﻿using Contracts.Board.ViewModels;
using Contracts.Boards;
using Contracts.Boards.ViewModels;
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

        public void Count(Point localBestPosition, IEnumerable<Action<Point>> except)
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

            foreach (var action in actions.Except(except))
                action(localBestPosition);
        }

        public void OnlyGoDown(Point localBestPosition)
        {
            if (localBestPosition.X == _board.GetRows - 1)
                return;

            byte x = 0;
            for (x = (byte)(localBestPosition.X + 1); x < _board.GetRows; x++)
            {
                var color = _board.GetColor(x, localBestPosition.Y);

                if (IsEmpty(color, localBestPosition))
                    return;

                if (!TryToScore(color, localBestPosition))
                    break;
            }

            localBestPosition.Scores += localBestPosition.AdditionalScores;
            localBestPosition.AdditionalScores = 0;
        }

        public void OnlyGoUp(Point localBestPosition)
        {
            if (localBestPosition.X == 0)
                return;
            byte x;
            for (x = (byte)(localBestPosition.X - 1); x >= 0; x--)
            {
                var color = _board.GetColor(x, localBestPosition.Y);

                if (IsEmpty(color, localBestPosition))
                    return;

                if (!TryToScore(color, localBestPosition))
                    break;
            }

            localBestPosition.Scores += localBestPosition.AdditionalScores;
            localBestPosition.AdditionalScores = 0;
        }

        public void OnlyGoLeft(Point localBestPosition)
        {
            if (localBestPosition.Y == 0)
                return;

            for (byte y = (byte)(localBestPosition.Y - 1); y >= 0; y--)
            {
                var color = _board.GetColor(localBestPosition.X, y);

                if (IsEmpty(color, localBestPosition))
                    return;

                if (!TryToScore(color, localBestPosition))
                    break;
            }

            localBestPosition.Scores += localBestPosition.AdditionalScores;
            localBestPosition.AdditionalScores = 0;
        }

        public void OnlyGoRight(Point localBestPosition)
        {
            if (localBestPosition.Y == _board.GetColumns - 1)
                return;

            for (byte y = (byte)(localBestPosition.Y + 1); y < _board.GetColumns; y++)
            {
                var color = _board.GetColor(localBestPosition.X, y);

                if (IsEmpty(color, localBestPosition))
                    return;

                if (!TryToScore(color, localBestPosition))
                    break;
            }

            localBestPosition.Scores += localBestPosition.AdditionalScores;
            localBestPosition.AdditionalScores = 0;
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

                if (IsEmpty(color, localBestPosition))
                    return;

                if (!TryToScore(color, localBestPosition))
                    break;

                x--;
                y--;
            }

            localBestPosition.Scores += localBestPosition.AdditionalScores;
            localBestPosition.AdditionalScores = 0;
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

                if (IsEmpty(color, localBestPosition))
                    return;

                if (!TryToScore(color, localBestPosition))
                    break;

                x--;
                y++;
            }
            
            localBestPosition.Scores += localBestPosition.AdditionalScores;
            localBestPosition.AdditionalScores = 0;
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

                if (IsEmpty(color, localBestPosition))
                    return;

                if (!TryToScore(color, localBestPosition))
                    break;

                x++;
                y--;
            }

            localBestPosition.Scores += localBestPosition.AdditionalScores;
            localBestPosition.AdditionalScores = 0;
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

                if (IsEmpty(color, localBestPosition))
                    return;

                if (!TryToScore(color, localBestPosition))
                    break;

                x++; y++;
            }

            localBestPosition.Scores += localBestPosition.AdditionalScores;
            localBestPosition.AdditionalScores = 0;
        }
        
        private bool IsEmpty(Color color, Point localBestPosition)
        {
            if (color == Color.Empty)
            {
                localBestPosition.AdditionalScores = 0;
                return true;
            }

            return false;
        }

        private bool TryToScore(Color color, Point localBestPosition)
        {
            if (color == Color.Red)
                return false;

            localBestPosition.AdditionalScores++;                            
            return true;
        }
    }
}