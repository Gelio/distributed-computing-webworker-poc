using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Common;

namespace ServerRunner
{
    public class ServerRunner
    {
        public static void Main()
        {
            /**
             * The DLL with the ISubtask and ITask is loaded here just in order to run the task and for the ITask
             * to be able to aggregate the results.
             * 
             * In the final version the input data will be sent to the distributed nodes who will be responsible
             * for executing the ISubtask compiled to JS.
             */
            Console.WriteLine("Path to DLL containing the task:");
            var taskAssemblyPath = Console.ReadLine();
            var taskAssembly = Assembly.LoadFrom(taskAssemblyPath);
            var task = GetTypeFromAssembly<ITask>(taskAssembly);
            var subtask = GetTypeFromAssembly<ISubtask>(taskAssembly);

            Console.WriteLine($"Input data for the distributed task ({subtask.GetType().FullName}):");
            var inputData = Console.ReadLine();

            var subtaskFactory = new SubtaskFactory();
            task.DefineTasks(inputData, subtaskFactory);

            var results = subtaskFactory.SubtasksInputs.Select(subtask.Perform).ToArray();
            var aggregatedResult = task.AggregateResults(inputData, results);

            Console.WriteLine("The final aggregated result is: " + aggregatedResult);
        }

        private static T GetTypeFromAssembly<T>(Assembly assembly) where T : class
        {
            // TODO: check if there are multiple implementations of the interface to avoid ambiguities
            foreach (var assemblyType in assembly.GetTypes())
            {
                var typeExample = assemblyType.GetInterface(typeof(T).Name);
                if (typeExample == null)
                    continue;

                T instance = (T)assembly.CreateInstance(assemblyType.FullName);
                if (instance != null)
                {
                    return instance;
                }
            }

            throw new Exception("The assembly does not contain an class that implements the " + typeof(T).Name + " interface");
        }
    }
}
