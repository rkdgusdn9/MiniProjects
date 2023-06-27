using System;
namespace Calculator.Class
{
	public class MultiplyOperator : Input
	{
        public int multiplyResult;
		public MultiplyOperator()
		{
		}
        public override void Result()
        {
            multiplyResult = input1 * input2;
            Console.WriteLine($"{input1} + {input2} = {multiplyResult}");
        }
    }
}

