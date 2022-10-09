using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Sample_3_FabricMethod
{
    internal abstract class Product
    {
        public abstract void PostConstruction();
    }
}
