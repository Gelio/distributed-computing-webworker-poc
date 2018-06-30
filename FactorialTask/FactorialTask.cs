using Common;

namespace FactorialTask
{
    public class FactorialTask
    {
        private int HighestResult = int.MinValue;

        public FactorialTask() { }

        public string Perform(string input)
        {
            var x = int.Parse(input);
            var result = Factorial(x);

            if (HighestResult < result)
            {
                HighestResult = result;
            }

            return result.ToString() + " " + HighestResult.ToString();
        }

        private int Factorial(int n)
        {
            if (n < 0)
            {
                return 0;
            }

            var result = 1;

            for (var i = 1; i <= n; i++)
            {
                result *= i;
            }

            return result;
        }
    }
}