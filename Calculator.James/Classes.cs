
namespace Calculator.James
{
    public interface ICalculate
    {
        public decimal CalculateResult();
    }

    public interface IOperator : ICalculate
    {
        public int Strength { get; set; }
        public bool IsSameOperator(IOperator compare);
        public bool ShouldCalculateFirst(IOperator compare);
        public void Add(ICalculate item);
    }

    public abstract class Operator : IOperator
    {
        protected List<ICalculate> CalculateItems;

        public int Strength { get; set; }

        public Operator()
        {
            CalculateItems = new List<ICalculate>();
        }

        public void Add(ICalculate item)
        {
            CalculateItems.Add(item);
        }

        public decimal CalculateResult()
        {
            return GetResult();
        }

        public abstract decimal GetResult();

        public bool ShouldCalculateFirst(IOperator compare)
        {
            return Strength > compare.Strength;
        }

        public bool IsSameOperator(IOperator compare)
        {
            return this.GetType() == compare.GetType();
        }
    }

    public class Addition : Operator
    {
        public override decimal GetResult()
        {
            return CalculateItems.Select(x => x.CalculateResult()).Aggregate((a, v) => a + v);
        }
    }

    public class Subtraction : Operator
    {
        public override decimal GetResult()
        {
            return CalculateItems.Select(x => x.CalculateResult()).Aggregate((a, v) => a - v);
        }
    }

    public class Multiplication : Operator
    {
        public Multiplication()
        {
            Strength = 1;
        }

        public override decimal GetResult()
        {
            return CalculateItems.Select(x => x.CalculateResult()).Aggregate((a, v) => a * v);
        }
    }

    public class Division : Operator
    {
        public Division()
        {
            Strength = 1;
        }

        public override decimal GetResult()
        {
            return CalculateItems.Select(x => x.CalculateResult()).Aggregate((a, v) => a / v);
        }
    }


    public class SingleInput : ICalculate
    {
        private decimal Value;
        public SingleInput(decimal value)
        {
            Value = value;
        }

        public decimal CalculateResult()
        {
            return Value;
        }
    }

    public class Calculator : ICalculate
    {
        protected ICalculate CalculatorResult;
        public Calculator(ICalculate calculatorResult)
        {
            CalculatorResult = calculatorResult;
        }

        public decimal CalculateResult()
        {
            return CalculatorResult.CalculateResult();
        }
    }
}
