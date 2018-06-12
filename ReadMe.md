## Description

To find the best position I used the following algorithm: 
1. I've found all of my disks (X) on the board, because I can only put a new disk diagonally or oppostite to it;
2. I move each disk to all 8 possible (Up, Down, Up and Left ...) directions and count the potential scores;
3. Also I remeber the visited positions. And if I have already been there, I skip this poisiton;
4. After I checked out all of the possible positions, I pick the one with the maximum scores.