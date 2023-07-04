namespace Calculator.James
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*  *Calculator * *");
            Console.WriteLine("Type your mathmatical problem!");

            var input = Console.ReadLine();

            var parsedOperators = Helpers.ParseStringToOperators(input);

            ICalculate calculator = new Calculator(parsedOperators);

            var result = calculator.CalculateResult();

            Console.WriteLine($"Result is: {result}");

            Console.ReadKey();
        }
    }
}