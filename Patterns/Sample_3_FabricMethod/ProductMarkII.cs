using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Sample_3_FabricMethod
{
    internal class ProductMarkII : Product
    {
        public ProductMarkII() { }

        public ProductMarkII(int t) { }

        public override void PostConstruction()
        {
            Console.WriteLine($"{this} created");
        }
    }
}
