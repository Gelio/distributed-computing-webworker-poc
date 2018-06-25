using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace ServerRunner
{
    public class ServerRunner
    {
        public static void Main()
        {
            Console.WriteLine("Path to DLL containing the distributed task:");
            var distributedTaskAssemblyPath = Console.ReadLine();
            var distributedTaskAssembly = Assembly.LoadFrom(distributedTaskAssemblyPath);
            var distributedTask = GetTypeFromAssembly<IDistributedTask>(distributedTaskAssembly);

            /**
             * The DLL with the ITask is loaded here just in order to run the task and for the IDistributedTask
             * to be able to aggregate the results.
             * 
             * In the final version the input data will be sent to the distributed nodes who will be responsible
             * for executing the ITask compiled to JS.
             */
            Console.WriteLine("Path to DLL containing the task:");
            var taskAssemblyPath = Console.ReadLine();
            var taskAssembly = Assembly.LoadFrom(taskAssemblyPath);
            var task = GetTypeFromAssembly<ITask>(taskAssembly);

            Console.WriteLine($"Input data for the distributed task ({distributedTask.GetType().FullName}):");
            var inputData = Console.ReadLine();

            var taskFactory = new TaskFactory();
            distributedTask.DefineTasks(inputData, taskFactory);

            var results = taskFactory.TaskInputs.Select(task.Perform).ToArray();
            var aggregatedResult = distributedTask.AggregateResults(inputData, results);

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

                if (assembly.CreateInstance(assemblyType.FullName) is T instance)
                    return instance;
            }

            throw new Exception("The assembly does not contain an class that implements the " + nameof(IDistributedTask) + " interface");
        }
    }
}
