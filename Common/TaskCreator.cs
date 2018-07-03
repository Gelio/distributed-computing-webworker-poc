using System;
using System.Reflection;

namespace Common
{
    public static class TaskCreator
    {
        public static ITask CreateTask(string assemblyName, string className)
        {
            var assembly = Assembly.Load(assemblyName);
            var classType = assembly.GetType(className);

            if (classType.GetInterface(nameof(ITask)) == null)
                throw new ArgumentException($"Class {className} does not implement the {nameof(ITask)} interface");

            return (ITask)Activator.CreateInstance(classType);
        }
    }
}