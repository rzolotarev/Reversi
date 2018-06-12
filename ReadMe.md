## Description

To find the best position I used the following algorithm: 
1. I look for all of the possible positions for a new disk on the board, they are adjacent to an 'O' tile;
2. I move each disk to all 8 possible (Up, Down, Up and Left ...) directions and count the potential scores;
3. After I checked out all of the possible positions, I pick the one with the maximum scores.