using System;
namespace Calculator.Class
{
	public abstract class Operation
	{
		public Input userInput;
		public Operation()
		{
		}
        public abstract void Result();
    }
}

