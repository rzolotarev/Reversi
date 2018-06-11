using Contracts.Board;
using Contracts.Board.ViewModels;
using Contracts.Boards;
using Contracts.Boards.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Boards
{
    public class BoardCreator : IBoardCreator
    {
        private readonly IBoardDimensionValidator _dimensionValidator;

        private readonly IDictionary<string, Color> _colorMapping = new Dictionary<string, Color>
        {
            { ".", Color.Empty },
            { "O", Color.Black },
            { "X", Color.Red }
        };

        public BoardCreator(IBoardDimensionValidator dimensionValidator)
        {
            _dimensionValidator = dimensionValidator;
        }        

        public Board Create(string board)
        {
            if (!_dimensionValidator.ValidateFormat(board))
                throw new Exception("Please make sure that you set a correct board format");

            var lines = board.Split('\n');
            var firstLine = lines.First().Trim();
            var dimensions = firstLine.Split(' ');

            if (!Byte.TryParse(dimensions[0], out byte width))
                throw new Exception($"Can not convert {dimensions[0]} to width");

            if (!Byte.TryParse(dimensions[1], out byte height))
                throw new Exception($"Can not convert {dimensions[1]} to height");

            Board boardObject = null;

            if (!_dimensionValidator.ValidateDimension(width, height))
                throw new Exception("Please make sure that you set correct width and height of the board");

            boardObject = new Board(width, height);

            for (byte row = 0; row < height; row++)
            {
                var currentRow = lines[row + 1];
                var splittedRow = currentRow.Trim().Split(' ');
                for (byte col = 0; col < width; col++)
                {
                    if (_colorMapping.TryGetValue(splittedRow[col], out Color value))
                        boardObject.SetColor(row, col, value);
                    else
                        throw new Exception($"Symbol {splittedRow[col]} was not parsed because of unknown symbol");
                }
            }

            return boardObject;
        }
    }
}
