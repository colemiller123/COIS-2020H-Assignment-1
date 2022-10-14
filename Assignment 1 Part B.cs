/////////////////////////////////////////////////////
//By: Cole Miller
//Date: 2022-10-14
//Project: Assignment 1, part B
/////////////////////////////////////////////////////

//Setup for Visual Studio Code https://learn.microsoft.com/en-us/dotnet/core/tutorials/with-visual-studio-code?pivots=dotnet-6-0
//Create a new folder
//Create a new terminal in VS code
//Run the command in the terminal  dotnet new console --framework net6.0
//Change the launch.json file's console parameter to "console": "externalTerminal",
//Run --> Start debugging
//Say "Yes" to download missing assets
//Change namesace to the file folder you are testing from

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            //Prompt the User for a single character and check if the User has entered more than one character
            static string CheckSingleCharacter() {
                bool singleCharacterCheck = false;
                string userInput = "";
                string msg = "Please enter a single character.";

                while (singleCharacterCheck == false) {
                    //Call method on the selected string
                    Console.WriteLine(msg);
                    userInput = Console.ReadLine();

                    //Check the user has not entered more than one character or no characters
                    if (userInput.Length > 1 || userInput.Length == 0) {
                        Console.Clear();
                        Console.WriteLine(msg);
                    }
                    else {
                        singleCharacterCheck = true;
                    }
                }
                return userInput;
                    
            }
            
            //Displays user created MyStrings, verifies the User has entered a valid choice from the menu, return their selection as an index number (int)
            static int VerifyUserInput (int ArrayLength, Object [] userStrings) {
                string errorMessage = "";
                bool userInputChecked = false;
                int userInputInt = -1;

                //Display list of user created MyStrings
                Console.Clear();
                while (userInputChecked == false) {
                    for (int i = 0; i < userStrings.Length; i++) {
                        Console.WriteLine(i + ": " + userStrings[i]);
                    }

                    //Get user input
                    Console.WriteLine("Please type the number to select the Object.");
                    string userInput = Console.ReadLine();
                    
                    //Check user selection is a number
                    try {
                        userInputInt = Int32.Parse(userInput);
                    }
                    catch {
                        errorMessage = "Please choose a number.";
                    }

                    //Check User selection is in the Arrays' index range
                    if (userInputInt < 0 || userInputInt > ArrayLength) {
                        errorMessage = "Please choose one of the listed numbers.";
                    }
                    else if (userStrings[userInputInt] == null) {
                        errorMessage = "Please choose an index with a non empty value.";
                    }

                    if (errorMessage != "") {
                        Console.Clear();
                        Console.WriteLine(errorMessage);
                        errorMessage = "";
                    }
                    else {
                        userInputChecked = true;
                    }
                }
                return userInputInt;
            }

            int userStringsArrayLength = 20; //length of 20 is arbitrary, if you make more than 20 strings thats on you
            MyString [] userStrings = new MyString[userStringsArrayLength];
            int userArrayIndex = 0;

            //////////////Demo main menu main loop//////////////////////////
            bool quitProgram = false;
            while (quitProgram == false) {
                Console.Clear();
                
                string userInput = "";
                int userInputInt = 0;
                string wait;

                bool userinputUnverified = false;
                while (userinputUnverified == false) {
                    Console.WriteLine("Part B Main Menu");
                    Console.WriteLine("1: Create a string");
                    Console.WriteLine("2: Reverse a string");
                    Console.WriteLine("3: Return the index of the first occurence of a char in a string");
                    Console.WriteLine("4: Remove a character from a string");
                    Console.WriteLine("5: Check the equalilty of an object to MyString");
                    Console.WriteLine("6: Print a MyString object");
                    Console.WriteLine("7: Quit");
                    Console.WriteLine("Please type in the number of the method you want to use.");
                    
                    //Get User input
                    userInput = Console.ReadLine();

                    //Check user selection is a number
                    try {
                        userInputInt = Int32.Parse(userInput);
                        userinputUnverified = true;
                    }
                    catch {
                        Console.Clear();
                        Console.WriteLine("Please choose a number.");
                    }
                }

                //Handle user input
                switch (userInputInt) {
                //Create a new MyString
                case 1:
                    Console.Clear();
                    Console.WriteLine("Please type the string you would like to create.");
                    userInput = Console.ReadLine();
                    
                    //Convert userInput to a char array
                    char [] userArray = new char[userInput.Length];
                    for (int i = 0; i < userInput.Length; i++) {
                        userArray[i] = userInput[i];
                    }

                    MyString newUserString = new MyString(userArray);

                    //Store that string in an array
                    userStrings[userArrayIndex] = newUserString;
                    userArrayIndex++;
                    break;

                //Reverse a MyString object
                case 2:
                    userInputInt = VerifyUserInput(userStringsArrayLength, userStrings);
                        
                    //Call method on the selected string
                    userStrings[userInputInt].Reverse();
                    break;

                //Return the index of the first occurence of a char in a MyString object
                case 3:
                    userInputInt = VerifyUserInput(userStringsArrayLength, userStrings);
                    
                    Console.Clear();

                    userInput = CheckSingleCharacter();
                    char character = Char.Parse(userInput);

                    int indexResult = userStrings[userInputInt].IndexOf(character);

                    Console.WriteLine(indexResult);

                    //Wait
                    Console.ReadLine();
                    break; 

                //Remove a single character from a MyString object
                case 4:
                    userInputInt = VerifyUserInput(userStringsArrayLength, userStrings);

                    Console.Clear();
 
                    userInput = CheckSingleCharacter(); 

                    char userInputChar = char.Parse(userInput);

                    userStrings[userInputInt].Remove(userInputChar);
                    break;
                
                //Compare to objects
                case 5:
                    int userInputObjectToCompareInt;
                    bool result;
                    int inputInt = -1;
                    
                    userInputInt = VerifyUserInput(userStringsArrayLength, userStrings);

                    Console.Clear();

                    bool userSelectionCheck = false;
                    while (userSelectionCheck == false) {
                        Console.WriteLine("Compare against a user created string or another object?");
                        Console.WriteLine("1: User String");
                        Console.WriteLine("2: A non-MyString Object");

                        string inputT = Console.ReadLine();
                        inputInt = Int32.Parse(inputT);

                        //Check input is valid
                        if (inputInt < 1 || inputInt > 2) {
                            Console.Clear();
                            Console.WriteLine("Please choose one of the listed numbers.");
                        }
                        else {
                            userSelectionCheck = true;
                        }
                    }

                    //Show the User his created MyStrings
                    if (inputInt == 1) {
                        userInputObjectToCompareInt = VerifyUserInput(userStringsArrayLength, userStrings);
                        result = userStrings[userInputInt].Equals(userStrings[userInputObjectToCompareInt]);
                    }
                    //Show other premade objects
                    else {
                        //Create object array
                        Car carOne = new Car();
                        Object [] objectArray = {carOne};

                        userInputObjectToCompareInt = VerifyUserInput(objectArray.Length, objectArray);
                        result = userStrings[userInputInt].Equals(objectArray[userInputObjectToCompareInt]);
                    }

                    Console.WriteLine(result);

                    //Wait
                    Console.ReadLine();
                    break;

                //Print the selected string
                case 6:
                    userInputInt = VerifyUserInput(userStringsArrayLength, userStrings);
                        
                    //Call method on the selected string
                    userStrings[userInputInt].Print();

                    //Wait
                    Console.ReadLine();
                    break;

                case 7:
                    quitProgram = true;
                    break;
                }
                //testing
                Console.WriteLine(userInput);
            }
        }

        public class Car {
            int color = 1;
        }

        public class MyString {
            private class Node {
                public char item;
                public Node next;

                // Constructor for an empty node
                public Node () {
                    this.next = null;
                }

                // Constructor (2 marks)
                public Node (char item, Node next) {
                    this.item = item;
                    this.next = next;
                }
            }

            private Node front = new Node();    // Reference to the first (header) node
            private int length = 0;             // Number of characters in MyString

            // Initialize with a header node an instance of MyString to the given character array A (4 marks)
            public MyString (char[ ] A) {
                Node tempPointer = front;       //Points to where the last node in the linked list is
                foreach (char character in A) {
                    //Create a new node for each character
                    Node newNode = new Node(character, null);

                    if (length == 0) {
                        front.next = newNode;
                    }
                    else {
                        tempPointer = tempPointer.next;
                        tempPointer.next = newNode;   
                    }
                    length++;
                }
            }
            
            // Using a stack, reverse this instance of MyString (6 marks)
            public void Reverse () { 
                Stack<char> S;
                S = new Stack<char> ();
                
                //Create a stack with the characters from this instances MyString, with the front being on the bottom of the stack
                Node String = front.next;
                for (int tempLength = length; tempLength != 0; tempLength--) {
                    //Add the front to the top of the stack
                    S.Push(String.item);

                    //Move the front forward
                    String = String.next;
                }

                //Reverse the order
                String = front.next;
                for (int tempLength = S.Count; tempLength > 0; tempLength--) {
                    String.item = S.Pop();
                    String = String.next;
                }
            }
            
            // Return the index of the first occurrence of c in this instance; otherwise -1 (4 marks)
            public int IndexOf(char c) {
                Node currentIndex = front;
                int index = 0;

                //Loop through MyString instance
                for (int tempLength = length; tempLength >= 0; tempLength--) {
                    if (currentIndex.item == c) {
                        return index;
                    }
                    else {
                        index++;
                        currentIndex = currentIndex.next;
                    }   
                }  
                //Return -1 if c was not found in MyString 
                return -1;
            }
            
            // Remove all occurrences of c from this instance (4 marks)
            public void Remove (char c) {
                Node currentIndex = front;
                
                //Loop through MyString instance
                for (int tempLength = length; tempLength > 0; tempLength--) {
                    if (currentIndex.next.item == c) {
                        //Remove c from MyString
                        currentIndex.next = currentIndex.next.next;

                        length--;
                    }
                    else {
                        currentIndex = currentIndex.next;
                    }   
                }  
            }
            
            // Return true if obj is both of type MyString and the same as this instance; 
            // otherwise false (6 marks)
            public override bool Equals(object obj) {
                if (obj == null) {
                    return false;
                }
                
                //Check obj is MyString object
                if (obj.GetType() == this.GetType()) {
                    MyString objToCompare = (MyString) obj;

                    //Check if the instances' have the same data
                    if (this.length == objToCompare.length) {
                        Node tempThis = this.front;
                        Node tempObjectNodeToCompare = objToCompare.front;
                        
                        //Compare each node value
                        for (int i = objToCompare.length; i > 0; i--) {
                            if (tempThis.item == tempObjectNodeToCompare.item) {
                                //True
                            }
                            else {
                                return false;
                            }

                            //Move up to the next node
                            tempThis = tempThis.next;
                            tempObjectNodeToCompare = tempObjectNodeToCompare.next;
                        }
                        return true;
                    }
                }
                return false;
            }
            
            // Print out this instance of MyString (3 marks)
            public void Print() {
                Console.WriteLine("Length of the String: " + length);
                
                //For each character/node in the linked list, print out the character
                Node String = front.next;
                for (int tempLength = length; tempLength > 0; tempLength--) {
                    Console.WriteLine(String.item);
                    String = String.next;
                }

            }
        }
    }
}