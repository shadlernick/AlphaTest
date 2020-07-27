using AlphaTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaTest
{
    /// <summary>
    /// This class does something.
    /// </summary>
    class Analyzer
    {
        List<IReader> _readers;
        IComparator _comparator;
        IWriter _writer;
        public Analyzer(List<IReader> readers, IComparator comparator, IWriter writer)
        {
            _readers = readers;
            _comparator = comparator;
            _writer = writer;
        }

        public void Analyze(string path1, string path2)
        {
            _writer.Write(
                _comparator.Compare(
                        _readers.Find(x => path1.EndsWith(x._format)).ReadToTable(path1), _readers.Find(x => path2.EndsWith(x._format)).ReadToTable(path2)));
        }
    }
}
