using System;
namespace Calculator.Class
{
	public class MinusOperator : Operation
    {
        public decimal minusResult { get; set; }
        public MinusOperator(decimal input1, decimal input2) : base()
        {
            UserInput.input1 = input1;
            UserInput.input2 = input2;
        }
        public override decimal Result()
        {
            minusResult = UserInput.input1 - UserInput.input2;
            //Console.WriteLine($"{userInput.input1} + {userInput.input2} = {minusResult}");
            return minusResult;
        }
    }
}

