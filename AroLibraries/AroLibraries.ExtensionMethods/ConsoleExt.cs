using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace AroLibraries.ExtensionMethods
{
    public static class ConsoleExt
    {
        public static void WriteLine(IEnumerable iEnumerable)
        {
            foreach (var enumerable in iEnumerable)
            {
                Console.WriteLine(enumerable);
            }
        }
        public static void WriteLine<T, TResult>(IEnumerable<T> iEnumerable, Func<T, TResult> func)
        {
            foreach (var enumerable in iEnumerable)
            {
                Console.WriteLine(func.Invoke(enumerable));
            }
        }


        public static void WriteLine<TKey, TElement>(IEnumerable<IGrouping<TKey, TElement>> iEnumerableGrouping)
        {
            foreach (IGrouping<TKey, TElement> grouping in iEnumerableGrouping)
            {
                Console.WriteLine(grouping.Key);
            }
        }

        public static void WriteLine(DataTable dt)
        {
            if (dt == null) return;
            Console.WriteLine();
            foreach (var column in dt.Columns)
            {
                DataColumn dataColumn = column as DataColumn;

                Console.Write("{0}",dataColumn.ColumnName +"\t");
            }
            Console.WriteLine();
            foreach (var row in dt.Rows)
            {
                DataRow dataRow = row as DataRow;

                foreach (var itemArray in dataRow.ItemArray)
                {
                    Console.Write("{0}",itemArray + "\t");
                }
                Console.WriteLine();
            }

        }


    }
}
