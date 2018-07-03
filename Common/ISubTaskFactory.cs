using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface ISubtaskFactory
    {
        // TODO: use more developer-friendly types instead of string
        void CreateNewTask(string input);
    }
}
