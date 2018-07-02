using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface ITask
    {
        // TODO: use more developer-friendly types instead of string
        // TODO: reference corresponding ISubTask from ITask
        void DefineTasks(string input, ISubTaskFactory subTaskFactory);

        string AggregateResults(string input, string[] results);
    }
}
