using System;
using System.Linq;

namespace AroLibraries.ExtensionMethods.Strings
{
    public static class CharExt
    {
        private static readonly char[] _Vowel = new char[] { 'a', 'e', 'u', 'i', 'o' };

        public static bool IsVowel(this Char iChar)
        {
            if (_Vowel.Contains(Char.ToLower(iChar)))
            {
                return true;
            }
            return false;
        }

        public static Char GetEmpty()
        {
            return '\0';
        }
    }
}