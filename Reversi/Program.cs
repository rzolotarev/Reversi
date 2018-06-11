using Services.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi
{
    class Program
    {
        static void Main(string[] args)
        {
            string board1 = @"5 1 
            X O O O . ";

            string result1 = Solution.PlaceToken(board1);
            Console.WriteLine($"correct answer E1 - result {result1}");

            string board2 = @"8 7 
            . . . . . . . . 
            . . . . . . . . 
            . . O . . . . . 
            . . . O X . . . 
            . . . X O O . . 
            . . . . . X . . 
            . . . . . . X . ";

            string result2 = Solution.PlaceToken(board2);
            Console.WriteLine($"correct answer B2 - result {result2}");

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
            Console.WriteLine($"correct answer  'D3', 'C4', 'F5', 'E6'  - result {result3}");

            string board4 = @"7 6 
            . . . . . . . 
            . . . O . O . 
            X O O X O X X 
            . O X X X O X 
            . X O O O . X 
            . . . . . . . ";
            string result4 = Solution.PlaceToken(board4);
            Console.WriteLine($"correct answer D6 - result {result4}");
        }
    }
}
