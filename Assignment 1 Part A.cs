using System;

public enum TColor { WHITE, BLACK };
public class Square
{
    public TColor Color { set; get; } // Either WHITE or BLACK
    public int Number { set; get; } // Either a clue number or -1 (Note: A BLACK square is always -1)

    // Initialize a square to WHITE and its clue number to -1 (2 marks)
    public Square()
    {
        Color = TColor.WHITE;
        Number = -1;
    }
}

public class Puzzle
{
    private Square[,] grid; // treated as [X,Y], so [1,4] would mean row 1 column 4, this can be changed relatively easily and doesn't really matter as long as it's treated consistently
    private int N;

    // Create an NxN crossword grid of WHITE squares (4 marks)
    public Puzzle(int N)
    {
        this.N = N;
        grid = new Square[this.N, this.N]; //field used to eliminate chance of field being inaccurate
        for (int ix = 0; ix < this.N; ix++)
        {//populating grid with actual Squares, without this grid is nothing but null
            for (int iy = 0; iy < this.N; iy++)
            {
                grid[ix, iy] = new Square(); //see Square constructor
            }
        }
    }

    // Randomly initialize a crossword grid with M black squares (5 marks)
    public void Initialize(int M)
    { //Optimization opportunity: instead of wrapping entire process in for loop, just update vacanciesCount accordingly when a new value is picked
        Random rand = new Random(); //Random class used to generate random ints
        bool full = false; //control variable for tracking if crossword is full of black squares (full==true) or not (full==false)
        for (int i = 0; i < M && full == false; i++)
        {
            int[,] vacancies = new int[N * N, 2]; //every coordinate that's not black, columns are coordinates, x on top, y on bottom, row value will be used for listing coords.
            int vacanciesCount = 0; //tracking how many coordinates populate vacancies (array itself set to maximum possible value)
            for (int ix = 0; ix < N; ix++) //manages column by column, so x managed by outer loop
            {
                for (int iy = 0; iy < N; iy++) //y managed by inner loop
                {
                    if (grid[ix, iy].Color == TColor.WHITE) //if current coordinate is white
                    {
                        vacancies[vacanciesCount, 0] = ix; //uses ix as x coordinate, as vacancies is being build, vacanciesCount acts as good index for where to add coords.
                        vacancies[vacanciesCount, 1] = iy; //uses iy as y coordinate
                        vacanciesCount++; // has to be updated last so that initial value (0) can be used to populate vacancies
                    }
                }
            }
            if (vacanciesCount != 0) //if vacanciesCount==0 then all squares are black and no new square can be turned black
            {
                int listPicker = rand.Next(0, vacanciesCount); //picking random coord from list of available coords to make black
                grid[vacancies[listPicker, 0], vacancies[listPicker, 1]].Color = TColor.BLACK; //making square at randomly chosen coord black
            }
            else { full = true; } //if vacanciesCount is 0 then initialization is finished, the program will still compile without this control, this just reduces unnecessary processing  
        }
    }

    // Number the crossword grid (6 marks)
    public void Number()
    {
        int number = 1; //tracks which number to use 
        for (int iy = 0; iy < N; iy++)
        {
            for (int ix = 0; ix < N; ix++)
            {   //considers acrosses first, idk why but it feels right
                //if on left end of grid or to right of black square, and space is ample
                if (ix + 2 < N
                    && (ix > 0 && grid[ix - 1, iy].Color == TColor.BLACK || ix == 0)
                    && grid[ix, iy].Color == TColor.WHITE
                    && grid[ix + 1, iy].Color == TColor.WHITE
                    && grid[ix + 2, iy].Color == TColor.WHITE)
                {
                    grid[ix, iy].Number = number;
                    number++;
                }
                //downs considerations, fundamentally same as across but also doesn't overwrite existing numbers
                if (grid[ix, iy].Number == -1 && iy + 2 < N
                    && (iy > 0 && grid[ix, iy - 1].Color == TColor.BLACK || iy == 0)
                    && grid[ix, iy].Color == TColor.WHITE
                    && grid[ix, iy + 1].Color == TColor.WHITE
                    && grid[ix, iy + 2].Color == TColor.WHITE)
                {
                    grid[ix, iy].Number = number;
                    number++;
                }
            }
        }
    }

