using BenchmarkDotNet.Running;
using FullTextSearchSimpleBenchmark.Benchmarks;
using FullTextSearchSimpleBenchmark.DBContext;
using FullTextSearchSimpleBenchmark.Services.Implements;
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
    public class Sample03
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddDbContext<DocumentDbContext>(options =>
                    {
                        options.UseSqlServer(@"data source=VLASTRO\SQLEXPRESS;initial catalog=DocumentsDatabase;User Id=DocumentsDatabaseUser;Password=qwerty;MultipleActiveResultSets=True;App=EntityFramework");
                    });
                })
                .Build();

            //FullTextIndexV1 fullTextIndexV1 = new FullTextIndexV1(host.Services.GetService<DocumentDbContext>());
            //fullTextIndexV1.BuildIndex();
            BenchmarkSwitcher.FromAssembly(typeof(Sample03).Assembly).Run(args, new BenchmarkDotNet.Configs.DebugInProcessConfig());
            BenchmarkRunner.Run<SearchBenchmarkV2>();
        }
    }
}
