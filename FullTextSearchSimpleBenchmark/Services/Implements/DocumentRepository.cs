using FullTextSearchSimpleBenchmark.DBContext;
using FullTextSearchSimpleBenchmark.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearchSimpleBenchmark.Services.Implements
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DocumentDbContext _dbContext;

        public DocumentRepository(
            DocumentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void LoadDocuments()
        {
            using (var streamReader = new StreamReader(AppContext.BaseDirectory + @"Data\data.txt"))
            {
                while (!streamReader.EndOfStream)
                {
                    var doc = streamReader.ReadLine().Split('\t');
                    if (doc.Length > 1 && int.TryParse(doc[0], out int id))
                    {
                        _dbContext.Documents.Add(new Document
                        {
                            Id = id,
                            Content = doc[1]
                        });
                        _dbContext.SaveChanges();
                    }
                }
            }
        }
    }
}
