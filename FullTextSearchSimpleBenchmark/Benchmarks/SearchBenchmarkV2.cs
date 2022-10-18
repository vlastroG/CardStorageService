using BenchmarkDotNet.Attributes;
using FullTextSearchSimpleBenchmark.Services.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullTextSearchSimpleBenchmark.Benchmarks
{
    [MemoryDiagnoser]
    [WarmupCount(1)]
    [IterationCount(5)]
    public class SearchBenchmarkV2
    {

        private readonly FullTextIndexV3 _index;
        private readonly string[] _documentsSet;

        [Params("intercontinental", "monday", "not")]
        public string Query { get; set; }

        public SearchBenchmarkV2()
        {
            _documentsSet = DocumentExtractor.DocumentsSet().Take(10000).ToArray();
            _index = new FullTextIndexV3();
            foreach (var item in _documentsSet)
                _index.AddStringToIndex(item);
        }

        [Benchmark(Baseline = true)]
        public void SimpleSearch()
        {
            new SimpleSearcherV2().SearchV3(Query, _documentsSet).ToArray();
        }

        [Benchmark]
        public void FullTextIndexSearch()
        {
            _index.SearchTest(Query).ToArray();
        }
    }
}
