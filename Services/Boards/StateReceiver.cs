using Contracts.Board.ViewModels;
using Contracts.Boards;
using Contracts.Boards.Contracts;
using Contracts.Boards.ViewModels;
using Services.Boards.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Boards
{
    public class StateReceiver : IStateReceiver
    {
        private List<Point> myPoints { get; set; }
        private Board board { get; set; }
        private List<Point> visitedPoints { get; set; }


        public StateReceiver(Board initializedBoard)
        {
            board = initializedBoard;
            visitedPoints = new List<Point>();

            FindMyPoints();            
        }

        private void FindMyPoints()
        {
            myPoints = new List<Point>();
            for(byte row = 0; row < board.GetRows; row++)
                for(byte col = 0; col < board.GetColumns; col++)                
                    if (board.GetColor(row, col) == Color.Red)
                        myPoints.Add(new Point(row, col));                
        }

        public string GetBestPosition()
        {            
            var boardWalker = new BoardWalker(board, visitedPoints);
            BoardUtils.SetBoard(board);

            var maxScores = 0;
            var bestPosition = "";

            myPoints.ForEach(myPoint => 
            {
                var localMaxPoint = boardWalker.BestStep(myPoint);
                if (localMaxPoint.Scores > maxScores)
                {
                    maxScores = localMaxPoint.Scores;
                    bestPosition = $"{NumberToString(localMaxPoint.Y + 1, true)}{localMaxPoint.X + 1}";
                }
            });

            return bestPosition;
        }

        private string NumberToString(int number, bool isCaps)
        {
            Char c = (Char)((isCaps ? 65 : 97) + (number - 1));
            return c.ToString();
        }
    }
}
