Exercise 01: robot simulator

A robot factory's test facility needs a program to verify robot movements. You are asked to implement a robot simulator. The robots have three possible movements: 1) turn right, 2) turn left, 3) advance.
Robots are placed on a hypothetical infinite grid, facing a particular direction (north, east, south, or west) at a set of {x,y} coordinates, e.g., {3,8}, with coordinates increasing to the north and east.
The robot then receives a number of instructions, at which point the testing facility verifies the robot's new position, and in which direction it is pointing.
As an example, the letter-string "RAALAL" means: 1) Turn right 2) Advance twice 3) Turn left 4) Advance once 5) Turn left yet again. Say a robot starts at {7, 3} facing north. Then running this stream of instructions should leave it at {9, 4} facing west.