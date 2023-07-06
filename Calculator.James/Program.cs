namespace Calculator.James
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //RunTest(1, "3+5+2", 10);
            //RunTest(2, "5+2*2", 9);
            //RunTest(3, "5*2*2-2", 18);
            //RunTest(4, "5/2*2", 5);
            //RunTest(5, "3.2+55.222", 58.422m);

            //RunTest(6, "(5+2)*2", 14);
            //RunTest(7, "5+(2*2)", 9);
            //RunTest(8, "(5+2)*(6-2)", 28);
            //RunTest(9, "(1+2*2)-((3+4)/(4-2))", 1.5m);
            //RunTest(10, "5+2*2", 9);
            RunTest(11, "(((2.1+11.23)*2.3)*(7.22/9)/(2-3)-1)", 9);

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
            Console.WriteLine($"       Pass?: {result == expectedResult}\n");
        }
    }
}