namespace Calculator.James
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunTest(1, "3+5+2", 10); // continuous same small operator
            RunTest(2, "5+2*2", 9); // continuous but bigger operator
            RunTest(3, "5*2*2-2", 18); // continous multiple times with differnt operators
            RunTest(4, "5/2*2", 5); // continous same big operator
            RunTest(5, "3.2+55.222", 58.422m); // handling decimal pointers

            RunTest(6, "(5+2)*2", 14); // handling front brackets
            RunTest(7, "5+(2*2)", 9); // handling back brackets
            RunTest(8, "(5+2)*(6-2)", 28); // handling front and back
            RunTest(9, "(1+2*2)-((3+4)/(4-2))", 1.5m); // handling multiple complex brackets
            RunTest(10, "((2.1+11.23)*2.3)*(7.22/9)/(2-3)-1", -25.5953311111m); // Very complex, Precision up to 8 decimals for now
            RunTest(11, "(2) + 1", 3); // handling single digit wrapped in its own bracket

            Console.WriteLine("\n");
            Console.WriteLine("*  *Calculator * *");
            Console.WriteLine("Type your mathmatical problem!");

            var input = Console.ReadLine();

            var result = Calculate(input);

            Console.WriteLine($"Result is: {result}");

            Console.ReadKey();
        }

        static decimal Calculate(string input)
        {
            var parsedOperators = Helpers.ParseStringToOperators(input);
            ICalculate calculator = new Calculator(parsedOperators);
            return calculator.CalculateResult();
        }

        static void RunTest(int index, string input, decimal expectedResult)
        {
            Console.WriteLine($"Test {index}: {input}");
            var result = Calculate(input);
            Console.WriteLine($"       Actual: {result}, Expected: {expectedResult}");
            Console.WriteLine($"       Pass?: {Math.Round(result, 8) == Math.Round(expectedResult, 8)}\n");
        }
    }
}