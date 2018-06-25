using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IDistributedTask
    {
        // TODO: use more developer-friendly types instead of string
        // TODO: reference corresponding ITask from IDistributedTask
        void DefineTasks(string input, ITaskFactory taskFactory);

        string AggregateResults(string input, string[] results);
    }
}
