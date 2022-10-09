using Patterns.Sample_1;

namespace Patterns
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Sample Singleton
            var singletonInstance1 = SampleSingleton.Instance;
            singletonInstance1.Count++;
            var singletonInstance2 = SampleSingleton.Instance;
            singletonInstance2.Count++;
            Console.WriteLine(singletonInstance1.Count);
            bool test = singletonInstance1 == singletonInstance2;

            // Sample AbstractFabric
        }
    }
}