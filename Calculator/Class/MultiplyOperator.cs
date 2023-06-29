using System;
namespace Calculator.Class
{
	public class MultiplyOperator : Operation
    {
        public decimal multiplyResult { get; set; }
        public MultiplyOperator(decimal input1, decimal input2) : base()
        {
            UserInput.input1 = input1;
            UserInput.input2 = input2;
        }
        public override decimal Result()
        {
            multiplyResult = UserInput.input1 * UserInput.input2;
            //Console.WriteLine($"{userInput.input1} + {userInput.input2} = {multiplyResult}");
            return multiplyResult;
        }
    }
}

