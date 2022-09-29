namespace COIS2020HAssignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Testing
            char [] testArray = {'c', 'o', 'l', 'e','m', 'i', 'l', 'l','e', 'r'};
            MyString testString = new MyString(testArray);

            
            testString.Print();
            testString.Reverse();
            testString.Print();

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
            //public int IndexOf(char c) { … }
            
            // Remove all occurrences of c from this instance (4 marks)
            //public void Remove (char c) { … }
            
            // Return true if obj is both of type MyString and the same as this instance; 
            // otherwise false (6 marks)
            //public override bool Equals (object obj) { … }
            
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