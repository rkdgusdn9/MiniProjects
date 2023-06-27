using System;
namespace Calculator.Class
{
	public class DivideOperator : Input
    {
        public int divideResult;
		public DivideOperator()
		{
		}
        public override void Result()
        {
            divideResult = input1 / input2;
            Console.WriteLine($"{input1} + {input2} = {divideResult}");
        }
    }
}

