using AlphaTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace AlphaTest
{
    public class CSVReader : IReader
    {
        public string _format { get { return ".csv"; }}

        public DataTable ReadToTable(string path)
        {
            try
            {
                DataTable result = new DataTable();
                string[] columns = null;

                var lines = File.ReadAllLines(path);

                // assuming the first row contains the columns information
                if (lines.Length > 0)
                {
                    columns = lines[0].Split(new char[] { ',' });

                    foreach (var column in columns)
                        result.Columns.Add(column);
                }

                // reading rest of the data
                for (int i = 1; i < lines.Length; i++)
                {
                    DataRow dr = result.NewRow();
                    string[] values = lines[i].Split(new char[] { ',' });

                    for (int j = 0; j < values.Length && j < columns.Length; j++)
                        dr[j] = values[j];

                    result.Rows.Add(dr);
                }

                return result;
            }
            catch (IOException ex)
            {
                Console.WriteLine("File {0} is already in use, close and try again", path);
                Environment.Exit(-1);
                return new DataTable();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error! Contact your administrator");
                Environment.Exit(-1);
                return new DataTable();
            }
        }
    }
}
