using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AroLibraries.ExtensionMethods.Enumerable
{
    public static class ArrayExt
    {
        public static Size GetSize(this Array iArray)
        {
            Size vSize = new Size();
            int length = iArray.Length;
            int rank = iArray.Rank;
            int[] index = null;
            if (rank == 1)
            {
                vSize.Height = 0;
                vSize.Width = length;
            }
            else if (rank == 2)
            {
                index = new int[2];
                for (int i = 0; i <= length; i++)
                {
                    index[0] = i;
                    try
                    {
                        iArray.GetValue(index);
                    }
                    catch (Exception)
                    {
                        vSize.Width = i;
                        break;
                    }
                }
                index[0] = 0;
                for (int i = 0; i <= length; i++)
                {
                    index[1] = i;
                    try
                    {
                        iArray.GetValue(index);
                    }
                    catch (Exception)
                    {
                        vSize.Height = i;
                        break;
                    }
                }
            }
            return vSize;
        }

        public static T[] ConvertTo<T>(this Array ar)
        {
            T[] ret = new T[ar.Length];
            System.ComponentModel.TypeConverter tc = System.ComponentModel.TypeDescriptor.GetConverter(typeof(T));
            if (tc.CanConvertFrom(ar.GetValue(0).GetType()))
            {
                for (int i = 0; i < ar.Length; i++)
                {
                    ret[i] = (T)tc.ConvertFrom(ar.GetValue(i));
                }
            }
            else
            {
                tc = System.ComponentModel.TypeDescriptor.GetConverter(ar.GetValue(0).GetType());
                if (tc.CanConvertTo(typeof(T)))
                {
                    for (int i = 0; i < ar.Length; i++)
                    {
                        ret[i] = (T)tc.ConvertTo(ar.GetValue(i), typeof(T));
                    }
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
            return ret;
        }
    }
}