using System;
namespace Calculator.Class
{
	public class PlusOperator : Operation
	{
        public decimal plusResult { get; set; }
		public PlusOperator(decimal input1, decimal input2) : base()
		{
            UserInput.input1 = input1;
            UserInput.input2 = input2;
		}
        public override decimal Result()
        {
            plusResult = UserInput.input1 + UserInput.input2;
            //Console.WriteLine($"{userInput.input1} + {userInput.input2} = {plusResult}");
            return plusResult;
        }
    }
}
