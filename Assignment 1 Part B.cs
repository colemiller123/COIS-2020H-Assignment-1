namespace COIS2020HAssignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Testing
            //char [] testArray = {'c', 'o', 'l', 'e','m', 'i', 'l', 'l','e', 'r'};
            //MyString testString = new MyString(testArray);

            
            //testString.Print();
            //testString.Reverse();
            //testString.Print();

            //Console.WriteLine(testString.IndexOf('c'));
            //Console.WriteLine(testString.IndexOf('v'));

            //testString.Remove('c');
            //testString.Print();

            //MyString testStringTwo = new MyString(testArray);
            //testStringTwo.Print();
            //Console.WriteLine(testString.Equals(testStringTwo));

            //testString.Remove('c');
            //Console.WriteLine(testString.Equals(testStringTwo));

            //Car carOne = new Car();
            //Console.WriteLine(testString.Equals(carOne));

            //////////////Demo main menu//////////////////////////

            //lenght of 20 is arbitrary
            MyString [] userStrings = new MyString[20]; 
            bool quitProgram = false;

            //Main Loop
            while (quitProgram == false) {
                Console.Clear();
                Console.WriteLine("Part B Main Menu");
                Console.WriteLine("1: Create a String");
                Console.WriteLine("2: Reverse a string");
                Console.WriteLine("3: Remove c from a string");
                Console.WriteLine("4: Check the equalilty of an object to MyString");
                Console.WriteLine("5: Print MyString");
                Console.WriteLine("6: Quit");
                Console.WriteLine("Please type in the number of the method you want to use.");

                string wait;
                int userArrayIndex = 0;
                
                //Get User input
                string userInput = Console.ReadLine();
                int result = Int32.Parse(userInput);

                //Handle user input
                switch (result) {
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

                    //wait
                    wait = Console.ReadLine();
                    break;
                case 2:
                    
                    break;
                case 3:
                    Console.WriteLine("Wednesday");
                    break;
                case 4:
                    Console.WriteLine("Thursday");
                    break;
                case 5:
                    //Display all user created Strings
                    for (int i = 0; i < ) {

                    }

                    //Prompt them to pick one

                    //print that string
                    
                    userStrings[userArrayIndex - 1].Print();
                    wait = Console.ReadLine();
                    break;
                case 6:
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

                public Node () {
                    this.next = null;
                }

                // Constructor (2 marks)
                public Node (char item, Node next) {
                    this.item = item;
                    this.next = next;
                }
            }

            private Node front = new Node();      // Reference to the first (header) node
            private int length = 0;         // Number of characters in MyString

            // Initialize with a header node an instance of MyString to the given character array A (4 marks)
            public MyString (char[ ] A) {
                Node tempPointer = front;   //Points to where the last node in the linked list is
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
                //Return -1 if the c was not found in MyString 
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
                
                //for each character/node in the linked list, print out the character
                Node String = front.next;
                for (int tempLength = length; tempLength > 0; tempLength--) {
                    Console.WriteLine(String.item);
                    String = String.next;
                }

            }
        }
    }
}