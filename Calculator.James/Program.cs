namespace Calculator.James
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RunTest(1, "3+5+2", 10);
            RunTest(2, "5+2*2", 9);
            RunTest(3, "5+(2*2)", 9);
            RunTest(4, "(5+2)*2", 14);
            RunTest(5, "(5+2)*(6-2)", 28);

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
            Console.WriteLine($"  Result: {result}, Expected: {expectedResult}, Pass?: {result == expectedResult}");
        }
    }
}