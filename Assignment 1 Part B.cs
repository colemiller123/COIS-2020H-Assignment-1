namespace COIS2020HAssignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Testing
            char [] testArray = {'c', 'o', 'l', 'e'};
            MyString testString = new MyString(testArray);

            //testString.Reverse();
            testString.Print();

        }
        public class MyString {
            private class Node {
                public char item;
                public Node next;

                // Constructor (2 marks)
                public Node (char item, Node next) {
                    this.item = item;
                    this.next = next;
                }
            }

            private Node front = null;      // Reference to the first (header) node
            private int length = 0;         // Number of characters in MyString

            // Initialize with a header node an instance of MyString to the given character array A (4 marks)
            public MyString (char[ ] A) {
                foreach (char character in A) {
                    //Create a new node for each character
                    Node newNode = new Node(character, null);

                    if (length == 0) {
                        front = newNode;
                    }
                    else {
                        
                    }
                    length++;
                }
            }
            
            // Using a stack, reverse this instance of MyString (6 marks)
            public void Reverse () { 
                Stack<char> S;
                S = new Stack<char> ();
                
                //Reverse the order
                while (length != 0) {
                    S.Push(front.item);
                    length--;
                }
                S.Push(front.item);
                Console.Write(S.Peek());

                //Create a
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
                //Testing
                Console.WriteLine(length);
                try {
                    Console.WriteLine(front.item);
                } catch {
                    
                }

                int tempLength = length;
                while (tempLength > 0) {
                    Node tempFront = front;
                    Console.WriteLine(tempFront.item);

                    tempFront = front.next;
                    
                    tempLength--;
                }
            }
        }
    }
}