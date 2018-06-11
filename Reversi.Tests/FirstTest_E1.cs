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
    public class FirstTest
    {
        [Test]
        public void ResultShouldReturnE1()
        {
            string board1 = @"5 1 X O O O . ";            

            string result1 = Solution.PlaceToken(board1);
        }
    }
}
