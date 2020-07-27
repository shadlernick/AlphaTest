using AlphaTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AlphaTest
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
#if DEBUG
            string[] my_args = { @"C:\Users\PROTEINUM\Desktop\Study\my.xlsx", @"C:\Users\PROTEINUM\Desktop\Study\my2.csv" };

            if (my_args.Length != 2)
            {
                Console.WriteLine("It`s possible to have two files!");
                Environment.Exit(-1);
            }

            List<IReader> readers = new List<IReader> { new XLSXReader(), new CSVReader() };

            new Analyzer(readers, new Comparator(), new Writer(my_args[0])).Analyze(my_args[0], my_args[1]);
#else

            if (args.Length != 2)
            {
                Console.WriteLine("It`s possible to have two files!");
                Environment.Exit(-1);
            }

            List<IReader> readers = new List<IReader> { new XLSXReader(), new CSVReader() };

            new Analyzer(readers, new Comparator(), new Writer(args[0])).Analyze(args[0], args[1]);
#endif
        }

    }
}
