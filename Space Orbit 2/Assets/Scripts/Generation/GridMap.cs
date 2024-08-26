using System.Collections.Generic;
using UnityEngine;

public class GridMap {
    public readonly bool[,] grid;
    private int width;
    private int height;

    public GridMap(int width, int height) {
        this.width = width;
        this.height = height;
        grid = new bool[width, height];
        Debug.Log("grid created with this many cells: " + grid.Length);
    }

    public void SetCell(int x, int y, bool isOccupied) {
        if (x >= 0 && y >= 0 && x < width && y < height) {
            grid[x, y] = isOccupied;
        }
    }

    public bool IsCellOccupied(int x, int y) {
        if (x >= 0 && y >= 0 && x < width && y < height) {
            return grid[x, y];
        }

        return true; // Return true for out of bounds to avoid placing objects outside the grid
    }

    private bool IsWithinCircle(int x, int y, int centerX, int centerY, int radius) {
        //magic math that checks if a point is within a circle
        int deltaX = x - centerX;
        int deltaY = y - centerY;
        return deltaX * deltaX + deltaY * deltaY <= radius * radius;
    }

    public Vector2 GetRandomCellWithDistanceFromOccupiedZones(int distance) {
        Vector2[] freeCells = GenerateListOfCellsWithDistanceFromOccupiedSpaced(distance);
        Vector2 randomFreeCell;
        if (freeCells.Length == 0) {
            Debug.LogWarning("No free cells found. Map might be too small");
            randomFreeCell = Vector2.zero;
        }
        else {
            Debug.Log("Found " + freeCells.Length + " free cells");
            randomFreeCell = freeCells[Random.Range(0, freeCells.Length)];
        }

        return randomFreeCell;
    }

    private Vector2[] GenerateListOfCellsWithDistanceFromOccupiedSpaced(int distance) {
        // Generate a map of cells that are forbidden because they are too close to occupied cells
        bool[,] mapWithForbiddenCells = GenerateMapOfForbiddenCellsWithDistanceFromOccupiedZones(distance);

        // Iterate through the map and add all free cells to a list
        List<Vector2> freeCells = new List<Vector2>();
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (!mapWithForbiddenCells[x, y]) {
                    freeCells.Add(new Vector2(x, y));
                }
            }
        }

        // Return the list as an array
        return freeCells.ToArray();
    }

    private bool[,] GenerateMapOfForbiddenCellsWithDistanceFromOccupiedZones(int distance) {
        // Clone the original grid, occupied spaces are occupied by default
        bool[,] mapWithForbiddenCells = (bool[,])grid.Clone();

        // Iterate through the original grid
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                //check if the cell is occupied
                if (grid[x, y]) {
                    //if yes, block the cells around it
                    OccupyCellsInCircle(mapWithForbiddenCells, x, y, distance);
                }
            }
        }

        // Return the map of forbidden cells
        return mapWithForbiddenCells;
    }

    public void OccupyCellsInCircle(bool[,] gridToOccupy, int centerX, int centerY, int radius) {
        int iterations = 0;
        //iterate through a square that is the size of the circle
        for (int x = centerX - radius; x <= centerX + radius; x++) {
            for (int y = centerY - radius; y <= centerY + radius; y++) {
                // check if the cell is within the circle
                if (IsWithinCircle(x, y, centerX, centerY, radius)) {
                    // //check if the cell is within the grid
                    if (x >= 0 && y >= 0 && x < width && y < height) {
                        //if yes, mark it as occupied
                        iterations++;
                        // gridToOccupy[x, y] = true;
                    }
                }
            }
        }

        Debug.Log("went through " + iterations + " iterations");
        Debug.Log("for grid with size: " + gridToOccupy.Length);
        gridToOccupy[1, 2] = true;
        
    }
}