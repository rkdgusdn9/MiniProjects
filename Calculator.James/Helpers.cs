
namespace Calculator.James
{
    public static class Helpers
    {
        public static ICalculate ParseStringToOperators(string stringInput)
        {
            var chars = stringInput.Replace(" ", "").Select(x => x).ToList();

            var next = GetNextOperator(chars, 0);

            return HandleOperator(chars, next.move, next.Operator, next.isOpen).Operator;
        }

        public static (ICalculate Operator, int skipTo) HandleOperator(
            IList<char> chars, 
            int currentIndex, 
            ICalculate leftOperator, 
            bool isOpeningBracket)
        {
            IOperator mathOperator = null;

            //var skipIndex = 0;
            var i = currentIndex;
            for (i = currentIndex; i < chars.Count;)
            {
                var current = GetNextOperator(chars, i);
                i += current.move;

                if (isOpeningBracket)
                {
                    var bracketGroup = HandleOperator(chars, i, current.Operator, current.isOpen);
                    leftOperator = bracketGroup.Operator;
                    i = bracketGroup.skipTo;
                    isOpeningBracket = false;
                    continue;
                }

                if (current.isOpen)
                {
                    var mooov = GetNextOperator(chars, i);

                    var bracketGroup = HandleOperator(chars, i, current.Operator, current.isOpen);
                    mathOperator.Add(bracketGroup.Operator);
                    i = bracketGroup.skipTo;
                    isOpeningBracket = false;
                    continue;
                }

                if (current.isClose)
                {
                    return (mathOperator, i);
                }

                if (current.isNumber)
                {
                    // look forward operator to see how to handle current right operation
                    var forward = GetNextOperator(chars, i);

                    if (forward.isClose)
                    {
                        mathOperator.Add(current.Operator);
                        return (mathOperator, i + forward.move);
                    }

                    var nextOperator = forward.Operator as IOperator;

                    if (nextOperator == null || nextOperator.Strength == mathOperator.Strength)
                    {
                        mathOperator.Add(current.Operator);
                    }
                    else if (nextOperator.Strength > mathOperator.Strength)
                    {
                        // go deeper because right operator should be handled in the nextnext operator group
                        var deeperLayer = HandleOperator(chars, i, current.Operator, current.isOpen);
                        mathOperator.Add(deeperLayer.Operator);
                        i += deeperLayer.skipTo;
                    }
                    else if (nextOperator.Strength < mathOperator.Strength)
                    {
                        mathOperator.Add(current.Operator);
                        leftOperator = mathOperator;
                    }
                    else
                    {
                        throw new Exception("Should not fall here but may be there could be some case?");
                    }
                }
                else
                {
                    var newOperator = current.Operator as IOperator;
                    if (mathOperator == null)
                    {
                        // this layer operator
                        mathOperator = newOperator;
                        mathOperator.Add(leftOperator);
                    } 
                    else if(mathOperator.IsSameOperator(newOperator))
                    {
                        // Do nothing as it continues same operator in this layer


                    }
                }

                var d = "";
            }

            return (mathOperator == null ? leftOperator : mathOperator, i);
        }

        public static (ICalculate Operator, int move, bool isNumber, bool isOpen, bool isClose) GetNextOperator(IList<char> chars, int currentIndex)
        {
            var isNumber = false;
            var isOpenBracket = false;
            var isClosingBracket = false;
            var currentNumberString = "";

            ICalculate mathOperator = null;
            var move = 0;
            for (var i = currentIndex; i < chars.Count; i++)
            {
                if (mathOperator != null)
                {
                    break;
                }

                var single = chars[i];

                if (string.IsNullOrEmpty(currentNumberString))
                {
                    move++;

                    // no opereator yet found most likely first loop
                    if (IsNumber(single) || IsDecimalDot(single))
                    {
                        currentNumberString += single.ToString();
                    }
                    else if (IsAddition(single))
                    {
                        mathOperator = new Addition();
                    }
                    else if (IsSubtraction(single))
                    {
                        mathOperator = new Subtraction();
                    }
                    else if (IsMultiplication(single))
                    {
                        mathOperator = new Multiplication();
                    }
                    else if (IsDivision(single))
                    {
                        mathOperator = new Division();
                    }
                    else if (IsOpeningBracket(single))
                    {
                        isOpenBracket = true;
                        break;
                    }
                    else if (IsClosingBracket(single))
                    {
                        isClosingBracket = true;
                        break;
                    }
                }
                else
                {
                    if (IsNumber(single) || IsDecimalDot(single))
                    {
                        // continuous number string
                        currentNumberString += single.ToString();
                        move++;
                    }
                    else
                    {
                        // End of continuous number string
                        break;
                    }
                }
            }

            if (mathOperator == null && !string.IsNullOrEmpty(currentNumberString))
            {
                mathOperator = new SingleInput(Convert.ToDecimal(currentNumberString));
                isNumber = true;
            }

            return (mathOperator, move, isNumber, isOpenBracket, isClosingBracket);
        }

        public static bool IsNumber(char singleChar) => singleChar >= 48 && singleChar <= 57;

        public static bool IsDecimalDot(char singleChar) => singleChar == 46;

        public static bool IsAddition(char singleChar) => singleChar == 43;

        public static bool IsSubtraction(char singleChar) => singleChar == 45;

        public static bool IsMultiplication(char singleChar) => singleChar == 42;

        public static bool IsDivision(char singleChar) => singleChar == 47;

        public static bool IsOpeningBracket(char singleChar) => singleChar == 40;

        public static bool IsClosingBracket(char singleChar) => singleChar == 41;


    }
}