    // Print out the numbers for the Across and Down clues (in order) (4 marks)
    public void PrintClues()
    {
        int[] Across = new int[N * N]; //a queue would be better for this but array that is definitely big enough is easier 
        int acrossCount = 0; //tracking size of inhabited array
        int[] Down = new int[N * N];
        int downCount = 0;
        for (int iy = 0; iy < N; iy++)
        { //goes row through row
            for (int ix = 0; ix < N; ix++)
            {
                if (grid[ix, iy].Number != -1) //only checks numbered squares
                { //has to check validity of across and down again in order to determine if the number is for down or across or both, it feels like this should be avoidable but idk
                    if (ix + 2 < N
                    && (ix > 0 && grid[ix - 1, iy].Color == TColor.BLACK || ix == 0)
                    && grid[ix + 1, iy].Color == TColor.WHITE
                    && grid[ix + 2, iy].Color == TColor.WHITE)
                    {
                        Across[acrossCount] = grid[ix, iy].Number; //occupying arrays with appropriate clue numbers
                        acrossCount++;
                    }
                    if (iy + 2 < N //same as across but down
                    && (iy > 0 && grid[ix, iy - 1].Color == TColor.BLACK || iy == 0)
                    && grid[ix, iy + 1].Color == TColor.WHITE
                    && grid[ix, iy + 2].Color == TColor.WHITE)
                    {
                        Down[downCount] = grid[ix, iy].Number;
                        downCount++;
                    }
                }
            }
        } //printing
        Console.WriteLine("\nAcross:");
        for (int i = 0; i < acrossCount; i++)
        {
            Console.WriteLine(Across[i]);
        }
        Console.WriteLine("\nDown:");
        for (int i = 0; i < downCount; i++)
        {
            Console.WriteLine(Down[i]);
        }
    }
    // Print out the crossword grid including the BLACK squares and clue numbers (5 marks)
    public void PrintGrid()
    { //this sucks, barely legible, do not hand in without making this better
        for (int iy = 0; iy < N; iy++)
        {
            for (int ix = 0; ix < N; ix++)
            {
                if (grid[ix, iy].Color == TColor.WHITE)
                {
                    Console.Write("[W,"); //White
                }
                else
                {
                    Console.Write("[_,"); //Black, underscore used so it's easier to find
                }

                Console.Write(grid[ix, iy].Number + "]"); //for validating numbering
                //Console.Write(ix + "," + iy + "]"); //for validating coordinates
            }
            Console.WriteLine(); //Moves to new row
        }
    }
    // Return true if the grid is symmetric (à la New York Times); false otherwise (4 marks)
    public bool Symmetric() //hasn't been adequately tested 
    { //should check for symmetry following matrix definitions (if transpose == original (transpose means rows become columns and vice versa))
        for (int ix = 0; ix < N; ix++)
        {
            for (int iy = 0; iy < N; iy++)
            {
                if (grid[ix, iy].Color != grid[iy, ix].Color)
                {//if the grid color at a point is not equal to what the color would be at that point if transposed
                    return false;
                }
            }

        }
        return true; //should only get here if all points match the transpose
    }
}

public class Demo //for actually doing stuff
{
    public static void Main()
    {
        while (true) //so you can restart without closing and starting up again
        {
            Console.Write("Input size of crossword :");
            Puzzle crossword = new Puzzle(Convert.ToInt32(Console.ReadLine())); //wrap in try-catch loop later

            if (crossword.Symmetric()) { Console.WriteLine("Symmetric"); }
            else { Console.WriteLine("Not Symmetric"); }

            Console.Write("Input number of black spaces :");
            crossword.Initialize(Convert.ToInt32(Console.ReadLine())); //wrap in try-catch loop later
            crossword.Number();
            crossword.PrintGrid();
            crossword.PrintClues();

            if (crossword.Symmetric()) { Console.WriteLine("Symmetric"); }
            else { Console.WriteLine("Not Symmetric"); }

            Console.ReadLine(); //prevents from closing when execution finished
        }
    }
}

