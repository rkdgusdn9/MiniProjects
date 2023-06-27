using System;
namespace Calculator.Class
{
	public class MinusOperator : Input
	{
        public int minusResult;
		public MinusOperator()
		{
		}
        public override void Result()
        {
            minusResult = input1 - input2;
            Console.WriteLine($"{input1} + {input2} = {minusResult}");
        }
    }
}

