using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AlphaTest.Interfaces
{
    interface IComparator
    {
        DataTable Compare(DataTable first, DataTable second);
    }
}
