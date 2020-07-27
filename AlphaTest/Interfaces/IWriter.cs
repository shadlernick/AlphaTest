using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AlphaTest.Interfaces
{
    interface IWriter
    {
        void Write(DataTable data);
    }
}
