# Space Mission - SPACE 2026 Assessment

## Overview
A C# .NET console application that guides astronauts through an asteroid field to the Space Station using BFS pathfinding algorithm.

## Features
- Finds the shortest path for up to 3 astronauts
- Displays the path visually on the map
- Sorts astronauts by shortest path length
- Handles cases where no path exists
- Supports manual map input or random map generation
- Extensible pathfinding via IPathFinder interface

## How to Run
dotnet run 


## Map Symbols
- S1, S2, S3 - Astronauts
- F - Space Station (destination)
- O - Open space
- X - Asteroid (blocked)

## Project Structure
- Program.cs - Entry point
- Grid.cs - Map representation
- Astronaut.cs - Astronaut model
- PathFinder.cs - BFS pathfinding algorithm
- IPathFinder.cs - Pathfinder interface
