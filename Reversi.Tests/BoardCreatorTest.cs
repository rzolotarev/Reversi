using Contracts.Board.ViewModels;
using NUnit.Framework;
using Services.Boards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Tests
{
    [TestFixture]
    public class BoardCreatorTest
    {
        [Test]
        public void TestOneRowTable()
        {
            var validator = new BoardDimensionValidator();
            var boardCreator = new BoardCreator(validator);
            var board = boardCreator.Create(@"5 1
            X O O O . ");
            Assert.AreEqual(Color.Red, board.GetColor(0, 0));
            Assert.AreEqual(Color.Black, board.GetColor(0, 1));
            Assert.AreEqual(Color.Empty, board.GetColor(0, 4));
        }

        [Test]
        public void TestMultiRowTable()
        {
            var validator = new BoardDimensionValidator();
            var boardCreator = new BoardCreator(validator);
            var board = boardCreator.Create(@"5 2
            X O O O . 
            X X . O O");
            Assert.AreEqual(Color.Red, board.GetColor(0, 0));
            Assert.AreEqual(Color.Black, board.GetColor(0, 1));
            Assert.AreEqual(Color.Empty, board.GetColor(0, 4));

            Assert.AreEqual(Color.Red, board.GetColor(1, 0));
            Assert.AreEqual(Color.Black, board.GetColor(1, 3));
            Assert.AreEqual(Color.Empty, board.GetColor(1, 2));
        }
    }
}
