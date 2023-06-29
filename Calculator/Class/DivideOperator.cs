using System;
namespace Calculator.Class
{
	public class DivideOperator : Operation
    {
        public decimal divideResult { get; set; }
        public DivideOperator(decimal input1, decimal input2) : base()
        {
            UserInput.input1 = input1;
            UserInput.input2 = input2;
        }
        public override decimal Result()
        {
            divideResult = UserInput.input1 / UserInput.input2;
            //Console.WriteLine($"{userInput.input1} / {userInput.input2} = {divideResult}");
            return divideResult;
        }
    }
}

