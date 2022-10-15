using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearchSimpleBenchmark.Services.Implements
{
    public class DocumentExtractor : IDocumentExtractor
    {
        public static IEnumerable<string> DocumentsSet()
        {
            return ReadDocuments(AppContext.BaseDirectory + @"Data\data.txt");
        }

        private static IEnumerable<string> ReadDocuments(string fileName)
        {
            using (var streamReader = new StreamReader(fileName))
            {
                while (!streamReader.EndOfStream)
                {
                    var doc = streamReader.ReadLine()?.Split('\t');
                    yield return doc[1];
                }
            }
        }
    }
}
