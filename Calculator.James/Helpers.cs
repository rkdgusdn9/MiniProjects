
namespace Calculator.James
{
    public static class Helpers
    {
        public static ICalculate ParseStringToOperators(string stringInput)
        {
            var chars = stringInput.Replace(" ", "").Select(x => x).ToList();

            var next = GetNextOperator(chars, 0);

            return HandleOperator(chars, next.skipTo, next.Operator).Operator;
        }

        public static (ICalculate Operator, int skipTo) HandleOperator(IList<char> chars, int currentIndex, ICalculate leftOperator)
        {
            IOperator mathOperator = null;

            var skipIndex = 0;
            for (var i = currentIndex; i < chars.Count; i = i + skipIndex)
            {
                var next = GetNextOperator(chars, i);
                skipIndex = next.skipTo;

                if (next.isNumber)
                {
                    // look forward operator to see how to handle current right operation
                    var nextStartIndex = i + skipIndex;
                    var nextNext = GetNextOperator(chars, nextStartIndex);
                    var nextOperator = nextNext.Operator as IOperator;

                    if (nextOperator != null && nextOperator.ShouldCalculateFirst(mathOperator))
                    {
                        // go deeper because right operator should be handled in the nextnext operator group
                        var deeperLayer = HandleOperator(chars, nextStartIndex, next.Operator);
                        mathOperator.Add(deeperLayer.Operator);
                        skipIndex += nextNext.skipTo + deeperLayer.skipTo;
                    }
                    else
                    {
                        // Add right operator
                        mathOperator.Add(next.Operator);
                    }
                }
                else
                {
                    // handle math operator
                    mathOperator = next.Operator as IOperator;
                    mathOperator.Add(leftOperator);
                }

                var d = "";
            }

            return (mathOperator, skipIndex);
        }

        public static (ICalculate Operator, int skipTo, bool isNumber) GetNextOperator(IList<char> chars, int currentIndex)
        {
            var isNumber = false;
            var currentNumberString = "";

            ICalculate mathOperator = null;
            var skip = 0;
            for (var i = currentIndex; i < chars.Count; i++)
            {
                if (mathOperator != null)
                {
                    break;
                }

                var single = chars[i];

                if (string.IsNullOrEmpty(currentNumberString))
                {
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

                    skip++;
                }
                else
                {
                    if (IsNumber(single) || IsDecimalDot(single))
                    {
                        // continuous number string
                        currentNumberString += single.ToString();
                        skip++;
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

            return (mathOperator, skip, isNumber);
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
