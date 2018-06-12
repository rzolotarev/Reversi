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
        private Board board { get; set; }
        private List<Point> visitedPoints { get; set; }


        public StateReceiver(Board initializedBoard)
        {
            board = initializedBoard;
            visitedPoints = new List<Point>();
        }
      
        public string GetBestPosition()
        {            
            var boardWalker = new BoardWalker(board, visitedPoints);
            BoardUtils.SetBoard(board);
            var possiblePositions = BoardUtils.GetPossiblePosition();
            var maxScores = 0;
            Point bestPosition = null;

            possiblePositions.ForEach(possiblePosition => 
            {
                var currentPossiblePosition = boardWalker.CountScores(possiblePosition);
                if (currentPossiblePosition.Scores > maxScores)
                {
                    maxScores = currentPossiblePosition.Scores;
                    bestPosition = currentPossiblePosition;                    
                }               
            });

            return $"{NumberToString(bestPosition.Y + 1, true)}{bestPosition.X + 1}"; ;
        }

        private string NumberToString(int number, bool isCaps)
        {
            Char c = (Char)((isCaps ? 65 : 97) + (number - 1));
            return c.ToString();
        }
    }
}
