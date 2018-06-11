using Services.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Solutions
{
    public class Solution
    {
        public static string PlaceToken(string board)
        {
            var validator = new BoardDimensionValidator();
            var boardCreater = new BoardCreator(validator);
            var tiledBoard = boardCreater.Create(board);
            var stateReceiver = new StateReceiver(tiledBoard);
            return stateReceiver.GetBestPosition();            
        }
    }
}
