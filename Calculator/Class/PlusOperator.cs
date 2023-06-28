using System;
namespace Calculator.Class
{
	public class PlusOperator : Operation
	{
        public int plusResult;

		public PlusOperator()
		{
		}
        public override void Result()
        {
            plusResult = userInput.input1 + userInput.input2;
            Console.WriteLine($"{userInput.input1} + {userInput.input2} = {plusResult}");
        }
    }
}
