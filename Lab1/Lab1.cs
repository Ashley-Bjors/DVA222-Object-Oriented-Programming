namespace Lab1
{
    
    namespace Task1
    {
        public class Plate64U{
            public UInt64 Value;
            public UInt64 Min;

            public Plate64U(UInt64 n)//Init a plate with a value
            {
                Value = n;
            }

            public Plate64U()//Init a plate without a value
            {

            }
        }
        public class Stack64U {
            private Plate64U[] Stack;
            private int Len;
            private int CurPos;
            public Stack64U(UInt64 n)//Init a Stack with Plate with a value
            {
                Len = 1;
                Stack = new Plate64U[Len];
                Stack[0] = new Plate64U(n);
                Stack[0].Min = n;
                CurPos = 1;
                //System.Console.WriteLine($"DEBUG: Pushed {n} into index {0}");

            }
            public Stack64U()//Init a Stack with Plate without a value
            {
                Len = 1;
                Stack = new Plate64U[Len];
                CurPos = 0;

            }
            public void Push(UInt64 n)//Add a plate to the top of the stack. Resize if necessary. Includes setting the minimum item inside the current stack(including the current plate) 
            {
                if(CurPos == Len)
                {
                    Len = Len*2;
                    Array.Resize(ref Stack, Len);
                }
                Stack[CurPos] = new Plate64U(n);
                //System.Console.WriteLine($"DEBUG: Pushed {n} into index {CurPos}");

                if(CurPos > 0)
                {
                    if(Stack[CurPos-1].Min > n)
                    {
                        Stack[CurPos].Min = n;
                    }
                    else
                    {
                        Stack[CurPos].Min = Stack[CurPos-1].Min;
                    }
                }
                else
                {
                    Stack[CurPos].Min = n;
                }
                CurPos++;
            }

            public UInt64 Pop()//Remove a plate to the top of the stack. Resize if necessary. Throw Exception if Stack is empty.
            {
                if(this.Empty())
                {
                    throw new Exception("cannot pop empty stack");
                }
                //System.Console.WriteLine($"DEBUG: Popped value {Stack[CurPos-1].Value} from index {CurPos-1}");
                UInt64 temp = Stack[CurPos-1].Value;
                CurPos--;
                if(CurPos == Len/2)
                {
                    Len = Len/2;
                    Array.Resize(ref Stack, Len);
                }
                return temp;
            }

            public UInt64 Peek()//Look the top plate in stack.
            {
                //System.Console.WriteLine($"DEBUG: Peeked value {Stack[CurPos-1].Value} in index {CurPos-1}");
                return Stack[CurPos-1].Value;

            }

            public bool Empty()//Check if stack is empty
            {
                if(CurPos==0)
                {
                    //System.Console.WriteLine($"DEBUG: Stack Is Empty");
                    return true;
                }
                else
                {
                    //System.Console.WriteLine($"$DEBUG: Stack Not Empty");
                    return false;
                }
            }

            public int Size()//Check nr of elements inside Stack
            {
                //System.Console.WriteLine($"DEBUG: Stack occupies {CurPos} out of {Len} positions");
                return Len;
            }

            public UInt64 Min()//Checke smallest value inside Stack
            {
                //System.Console.WriteLine($"DEBUG: Minimum value in stack is {Stack[CurPos-1].Min}");
                return Stack[CurPos-1].Min;
            }
        }    
    }
    namespace Task2
    {
        using Task1;
        using Task3;    
        class Program {
            static void Main()
            {
                System.Console.WriteLine("Task1:");
                Task1.Stack64U Task1Stack = new Task1.Stack64U(8);    System.Console.WriteLine($"INPUT: {8}");
                Task1Stack.Push(3);    System.Console.WriteLine($"INPUT: {3}");
                Task1Stack.Push(15);    System.Console.WriteLine($"INPUT: {15}");
                Task1Stack.Push(4);    System.Console.WriteLine($"INPUT: {4}");
                System.Console.WriteLine($"Peek: {Task1Stack.Peek()}");
                System.Console.WriteLine($"Size: {Task1Stack.Size()}");
                System.Console.WriteLine($"Min: {Task1Stack.Min()}");
                Task1Stack.Push(8);    System.Console.WriteLine($"INPUT: {8}");
                System.Console.WriteLine($"Size: {Task1Stack.Size()}");
                System.Console.WriteLine($"Pop: {Task1Stack.Pop()}");
                System.Console.WriteLine($"Size: {Task1Stack.Size()}");
                System.Console.WriteLine($"Empty: {Task1Stack.Empty()}");
                System.Console.WriteLine($"Pop: {Task1Stack.Pop()}");
                System.Console.WriteLine($"Pop: {Task1Stack.Pop()}");
                System.Console.WriteLine($"Pop: {Task1Stack.Pop()}");
                System.Console.WriteLine($"Min: {Task1Stack.Min()}");
                System.Console.WriteLine($"Pop: {Task1Stack.Pop()}");
                System.Console.WriteLine($"Empty: {Task1Stack.Empty()}");
                //Task1Stack.Pop(); //Throws exception
                System.Console.WriteLine("\nTask3:");
                Task3.Stack64U Task3Stack = new Task3.Stack64U(8);    System.Console.WriteLine($"INPUT: {8}");
                Task3Stack.Push(3);    System.Console.WriteLine($"INPUT: {3}");
                Task3Stack.Push(15);    System.Console.WriteLine($"INPUT: {15}");
                Task3Stack.Push(4);    System.Console.WriteLine($"INPUT: {4}");
                System.Console.WriteLine($"Peek: {Task3Stack.Peek()}");
                System.Console.WriteLine($"Size: {Task3Stack.Size()}");
                System.Console.WriteLine($"Min: {Task3Stack.Min()}");
                Task3Stack.Push(8);    System.Console.WriteLine($"INPUT: {8}");
                System.Console.WriteLine($"Size: {Task3Stack.Size()}");
                System.Console.WriteLine($"Pop: {Task3Stack.Pop()}");
                System.Console.WriteLine($"Size: {Task3Stack.Size()}");
                System.Console.WriteLine($"Empty: {Task3Stack.Empty()}");
                System.Console.WriteLine($"Pop: {Task3Stack.Pop()}");
                System.Console.WriteLine($"Pop: {Task3Stack.Pop()}");
                System.Console.WriteLine($"Pop: {Task3Stack.Pop()}");
                System.Console.WriteLine($"Min: {Task3Stack.Min()}");
                System.Console.WriteLine($"Pop: {Task3Stack.Pop()}");
                System.Console.WriteLine($"Empty: {Task3Stack.Empty()}");
                //Task3Stack.Pop(); //Throws exception
            }
        }
    }
    namespace Task3
    {
        public class Plate64U{
            public UInt64 Value;

            public Plate64U(UInt64 n)
            {
                Value = n;
            }

            public Plate64U()
            {

            }
        }
        public class Stack64U {
            private List<UInt64> Stack;
            private int count;
            private int CurPos;
            public Stack64U(UInt64 n)//Init a Stack with Plate with a value
            {
                Stack = new List<UInt64>();
                Stack.Add(n);
                //System.Console.WriteLine($"DEBUG: Pushed {n} into index {Stack.Count-1}");

            }
            public Stack64U()//Init a Stack with Plate without a value
            {
                Stack = new List<UInt64>();

            }
            public void Push(UInt64 n)//Add a plate to the top of the stack.
            {
                Stack.Add(n);
                //System.Console.WriteLine($"DEBUG: Pushed {n} into index {Stack.Count-1}");
            }

            public UInt64 Pop()//Remove a plate to the top of the stack. Throw Exception if Stack is empty.
            {
                if(this.Empty())
                {
                    throw new Exception("cannot pop empty stack");
                }
                //System.Console.WriteLine($"DEBUG: Popped value {Stack[Stack.Count-1]} from index {Stack.Count-1}");
                UInt64 temp = Stack[Stack.Count-1];
                Stack.RemoveAt(Stack.Count-1);
                return temp;
            }

            public UInt64 Peek()//Look the top plate in stack.
            {
                //System.Console.WriteLine($"DEBUG: Peeked value {Stack[Stack.Count-1]} in index {Stack.Count-1}");
                return Stack[Stack.Count-1];
            }

            public bool Empty()//Check if stack is empty
            {
                if(Stack.Count==0)
                {
                    //System.Console.WriteLine($"DEBUG: Stack Is Empty");
                    return true;
                }
                else
                {
                    //System.Console.WriteLine($"DEBUG: Stack Not Empty");
                    return false;
                }
            }

            public int Size()//Check nr of elements inside Stack
            {
                //System.Console.WriteLine($"DEBUG: Stack occupies {Stack.Count} out of {Stack.Count} positions");
                return Stack.Count;
            }

            public UInt64 Min()//Checke smallest value inside Stack
            {
                //System.Console.WriteLine($"DEBUG: Minimum value in stack is {Stack.Min()}");
                return Stack.Min();
            }
        }    
    }
}