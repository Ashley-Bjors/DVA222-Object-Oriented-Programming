namespace Lab1
{
    namespace Task1
    {
        public class Plate64U{
            public UInt64 Value;
            public UInt64 Min;

            public Plate64U(UInt64 n)
            {
                Value = n;
            }

            public Plate64U()
            {

            }
        }
        public class Stack64U {
            public Plate64U[] Stack;
            public int Len;
            public int CurPos;
            public Stack64U(UInt64 n)
            {
                Len = 1;
                Stack = new Plate64U[Len];
                Stack[0] = new Plate64U(n);
                Stack[0].Min = n;
                CurPos = 1;
                System.Console.WriteLine($"Pushed {n} into index {0}");

            }
            public Stack64U()
            {
                Len = 1;
                Stack = new Plate64U[Len];
                CurPos = 0;

            }
            public void Push(UInt64 n)
            {
                if(CurPos == Len)
                {
                    Len = Len*2;
                    Array.Resize(ref Stack, Len);
                }
                Stack[CurPos] = new Plate64U(n);
                System.Console.WriteLine($"Pushed {n} into index {CurPos}");

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

            public void Pop()
            {
                if(CurPos == 0)
                {
                    throw new Exception("cannot pop empty stack");
                }
                System.Console.WriteLine($"Popped value {Stack[CurPos-1].Value} from index {CurPos-1}");
                CurPos--;
                if(CurPos == Len/2)
                {
                    Len = Len/2;
                    Array.Resize(ref Stack, Len);
                }
            }

            public void Peek()
            {
                System.Console.WriteLine($"Peeked value {Stack[CurPos-1].Value} in index {CurPos-1}");
            }

            public void Empty()
            {
                if(CurPos==0)
                {
                    System.Console.WriteLine("Stack Is Empty");
                }
                else
                {
                    System.Console.WriteLine("Stack Not Empty");
                }
            }

            public void Size()
            {
                System.Console.WriteLine($"Stack occupies {CurPos} out of {Len} positions");
            }

            public void Min()
            {
                System.Console.WriteLine($"Minimum value in stack is {Stack[CurPos-1].Min}");
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
                Task1.Stack64U Task1Stack = new Task1.Stack64U(8);
                Task1Stack.Push(3);
                Task1Stack.Push(15);
                Task1Stack.Push(4);
                Task1Stack.Peek();
                Task1Stack.Size();
                Task1Stack.Min();
                Task1Stack.Push(8);
                Task1Stack.Size();
                Task1Stack.Pop();
                Task1Stack.Size();
                Task1Stack.Empty();
                Task1Stack.Pop();
                Task1Stack.Pop();
                Task1Stack.Pop();
                Task1Stack.Min();
                Task1Stack.Pop();
                Task1Stack.Empty();
                //Task1Stack.Pop(); //Throws exception
                System.Console.WriteLine("\nTask3:");
                Task3.Stack64U Task3Stack = new Task3.Stack64U(8);
                Task3Stack.Push(3);
                Task3Stack.Push(15);
                Task3Stack.Push(4);
                Task3Stack.Peek();
                Task3Stack.Size();
                Task3Stack.Min();
                Task3Stack.Push(8);
                Task3Stack.Size();
                Task3Stack.Pop();
                Task3Stack.Size();
                Task3Stack.Empty();
                Task3Stack.Pop();
                Task3Stack.Pop();
                Task3Stack.Pop();
                Task3Stack.Min();
                Task3Stack.Pop();
                Task3Stack.Empty();
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
            public List<UInt64> Stack;
            public int count;
            public int CurPos;
            public Stack64U(UInt64 n)
            {
                Stack = new List<UInt64>();
                Stack.Add(n);
                System.Console.WriteLine($"Pushed {n} into index {Stack.Count-1}");

            }
            public Stack64U()
            {
                Stack = new List<UInt64>();

            }
            public void Push(UInt64 n)
            {
                Stack.Add(n);
                System.Console.WriteLine($"Pushed {n} into index {Stack.Count-1}");
            }

            public void Pop()
            {
                if(Stack.Count == 0)
                {
                    throw new Exception("cannot pop empty stack");
                }
                System.Console.WriteLine($"Popped value {Stack[Stack.Count-1]} from index {Stack.Count-1}");
                Stack.RemoveAt(Stack.Count-1);
            }

            public void Peek()
            {
                System.Console.WriteLine($"Peeked value {Stack[Stack.Count-1]} in index {Stack.Count-1}");
            }

            public void Empty()
            {
                if(Stack.Count==0)
                {
                    System.Console.WriteLine("Stack Is Empty");
                }
                else
                {
                    System.Console.WriteLine("Stack Not Empty");
                }
            }

            public void Size()
            {
                System.Console.WriteLine($"Stack occupies {Stack.Count} out of {Stack.Count} positions");
            }

            public void Min()
            {
                System.Console.WriteLine($"Minimum value in stack is {Stack.Min()}");
            }
        }    
    }
}