
namespace Calculator.James
{
    public static class Helpers
    {
        public static ICalculate ParseStringToOperators(string stringInput)
        {
            var chars = stringInput.Replace(" ", "").Select(x => x).ToList();

            return HandleOperatorV2(chars, 0, false).Operator;
        }

        public static (ICalculate Operator, int skipTo) HandleOperatorV2(
            IList<char> chars,
            int startingIndex,
            bool isOpenBracket)
        {
            ICalculate leftOperator = null;
            IOperator mathOperator = null;

            var i = startingIndex;
            for (i = startingIndex; i < chars.Count;)
            {
                var current = GetNextOperator(chars, i);
                i += current.move;

                var currentOperator = current.Operator;
                var currentIsValue = current.isNumber;

                if (current.isOpen)
                {
                    var bracketGroup = HandleOperatorV2(chars, i, current.isOpen);
                    currentOperator = bracketGroup.Operator;
                    i = bracketGroup.skipTo;
                    currentIsValue = true;
                }

                if (current.isClose)
                {
                    return (mathOperator == null ? leftOperator : mathOperator, i);
                }

                if (leftOperator == null)
                {
                    leftOperator = currentOperator;
                    continue;
                }

                if (mathOperator == null)
                {
                    mathOperator = currentOperator as IOperator;
                    mathOperator.Add(leftOperator);
                    continue;
                }

                // Right operator as mathmatical operator
                if (!currentIsValue)
                {
                    var currentOperatorAsIOperator = currentOperator as IOperator;
                    if (mathOperator.IsSameOperator(currentOperatorAsIOperator))
                    {
                        // if same operator just skip
                        continue;
                    }

                    if (mathOperator.Strength >= currentOperatorAsIOperator.Strength)
                    {
                        leftOperator = mathOperator;
                        mathOperator = currentOperatorAsIOperator;
                        mathOperator.Add(leftOperator);
                        continue;
                    }
                }


                // Right operator as numbers - look ahead to handle special cases
                var next = GetNextOperator(chars, i);

                if (next.isClose)
                {
                    if (isOpenBracket)
                    {
                        mathOperator.Add(currentOperator);
                        return (mathOperator, i + 1);
                    }
                    else
                    {
                        mathOperator.Add(currentOperator);
                        return (mathOperator, i);
                    }
                }

                var nextOperator = next.Operator as IOperator;

                if (nextOperator == null || nextOperator.IsSameOperator(mathOperator))
                {
                    // either no more operator found OR same next operator and just continue
                    mathOperator.Add(currentOperator);
                }
                else if (nextOperator.Strength > mathOperator.Strength)
                {
                    // go deeper because right operator should be handled as a result of a group
                    var indexOfCurrentForNestToHandle = i - current.move;
                    var nest = HandleOperatorV2(chars, indexOfCurrentForNestToHandle, current.isOpen);
                    mathOperator.Add(nest.Operator);
                    i = nest.skipTo;
                }
                else if (nextOperator.Strength <= mathOperator.Strength)
                {
                    mathOperator.Add(currentOperator);
                    leftOperator = mathOperator;
                    mathOperator = null;
                }
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
