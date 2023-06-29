using System;
namespace Calculator.Class
{
	public abstract class Operation
	{
		public Input UserInput { get; set; }
		public Operation()
		{
			UserInput = new Input();
		}
        public abstract decimal Result();
    }
}

