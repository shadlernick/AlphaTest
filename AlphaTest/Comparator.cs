using AlphaTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace AlphaTest
{

    public class Comparator : IComparator
    {
        public DataTable Compare(DataTable first, DataTable second)
        {
            try
            {
                var a = first.AsEnumerable().GroupBy(
                    t => t.Field<double>("Номер рахунку")).Select(g =>
                    {
                        var row = first.NewRow();

                        row["Номер рахунку"] = g.Key;
                    //row["Вал."] = g.Select(r => r.Field<string>("Вал."));
                    row["Вих.залишок"] = g.Sum(r => r.Field<double>("Вих.залишок"));

                        return row;
                    }).Join(second.AsEnumerable().GroupBy(
                    t => t.Field<string>("Номер рахунку")).Select(g =>
                     {
                         var row = second.NewRow();

                         row["Номер рахунку"] = g.Key;
                     ///row["Вал."] = g.Select(r => r.Field<string>("Вал."));
                     row["Остаток"] = g.Sum(r => Convert.ToDouble(r.Field<string>("Остаток")));

                         return row;
                     }), t => t.Field<double>("Номер рахунку"),
                    p => Convert.ToDouble(p.Field<string>("Номер рахунку")),
                    (t, p) =>
                    {
                        var row = second.NewRow();

                        row["Номер рахунку"] = t["Номер рахунку"];
                        row["Вал."] = "UAH";  //second.AsEnumerable().Where(x => x["Номер рахунку"].Equals(t["Номер рахунку"])).Select(x => x["Вал."]).FirstOrDefault().ToString();
                        row["Остаток"] = t.Field<double>("Вих.залишок") - Convert.ToDouble(p.Field<string>("Остаток"));

                        return row;
                    }).Where(x => Convert.ToDouble(x.Field<string>("Остаток")) != 0).CopyToDataTable();
                return a;
            }
            catch (Exception ex)
            {
                Console.WriteLine("It`s look like programm rule is incorrect for your file. Contact your administrator");
                Environment.Exit(-1);
                return new DataTable();
            }


            //return (from a in first.AsEnumerable()
            //        join c in second.AsEnumerable()
            //        on
            //        a["Номер рахунку"].ToString() equals c["Номер рахунку"].ToString()
            //        where a["Вих.залишок"] != c["Остаток"]
            //        select a).Distinct().CopyToDataTable();
            //var a = first.AsEnumerable()
            //    .Select(r => string.Format("[{0}]", string.Join(",", r.ItemArray)))
            //    .Except(second.AsEnumerable().Select(r => string.Format("[{0}]", string.Join(",", r.ItemArray))))
            //    .CopyToDataTable();      
        }
    }
}
