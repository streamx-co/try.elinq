using System;
using System.Reflection;

namespace EFCoreIssues {
    class Program {
        private static readonly String RootNamespace = typeof(Program).Namespace;

        public static void Main(string region = null,
            string session = null,
            string package = null,
            string project = null,
            string sourceFile = null) {

            if (sourceFile != null)
                ExecuteSingleExample(region, sourceFile);
            else {
                
                Issue20505.EnsureDatabase();

                var target = typeof(Issue20505);
                ExecuteAllExamples(target);

                Console.WriteLine($"{counter} examples executed");
            }
        }

        static int counter;

        private static void ExecuteAllExamples(Type type) {

            foreach (var method in type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public)) {
                if (method.GetParameters().Length == 0) {
                    ExecuteSingleExample(method, type);

                    counter++;
                }
            }
        }

        private static void ExecuteSingleExample(MethodInfo method, Type type) {
            var queries = Activator.CreateInstance(type);

            method.Invoke(queries, null);
        }

        private static void ExecuteSingleExample(string methodName, string sourceFile) {
            var start = sourceFile.IndexOf(RootNamespace, StringComparison.Ordinal);
            var type = Type.GetType(sourceFile.Substring(start, sourceFile.Length - start - 3).Replace('/', '.'));
            ExecuteSingleExample(type?.GetMethod(methodName), type);
        }
    }
}
