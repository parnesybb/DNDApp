using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly myAssembly = Assembly.GetExecutingAssembly();
            string className;
            if (args.Length == 1)
            {
                className = args[0];
            }
            else
            {
                Console.Write("Enter the name of the class you would like to run: ");
                className = Console.ReadLine();
            }

            Type type = myAssembly.GetType("TestDriver."+ className);
            Console.WriteLine("Class: TestDriver." + className);

            if (type != null)
            {

                MethodInfo methodInfo = type.GetMethod("run", new Type[0]);

                if (methodInfo != null)
                {
                    object result = null;
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    object classInstance = Activator.CreateInstance(type, null);
                    if (parameters.Length == 0)
                    {
                        //This works fine
                        result = methodInfo.Invoke(classInstance, null);
                    }
                    else
                    {
                        string curValue;
                        Console.WriteLine("function run");
                        foreach (ParameterInfo param in parameters)
                        {
                            Console.WriteLine("param: " + param.ParameterType.Name + " " + param.Name + " Enter value:");
                            //curValue = Console.ReadLine();
                            //param.ParameterType.
                        }
                        //result = methodInfo.Invoke(classInstance, null);
                    }
                }
            }
            else
            {
                Console.WriteLine("Type is null. Class name not found!");
            }
        }
    }
}
