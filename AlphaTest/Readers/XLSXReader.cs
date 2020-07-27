using AlphaTest.Interfaces;
using ExcelDataReader;
using LinqToExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace AlphaTest
{

    class XLSXReader : IReader
    {
        public string _format { get { return ".xlsx"; } }
        public DataTable ReadToTable(string path)
        {
            try
            {
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                DataTable result;
                using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        result = reader.AsDataSet(new ExcelDataSetConfiguration() { ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration() { UseHeaderRow = true } }).Tables[0];
                    }
                }

                return result;
            }
            catch (IOException ex)
            {
                Console.WriteLine("File {0} is already in use, close and try again", path);
                Environment.Exit(-1);
                return new DataTable();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unexpected error! Contact your administrator");
                Environment.Exit(-1);
                return new DataTable();
            }
        }
    }
}