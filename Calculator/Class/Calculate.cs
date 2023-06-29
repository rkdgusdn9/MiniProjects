using System;
namespace Calculator.Class
{
	public class Calculate
	{
		public decimal calculateResult;
		public Calculate(Operation operation)
		{
			calculateResult = operation.Result();
		}
		public decimal CalculateResult() {
			return calculateResult;
		}
	}
}

