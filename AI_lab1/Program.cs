using System;
using System.ComponentModel;

namespace AI_lab1
{
    internal class Program 
    {
        static void Main(string[] args)
        {
            BackPropagation.Test();
            BackPropagation.Leanrning();
            BackPropagation.Test();
            BackPropagation.Test(11, 2);

            Console.WriteLine("\n\n\nLogic functions:");
            Console.WriteLine(0 + " AND " + 0 + " = " + LogicFunctions.AND(0, 0));
            Console.WriteLine(0 + " AND " + 1 + " = " + LogicFunctions.AND(0, 1));
            Console.WriteLine(1 + " AND " + 0 + " = " + LogicFunctions.AND(1, 0));
            Console.WriteLine(1 + " AND " + 1 + " = " + LogicFunctions.AND(1, 1));
            Console.WriteLine("---------------");
            Console.WriteLine(0 + " OR " + 0 + " = " + LogicFunctions.OR(0, 0));
            Console.WriteLine(0 + " OR " + 1 + " = " + LogicFunctions.OR(0, 1));
            Console.WriteLine(1 + " OR " + 0 + " = " + LogicFunctions.OR(1, 0));
            Console.WriteLine(1 + " OR " + 1 + " = " + LogicFunctions.OR(1, 1));
            Console.WriteLine("---------------");
            Console.WriteLine(0 + " XOR " + 0 + " = " + LogicFunctions.XOR(0, 0));
            Console.WriteLine(0 + " XOR " + 1 + " = " + LogicFunctions.XOR(0, 1));
            Console.WriteLine(1 + " XOR " + 0 + " = " + LogicFunctions.XOR(1, 0));
            Console.WriteLine(1 + " XOR " + 1 + " = " + LogicFunctions.XOR(1, 1));
            Console.WriteLine("---------------");
            Console.WriteLine("NOT " + 0 + " = " + LogicFunctions.NOT(0));
            Console.WriteLine("NOT " + 1 + " = " + LogicFunctions.NOT(1));
            Console.ReadKey();
        }
    }
}
