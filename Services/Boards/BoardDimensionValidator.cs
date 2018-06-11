using Contracts.Boards.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Boards
{
    public class BoardDimensionValidator : IBoardDimensionValidator
    {
        public BoardDimensionValidator()
        {

        }

        //TODO взять из appConfig
        public bool ValidateDimension(byte width, byte height)
        {
            if (width < 0 || width > 26)
                return false;

            if (height < 1 || height > 26)
                return false;

            return true;
        }

        public bool ValidateFormat(string board)
        {
            var lines = board.Split('\n');
            if (lines.FirstOrDefault() == null)
                return false;

            return true;
        }
    }
}
