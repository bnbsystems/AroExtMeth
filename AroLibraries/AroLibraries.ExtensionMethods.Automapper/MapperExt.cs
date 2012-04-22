using System;
using AutoMapper;

namespace AroLibraries.ExtensionMethods.Automapper
{
    public static class MapperExt
    {
        public static object Map(this object iObject, Type destinationType)
        {
            return Mapper.Map(iObject, iObject.GetType(), destinationType);
        }

        public static TDestination Map<TDestination>(this object iObject)
        {
            if (iObject == null)
            {
                return default(TDestination);
            }

            return (TDestination)iObject.Map(typeof(TDestination));
        }

        public static TDestination Map<TSource, TDestination>(this TSource iObject)
        {
            return Mapper.Map<TSource, TDestination>(iObject);
        }
    }
}