using System.Linq;
using Common;

namespace FactorialSumDistributed
{
    public class FactorialSumDistributed : IDistributedTask
    {
        public void DefineTasks(string input, ITaskFactory taskFactory)
        {
            var numbers = input.Split(',');

            foreach (var number in numbers)
            {
                taskFactory.CreateNewTask(number);
            }
        }

        public string AggregateResults(string input, string[] results)
        {
            return results.Select(int.Parse).Sum().ToString();
        }
    }
}
