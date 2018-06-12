## Description

To find the best position I used the following algorithm: 
1. I've found all of my disks (X) on the board, as starting points to find new positions for a new disk;
2. I move each disk to all 8 possible (Up, Down, Up and Left ...) directions and count the potential scores;
3. Also I remeber the visited positions. And if I have already been there, I skip this poisiton;
4. After I checked out all of the possible positions, I pick the one with the maximum scores.