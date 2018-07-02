using System.Linq;
using Common;

namespace FactorialTask
{
    public class FactorialTask : ITask
    {
        public void DefineTasks(string input, ISubTaskFactory subTaskFactory)
        {
            var numbers = input.Split(',');

            foreach (var number in numbers)
            {
                subTaskFactory.CreateNewTask(number);
            }
        }

        public string AggregateResults(string input, string[] results)
        {
            return results.Select(int.Parse).Sum().ToString();
        }
    }
}
