namespace Exam
{
    //LSP
    public abstract class Calculator
    {
        protected readonly int[] Numbers;

        protected Calculator(int[] numbers)
        {
            Numbers = numbers;
        }

        public abstract int Calculate();
    }

    public class SumCalculator : Calculator
    {
        public SumCalculator(int[] numbers) : base(numbers)
        {
        }

        public override int Calculate()
        {
            return Numbers.Sum();
        }
    }

    public class SumEvenCalculator : Calculator
    {
        public SumEvenCalculator(int[] numbers) : base(numbers)
        {
        }

        public override int Calculate()
        {
            return Numbers.Where(n => n % 2 == 0).Sum();
        }
    }
}
