using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ServerRunner
{
    class TaskFactory : ITaskFactory
    {
        public List<string> TaskInputs { get; } = new List<string>();

        public void CreateNewTask(string input)
        {
            // This method should queue instantiating a new task on some distributed node.
            Console.WriteLine("Creating a new task with data: " + input);
            TaskInputs.Add(input);
        }
    }
}
