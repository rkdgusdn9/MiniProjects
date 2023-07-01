using System;
namespace Calculator.Class
{
	public abstract class Operation
	{
		public Input UserInput { get; set; } // input 자체도 opereation를 inherit해야되고, Input보단 operation이여야되고, List<Operation> 이면 input갯수에 제한되지 않아서 좋을꺼같아
		public Operation()
		{
			UserInput = new Input();
		}
        public abstract decimal Result();
    }
}

