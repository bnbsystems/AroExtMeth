
using System.Collections.Generic;
using System.Linq;

namespace AroLibraries.ExtensionMethods.Enumerable
{
    public static class IListExt
    {

        public static List<List<T>> TakeInner<T>(this List<List<T>> iEnumerable, int numbers)
        {
            int counter = iEnumerable.Count();
            int maxObjectsInList = iEnumerable.Select(x => x.Count).Max();
            List<List<T>> rList = new List<List<T>>(counter);
            for (int i = 0; i < counter; i++)
            {
               rList.Add( new List<T>());
            }

            int addedCounter = 0;
            bool enoughObjects = false;
            for (int i = 0; enoughObjects == false && i < maxObjectsInList; i++)
            {
                if (iEnumerable != null)
                {
                    int bigListCounter = 0;
                    foreach (var innerEnumerableIter in iEnumerable)
                    {
                        if (i < innerEnumerableIter.Count)
                        {
                            var vobject = innerEnumerableIter.ElementAtOrDefault(i);
                            if (vobject != null)
                            {
                                rList[bigListCounter].Add(vobject);
                                
                                addedCounter++;
                                if (addedCounter == numbers)
                                {
                                    enoughObjects = true;
                                    break;
                                }
                            }
                        }
                        bigListCounter++;
                    }
                }
            }

            return rList;
        }

        public static List<List<T>> TakeInner<T>(this List<List<T>> iEnumerable, int numbers, int groupNumber)
        {
            int counter = iEnumerable.Count();
            int maxObjectsInList = iEnumerable.Select(x => x.Count).Max();
            List<List<T>> rList = new List<List<T>>(counter);
            for (int i = 0; i < counter; i++)
            {
                rList.Add(new List<T>());
            }

            int addedCounter = 0;
            int group = 0;
            int innerCounter = 0;
            bool enoughObjects = false;
            for (int i = 0; enoughObjects == false && i < maxObjectsInList; i++)
            {
                if (iEnumerable != null)
                {
                    int bigListCounter = 0;
                    foreach (var innerEnumerableIter in iEnumerable)
                    {
                        if (i < innerEnumerableIter.Count)
                        {
                            if (group == groupNumber)
                            {
                                var vobject = innerEnumerableIter.ElementAtOrDefault(i);
                                if (vobject != null)
                                {
                                    rList[bigListCounter].Add(vobject);

                                    addedCounter++;
                                    if (addedCounter == numbers)
                                    {
                                        enoughObjects = true;
                                        break;
                                    }
                                }
                            }

                            innerCounter++;
                            if (innerCounter == numbers)
                            {
                                innerCounter = 0;
                                group++;
                            }
                            if (group > groupNumber)
                            {
                                enoughObjects = true;
                                break; 
                            }
                        }
                        bigListCounter++;
                    }
                }
            }

            return rList;
        }


        public static IList<T> ReverseList<T>(this IList<T> iList)
        {
            List<T> l = new List<T>(iList);
            l.Reverse();
            return l;
        }
        //public static T[,] ConvertToTwoDimensionalArray<T>(this IList<List<T>> iList)
        //{
        //    int vFirstDimensionalSize = iList.Count;
        //    int vSecondDimensionalSize = iList.First().Count;

        //    T[,] rTwoDimensionalArray= new T[vFirstDimensionalSize,vSecondDimensionalSize];

        //    int vCounter = 0;
        //    foreach (var item in iList)
        //    {
        //        int vInnerCounter = 0;
        //        foreach (var itemValue in item)
        //        {
        //            rTwoDimensionalArray[vCounter, vInnerCounter] = itemValue;
        //            vInnerCounter++;
        //        }
        //        vCounter++;
        //    }
        //    return rTwoDimensionalArray;
        //}


        public static IList<T> Swap<T>(this List<T> iList, int firstIndex, int secondIndex)
        {
            var list = new List<T>(iList);
            if (firstIndex > -1 && secondIndex > -1
                    && iList.Count > firstIndex && iList.Count > secondIndex)
            {

                var buffer = list[firstIndex];
                list[firstIndex] = list[secondIndex];
                list[secondIndex] = buffer;
            }
            return list;
        }
        public static IList<T> SwapFirstAndLastElement<T>(this List<T> iList)
        {
            return Swap(iList, 0, iList.Count);
        }
    }
}
