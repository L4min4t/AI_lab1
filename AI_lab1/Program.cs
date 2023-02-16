using System;

namespace AI_lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {   
            while(true)
            {
                Console.WriteLine("Choose 1-and, 2-or, 3-xor, 4-not, and enter the two Boolean values as integers.");
                int choose, a, b;
                var numbers = Console.ReadLine().Split(' ');
                choose = int.Parse(numbers[0]);
                a = int.Parse(numbers[1]);
                b = int.Parse(numbers[2]);
                switch (choose)
                {
                    case 1:
                        Console.WriteLine($"result: {LogicFunctions.AND(a, b)}");
                        break;

                    case 2:
                        Console.WriteLine($"result: {LogicFunctions.OR(a, b)}\n");
                        break;

                    case 3:
                        Console.WriteLine($"result: {LogicFunctions.XOR(a, b)}\n");
                        break;

                    case 4:
                        Console.WriteLine($"result: {LogicFunctions.NOT(a)}\n");
                        break;
                }
            }
            
        }
    }
}
