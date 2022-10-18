using FullTextSearchSimpleBenchmark.DBContext;
using FullTextSearchSimpleBenchmark.Services;
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
    internal class Sample01
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

                    services.AddTransient<IDocumentRepository, DocumentRepository>();
                })
                .Build();

            //host.Services.GetRequiredService<IDocumentRepository>().LoadDocuments();
            var documentsSet = DocumentExtractor.DocumentsSet().Take(10000).ToArray();
            new SimpleSearcher().Search("Monday", documentsSet);
        }
    }
}
