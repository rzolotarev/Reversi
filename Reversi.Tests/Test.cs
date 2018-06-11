using NUnit.Framework;
using Services.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Tests
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void ResultShouldReturnE1()
        {
            string board1 = @"5 1 
            X O O O . ";            

            string result1 = Solution.PlaceToken(board1);
            Assert.AreEqual("E1", result1);
        }


        [Test]
        public void ResultShouldReturnB2()
        {
            string board2 = @"8 7 
            . . . . . . . . 
            . . . . . . . . 
            . . O . . . . . 
            . . . O X . . . 
            . . . X O O . . 
            . . . . . X . . 
            . . . . . . X . ";

            string result2 = Solution.PlaceToken(board2);
            Assert.AreEqual("B2", result2);
        }

        [Test]
        public void ResultShouldReturnD3C4F5E6()
        {            
            var possibleResults = new List<string>()
            {
                "D3", "C4", "F5", "E6"
            };
            string board3 = @"8 8 
            . . . . . . . . 
            . . . . . . . . 
            . . . . . . . . 
            . . . O X . . . 
            . . . X O . . . 
            . . . . . . . . 
            . . . . . . . . 
            . . . . . . . . "; 
            
            string result3 = Solution.PlaceToken(board3);
            Assert.IsTrue(possibleResults.Contains(result3));
        }

        [Test]
        public void ResultShouldReturnD6()
        {            
            string board4 = @"7 6 
            . . . . . . . 
            . . . O . O . 
            X O O X O X X 
            . O X X X O X 
            . X O O O . X 
            . . . . . . . ";
            string result4 = Solution.PlaceToken(board4);
            Assert.AreEqual("D6", result4);
        }
    }
}
