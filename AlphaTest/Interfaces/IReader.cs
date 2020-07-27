using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AlphaTest.Interfaces
{
    interface IReader
    {
        string _format { get; }
        DataTable ReadToTable(string path);
    }
}
