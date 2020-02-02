import java.util.Random;

/**
 *
 * UI : 100 parts and 10 free space => 10 layers of 10 parts each
 *
 *  difficulty
 *      C(t) = C(l) + C(ai) (speed, awareness/range)
 *
 * 3 levels of difficulty
 *  -> jump
 *  -> crouch
 *  -> bomb / break wall
 *
 * Given target cost
 * Each cube has a cost/difficulty
 *
 * Look : exercise intensity driven level design
 *
 *  target cost - current cost -> 0
 *  keep subtracting the cost of the new cube from the current cost
 *
 *  moves :
 *          (1) add
 *          (2) remove
 *          (3) replace
 *
 *  Algorithm :
 *              (1) randomly pick a cube category (1, 2, 3 or 4)
 *              (2) randomly pick a texture
 *              (3) place the selected cube combination
 *
 *
 *  Abstract :
 *              common problem in VR - optimize game level for the given constraints of the living room / bedroom
 *              they most likely do not move / are scared of hurting themselves
 *
 *              our goal is to develop a game level by splitting the real space into a virtual grid
 *
 *  Title :
 *              solving the problem of fitting VR games into the living room
 *
 * Program to simulate game level creation based on randomized user input
 *
 *
 * Maximize common area
 * Algorithm to apply move to grid B
 * target score = max possible common cells
 *
 * target - proposed < target - current
 *
 * Created by Krishna on 9/22/2019.
 */
public class PathFinder {
    public static int LAYERS = 2;               // Change according to difficulty
    public static int NUMPATH = 10;             // Number of cells in the path (per layer) - change according to difficulty
    public static int targetCost = 100;         // got from user input (difficulty)
    public static int currentCost = 0;
    public static int endX = -1;
    public static int endY = -1;                // coordinates to keep track of the last cell to connect between layers
                                                // ending cell of layer x will be starting cell of layer x+1

    public static void main(String[] args) {

        PathFinder object = new PathFinder();

        int [][] grid = new int[5][5];
        int [][] userInput = new int[5][5];

        for (int i=0; i<5; i++) {
            for (int j=0; j<5; j++) {
                grid[i][j] = 0;
                userInput[i][j] = 0;
            }
        }

        // create the obstacles randomly (this will be generated by user input)
        object.createObstacles(grid);
        System.out.println("Grid after user input");
        printGrid(grid);

        for (int i=0; i<LAYERS; i++) {
            // for each layer, create a path of 10 objects starting from random available point
            object.findPath(grid, i+1);
            System.out.println("\nLayer " + (int)(i+1));
            printGrid(grid);
        }

        // copy the user input obstacles into an auxiliary array
        copyGrid(userInput, grid);
    }

    // Method to copy the user input grid to an auxiliary array
    public static void copyGrid (int [][] grid1, int [][] grid2) {
        for (int i=0; i<grid1.length; i++) {
            for (int j=0; j<grid1[i].length; i++) {
                grid1[i][j] = grid2[i][j];
            }
        }
    }

    // argument layer holds the current layer
    public void findPath (int [][] grid, int layer) {
        Random random = new Random();
        int starti;
        int startj;

        if (layer == 1) {
            // Randomly generate a starting point in the free space
            starti = -1;
            startj = -1;

            // find an unoccupied starting position
            while (true) {
                starti = random.nextInt(5);
                startj = random.nextInt(5);
                if (grid[starti][startj] == 0) {
                    grid[starti][startj] = 2;
                    break;
                }
            }
        }
        else {
            starti = endX;
            startj = endY;
        }

        // Create a path of NUMPATH cubes
        for (int count =0, i=starti, j=startj; count < NUMPATH; count++) {

            // Note: I changed the cubeType variable to hold the cube cost to simplify the process
            // randomly pick a cube type (2, 3, 4, 5)
            //int cubeType = 2 + random.nextInt(4);
            int cubeType = 5;

            // update current cost by adding the cube cost to it
            //currentCost += (cubeType-1)*5;
            currentCost += cubeType;

            System.out.println("CC: " + currentCost + "\nTC: " + targetCost);

            // check if target cost has been achieved
            if (currentCost >= targetCost) {
                break;
            }

            // check U,D,L,R
            if (i!= 0 && grid[i-1][j] == 0) {
                i = i-1;
                grid[i][j] = cubeType;
            }
            else if (i != grid.length-1 && grid[i+1][j] == 0) {
                i = i+1;
                grid[i][j] = cubeType;
            }
            else if (j != 0 && grid[i][j-1] == 0) {
                j = j-1;
                grid[i][j] = cubeType;
            }
            else if (j != grid.length-1 && grid[i][j+1] == 0) {
                j = j+1;
                grid[i][j] = cubeType;
            }

            endX = i;
            endY = j;
        }
        System.out.println("EX, EY = " + endX + ", " + endY);

    }

    public void createObstacles (int [][] grid) {
        Random random = new Random();

        // generate 8 obstacles randomly
        for (int i=0; i<8; i++) {
            int row = random.nextInt(5);
            int column = random.nextInt(5);
            grid[row][column] = 1;
        }

    }


    public static void printGrid(int [][] grid) {

        System.out.println("--------------------");

        for (int i=0; i<grid.length; i++) {
            for (int j=0; j<grid[i].length; j++) {
                System.out.print(grid[i][j] + " | ");
                if (j%4 == 0 && j!=0) System.out.println();
            }
        }

        System.out.println("--------------------");
    }
}