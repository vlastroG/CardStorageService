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
    public class Sample02
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddDbContext<DocumentDbContext>(options =>
                    {
                        options.UseSqlServer(@"data source=DESKTOP-6DH4OOP\SQLEXPRESS;initial catalog=DocumentsDatabase;User Id=DocumentsDatabaseUser;Password=12345;MultipleActiveResultSets=True;App=EntityFramework");
                    });
                })
                .Build();

            var documentsSet = DocumentExtractor.DocumentsSet().Take(10000).ToArray();
            //new SimpleSearcherV2().SearchV1("Monday", documentsSet);
            //new SimpleSearcherV2().SearchV2("Monday", documentsSet);

            BenchmarkSwitcher.FromAssembly(typeof(Sample02).Assembly).Run(args, new BenchmarkDotNet.Configs.DebugInProcessConfig());
            BenchmarkRunner.Run<SearchBenchmarkV1>();
        }
    }
}
