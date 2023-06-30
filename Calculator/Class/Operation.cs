using System;
namespace Calculator.Class
{
	public abstract class Operation
	{
		public Input UserInput { get; set; } // input 자체도 opereation를 inherit해야되니깐 operation안에 input이 있으면 안되. input이 곧 operation이여야되
		public Operation()
		{
			UserInput = new Input();
		}
        public abstract decimal Result();
    }
}

