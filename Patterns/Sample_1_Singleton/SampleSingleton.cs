using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Sample_1_Singleton
{
    internal class SampleSingleton
    {
        private static SampleSingleton _instance;

        private SampleSingleton() { }

        public int Count { get; set; } = 1;

        public static SampleSingleton Instance
        {
            get
            {
                return _instance ??= new SampleSingleton();
            }
        }
    }
}
