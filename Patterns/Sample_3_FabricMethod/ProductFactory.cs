using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Sample_3_FabricMethod
{
    internal static class ProductFactory
    {
        // Good fabric
        public static T Create<T>() where T : Product, new()
        {
            var t = new T();
            t.PostConstruction();
            return t;
        }

        // Bad fabric
        public static Product Create(Type t)
        {
            var assembly = Assembly.GetAssembly(typeof(Sample_3_FabricMethod.Product));
            var product = (Product)assembly.CreateInstance(t.FullName);
            product.PostConstruction();
            return product;
        }
    }
}
