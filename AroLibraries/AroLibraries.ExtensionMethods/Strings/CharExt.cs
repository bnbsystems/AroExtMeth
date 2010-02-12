using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AroLibraries.ExtensionMethods.Strings
{
    public static class CharExt
    {
        private static readonly char[] _Vowel=new char[]{'a','e','u','i','o'};
        public static bool Ext_IsVowel(this Char iChar)
        {
            if (_Vowel.Contains(Char.ToLower(iChar)))
            {
                return true;
            }
            return false;
        }
    }
}
