using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Boards.ViewModels
{
    public class Point
    {
        public byte X { get; set; }
        public byte Y { get; set; }
        public byte Scores { get; set; }
        public byte AdditionalScores { get; set; }
        public Point(byte x, byte y, byte scores = 0, byte additionalScores = 0)
        {
            X = x;
            Y = y;
            Scores = scores;
            AdditionalScores = additionalScores;
        }
    }
}
