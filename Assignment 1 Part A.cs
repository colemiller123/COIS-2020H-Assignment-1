/*
 * COIS 2020H | Assignment 1 - Part A
 * Group Members (alphabetical): Cole Miller, Jesse Laframboise, Matthew Hellard
 * 
 * ----
 * Easiest way to run in Visual Studio:
 * Create new Console App (.NET Framework)
 * Clear new .cs file of contents
 * Copy & Paste this code into new .cs file
 * ----
 */
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
    private Square[,] grid; // treated as [X,Y], so [1,4] would mean row 1 column 4, should be treated consistently
    private int N;

    // Create an NxN crossword grid of WHITE squares (4 marks)
    public Puzzle(int N)
    {
        if (N < 0)
        {
            throw new Exception("Cannot construct Puzzle with negative value");
        }
        this.N = N;
        grid = new Square[this.N, this.N]; //field used to eliminate chance of field being inaccurate
        for (int ix = 0; ix < this.N; ix++)
        {
            for (int iy = 0; iy < this.N; iy++)
            { //populating grid with actual squares
                grid[ix, iy] = new Square(); //see Square constructor
            }
        }
    }

    // Randomly initialize a crossword grid with M black squares (5 marks)
    public void Initialize(int M)
    {
        if (M < 0)
        {
            throw new Exception("Cannot initialize Puzzle with negative value");
        }
        //building list of available coordinates
        int[,] vacancies = new int[N * N, 2]; //every coordinate that's not black, columns are coordinates, x on top, y on bottom.
        int vacanciesCount = 0; //tracking how many coordinates populate vacancies (array itself set to maximum possible value)
        for (int ix = 0; ix < N; ix++)
        {
            for (int iy = 0; iy < N; iy++)
            {
                if (grid[ix, iy].Color == TColor.WHITE) //if current coordinate is white
                {
                    vacancies[vacanciesCount, 0] = ix;
                    vacancies[vacanciesCount, 1] = iy;
                    vacanciesCount++; //has to be updated last so that initial value can be used to populate vacancies
                }
            }
        }
        //placing black squares and updating list
        Random rand = new Random(); //Random class used to generate random ints
        int index = 0;
        while (index < M && vacanciesCount!=0) //while index is less than M AND there exist vacancies, considering vacanciesCount lets users "overfill" the grid without consequence
        {
            int listPicker = rand.Next(0, vacanciesCount); //picking random coord from list of available coords to make black
            grid[vacancies[listPicker, 0], vacancies[listPicker, 1]].Color = TColor.BLACK; //making square at randomly chosen coord from vacancies black
            for (int i = listPicker; i + 1 < vacanciesCount; i++) //updating vacancies to remove used item and reorganize for reuse
            {
                vacancies[i, 0] = vacancies[i + 1, 0]; //moving everything down to fill used index
                vacancies[i, 1] = vacancies[i + 1, 1]; //moving everything down to full used index
            }
            vacanciesCount--; //updating to represent that a value has been used and can't be used again
            index++;
        }
    }

    // Number the crossword grid (6 marks)
    public void Number()
    {
        int number = 1; //tracks which number to use 
        for (int iy = 0; iy < N; iy++)
        {
            for (int ix = 0; ix < N; ix++)
            {   //goes row by row so that numbers will progress row by row
                //across considerations
                if (ix + 1 < N //if on left end of grid or to right of black square, and space is ample
                    && (ix > 0 && grid[ix - 1, iy].Color == TColor.BLACK || ix == 0)
                    && grid[ix, iy].Color == TColor.WHITE
                    && grid[ix + 1, iy].Color == TColor.WHITE)
                {
                    grid[ix, iy].Number = number;
                    number++;
                }
                //down considerations, only considered if current position wasn't deemed a viable across
                else if (iy + 1 < N//if on top end of grid or below black square, and space is ample
                    && (iy > 0 && grid[ix, iy - 1].Color == TColor.BLACK || iy == 0)
                    && grid[ix, iy].Color == TColor.WHITE
                    && grid[ix, iy + 1].Color == TColor.WHITE)
                {
                    grid[ix, iy].Number = number;
                    number++;
                }
                else //sets any positions that shouldn't be numbered to not be numbered
                {
                    grid[ix, iy].Number = -1;
                }
            }
        }
    }

    // Print out the numbers for the Across and Down clues (in order) (4 marks)
    public void PrintClues()
    {
        //building complete lists of across and down clues before printing anything
        int[] Across = new int[N * N]; //array to store acrosses
        int acrossCount = 0; //tracking size of inhabited across array
        int[] Down = new int[N * N]; //array to store downs
        int downCount = 0; //tracking size of inhabited down array
        for (int iy = 0; iy < N; iy++)
        {
            for (int ix = 0; ix < N; ix++)
            {
                if (grid[ix, iy].Number != -1) //only checks numbered squares. Because numbers will only end up on valid squares, checks past here don't need to check current square for anything
                { //has to check validity of across and down again in order to determine if the number is for down or across or both
                    //across
                    if (ix + 1 < N //if on left end of grid or to right of black square, and space is ample
                    && (ix > 0 && grid[ix - 1, iy].Color == TColor.BLACK || ix == 0)
                    && grid[ix + 1, iy].Color == TColor.WHITE)
                    {
                        Across[acrossCount] = grid[ix, iy].Number; //occupying array with appropriate clue numbers
                        acrossCount++;
                    }
                    //down
                    if (iy + 1 < N //if number is unused and on top end of grid or below black square, and space is ample
                    && (iy > 0 && grid[ix, iy - 1].Color == TColor.BLACK || iy == 0)
                    && grid[ix, iy + 1].Color == TColor.WHITE)
                    {
                        Down[downCount] = grid[ix, iy].Number; //occupying array with appropriate clue numbers
                        downCount++;
                    }
                }
            }
        } 
        //printing
        int scale = Convert.ToInt32(Convert.ToString(N * N * 2).Length) + 5; //scale used to determine spacing when lining up number columns
        Console.Write("\nAcross:");
        for (int i = 0; i < scale; i++)
        {
            Console.Write(' '); //scale based spacing between list titles, should give enough horizontal room for biggest clue number without messing up spacing
        }
        Console.WriteLine("Down:");
        for (int i = 0; i < downCount || i < acrossCount; i++) //repeats until across and down are both exhausted
        {
            if (i < acrossCount) { Console.Write(Across[i]); } else { Console.Write(' '); } //prints across clues until exhausted, then prints blanks to maintain spacing for down list
            for (int spacing = 0; spacing < scale + 7 - Convert.ToInt32(Convert.ToString(Across[i]).Length); spacing++) //adding appropriate spacing, +7 accounts for length of "Across:" string
            {
                Console.Write(' '); //spacing between actual contents of lists
            }
            if (i < downCount) { Console.Write(Down[i]); } //only prints if there are down clues left
            Console.WriteLine();
        }
    }

    // Print out the crossword grid including the BLACK squares and clue numbers (5 marks)
    public void PrintGrid() //inverted colourwise as white black squares look better on black background (console)
    { // breaks when boxes exceed console window, no reasonable fix as it depends on console window size and display size, neither of which can be reasonably known by this program let alone dealt with
        Console.WriteLine("*Note: Colours inverted*");
        //scale grid by largest possible clue number (square of size doubled due to downs and ups)
        int scale = Convert.ToInt32(Convert.ToString(N * N * 2).Length) * 2; //scale=number of digits in 2xN^2,x2. Second double is to let nums go in top left of box 
        linebreak();//see internal linebreak method
        for (int iy = 0; iy < N; iy++)
        {
            Console.Write('|'); //front divider to box in squares
            for (int ix = 0; ix < N; ix++) //does tops of every box
            {
                if (grid[ix, iy].Number != -1) //if current square has a number,
                {
                    Console.Write(grid[ix, iy].Number); //write number,
                    for (int remainder = scale - Convert.ToInt32(Convert.ToString(grid[ix, iy].Number).Length); remainder > 0; remainder--) //remainder=scale-digits of current square number
                    {
                        Console.Write(' '); //fill remaining space on top row in box with void.
                    }
                }
                else if (grid[ix, iy].Color == TColor.WHITE) //if current square has no number but is white,
                {
                    for (int i = 0; i < scale; i++)
                    {
                        Console.Write(' '); //fill space on top row in box with void.
                    }
                }
                else //if current square is black,
                {
                    for (int i = 0; i < scale; i++)
                    {
                        Console.Write('#'); //fill space on top row in box with # to indicate being occupied.
                    }
                }
                Console.Write('|'); //end row of box with column divider (|)
            } // at this point top row of all squares of current row has been printed
            //adding verticality to each box, without this, every box is one line tall, the amount of verticality is somewhat dependent on width (N)
            for (int vertical = 0; vertical < Math.Round(Math.Sqrt(scale)); vertical++) 
            {
                Console.WriteLine();
                Console.Write('|');//front divider for row
                for (int ix = 0; ix < N; ix++)
                {
                    if (grid[ix, iy].Color == TColor.WHITE) //if current square is white,
                    {
                        for (int i = 0; i < scale; i++)
                        {
                            Console.Write(' '); //fill row of box with void.
                        }
                    }
                    else //if current square is black,
                    {
                        for (int i = 0; i < scale; i++)
                        {
                            Console.Write('#'); //fill row of box with # to indicate being occupied.
                        }
                    }
                    Console.Write('|'); //end row of box with column divider(|)
                }
            }
            linebreak();
        }
        void linebreak() //for separating rows
        {
            Console.WriteLine();
            for (int i = 0; i < scale * N + N + 1; i++) { Console.Write("-"); } //scale*N to account for squares, +N+1 to account for column dividers, including the one in front (+1)
            Console.WriteLine();
        }
    }

    // Return true if the grid is symmetric (à la New York Times); false otherwise (4 marks)
    public bool Symmetric()
    { //NYT symmetry seems to be defined as being reversible (the same forwards and backwards)
        for (int iy = 0; iy < N; iy++)
        {
            for (int ix = 0; ix < N; ix++)
            {
                if (grid[ix, iy].Color != grid[N - 1 - ix, N - 1 - iy].Color)
                {//if the grid color at a point is not equal to what the color would be at that point if reversed
                    return false;
                }
            }
        }
        return true; //should only get here if all points fulfill the required symmetry
    }
}

public class Demo //get rid of this class before handing in? (not part of assignment)
{
    public static void Main()
    {
        while (true) //so you can restart without closing and starting up again
        {
            Console.Write("Input size of crossword :");
            Puzzle crossword = new Puzzle(Convert.ToInt32(Console.ReadLine()));

            //Puzzle crossword = new Puzzle(3); //for automatic input

            Console.Write("Input number of black spaces :");
            crossword.Initialize(Convert.ToInt32(Console.ReadLine()));

            //crossword.Initialize(4); //for automatic input

            crossword.Number();
            crossword.PrintGrid();
            crossword.PrintClues();

            if (crossword.Symmetric()) { Console.WriteLine("\nSymmetric"); }
            else { Console.WriteLine("\nNot Symmetric"); }

            Console.ReadLine(); //prevents from closing when execution finished
        }
    }
}
