using System;
namespace Calculator.Class
{
	public class Calculate
	{
		public decimal calculateResult;
		public Calculate(Operation operation)
		{
            // 컨스트럭트에서 이미 답을 가져오는건 안좋은거같아
            // 오퍼레이션을 계산기에 넣어두고 opreation 을 좀 밨꿀수도 있으니깐
            // flexible해지게 CalculateResult call 할때만 답을 resolve하게.
            calculateResult = operation.Result();
		}
		public decimal CalculateResult() {
            // return operation.Result();
            return calculateResult;
		}
	}
}

