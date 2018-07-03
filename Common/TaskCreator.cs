using System;
using System.Reflection;

namespace Common
{
    public static class TaskCreator
    {
        public static ISubtask CreateTask(string assemblyName, string className)
        {
            var assembly = Assembly.Load(assemblyName);
            var classType = assembly.GetType(className);

            if (classType.GetInterface(nameof(ISubtask)) == null)
                throw new ArgumentException($"Class {className} does not implement the {nameof(ISubtask)} interface");

            return (ISubtask)Activator.CreateInstance(classType);
        }
    }
}