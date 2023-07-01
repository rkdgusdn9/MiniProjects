using System;
using Calculator.Class;

namespace Calculator {
    class Program
    {

        // Test Requirements
        // 1 + 2 + 3 + 4 + 5 = 15       Eg. unlimited inputs
        // 4 + (20 * 2) = 44 
        // (4 / 2) * (5 - 3) + 1 =  5     Eg. complex inputs



        public static Input userInput = new Input();
        public static Operation operation = null;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Calculator\n");
            TwoNumberInput();
            var operationResult = ChooseOperation();
            var cal = new Calculate(operationResult);
            Console.WriteLine("\nResult is: " + cal.calculateResult);

            //Operation operation2 = new PlusOperator(cal.calculateResult, 5);
            //operation2.Result();
            //cal = new Calculate(operation2);
            //Console.WriteLine(cal.calculateResult);

            Console.ReadKey();
        }

        public static void TwoNumberInput()
        {
            Console.WriteLine("Input your First Number: \n");
            userInput.input1 = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Input your Second Number: \n");
            userInput.input2 = Convert.ToDecimal(Console.ReadLine());
        }

        public static Operation ChooseOperation() {
            //Operation operation = null;
            Console.WriteLine("Choose Operation: \n");
            Console.WriteLine("1.Plus\n2.Minus\n3.Multiply\n4.Divide");

            int inputType = Convert.ToInt32(Console.ReadLine());
            switch (inputType)
            {
                case 1:
                    operation = new PlusOperator(userInput.input1, userInput.input2);
                    break;

                case 2:
                    operation = new MinusOperator(userInput.input1, userInput.input2);
                    break;

                case 3:
                    operation = new MultiplyOperator(userInput.input1, userInput.input2);
                    break;

                case 4:
                    operation = new DivideOperator(userInput.input1, userInput.input2);
                    break;
            }
            operation.Result(); // 굳이 여기서 result 을 가져오는 이유는??
            return operation;
        }
    }
}
