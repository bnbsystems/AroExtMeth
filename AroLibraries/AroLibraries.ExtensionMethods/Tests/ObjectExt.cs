using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AroLibraries.ExtensionMethods.Tests
{
    public static class ObjectExt
    {
        public static T ShouldBe<T>(this T iActual, T iExpected)
        {
            Assert.AreEqual(iExpected, iActual);
            return iActual;
        }
    }
}