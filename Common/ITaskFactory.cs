using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface ITaskFactory
    {
        // TODO: use more developer-friendly types instead of string
        void CreateNewTask(string input);
    }
}
