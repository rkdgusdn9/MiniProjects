using System;
namespace Calculator.Class
{
	public class PlusOperator : Input
	{
        public int plusResult;

		public PlusOperator()
		{
		}
        public override void Result()
        {
            plusResult = input1 + input2;
            Console.WriteLine($"{input1} + {input2} = {plusResult}");
        }
    }
}

