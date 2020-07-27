using AlphaTest.Interfaces;
using ClosedXML.Excel;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace AlphaTest
{
    class Writer : IWriter
    {
        string _path { get; set; }
        public Writer(string path)
        {
            _path = path;
        }
        public void Write(DataTable data)
        {
            try
            {
                XLWorkbook wb = new XLWorkbook(_path);
                wb.Worksheets.Add(data, "Result");
                wb.Save();
                Console.WriteLine("Success! Result is written to: " + _path);
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine("Check if xlsx doc is not in use and doesn`t contain worksheet with 'Result' name");
                Environment.Exit(-1);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unexpected error! Contact your administrator");
                Environment.Exit(-1);
            }
        }
    }
}
