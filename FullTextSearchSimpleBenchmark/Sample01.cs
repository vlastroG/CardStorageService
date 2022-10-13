using FullTextSearchSimpleBenchmark.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearchSimpleBenchmark
{
    internal class Sample01
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(servises =>
                {
                    servises.AddDbContext<DocumentDbContext>(options =>
                    {
                        options.UseSqlServer(@"data source=VLASTRO\SQLEXPRESS;initial catalog=DocumentsDatabase;User Id=DocumentsDatabaseUser;Password=qwerty;MultipleActiveResultSets=True;App=EntityFramework");
                    });
                })
                .Build();
        }
    }
}
