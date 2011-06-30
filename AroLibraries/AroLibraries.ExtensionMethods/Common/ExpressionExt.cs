using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace AroLibraries.ExtensionMethods.Common
{
    public static class ExpressionExt
    {
        public static MemberInfo MemberOf<T>(Expression<Func<T>> e)
        {
            return MemberOf(e.Body);
        }

        public static MemberInfo MemberOf(Expression<Action> e)
        {
            return MemberOf(e.Body);
        }

        private static MemberInfo MemberOf(Expression body)
        {
            {
                var member = body as MemberExpression;
                if (member != null) return member.Member;
            }

            {
                var method = body as MethodCallExpression;
                if (method != null) return method.Method;
            }

            throw new ArgumentException(
                "'" + body + "': not a member access");
        }

        /// <summary>
        ///
        /// </summary>
        /// <example>
        ///     Dictionary<string, string> items = Hash(Name => "alex", Age => "10", Height => "20");
        ///     Assert.AreEqual("alex", items["Name"]);
        ///     Assert.AreEqual("10", items["Age"]);
        ///     Assert.AreEqual("20", items["Height"]);
        /// </example>
        /// <see cref="http://blog.bittercoder.com/PermaLink,guid,d1831805-dbf7-4b74-a6fd-2e9ed437c3d9.aspx"/>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IDictionary<string, T> ToDictionary<T>(params Expression<Func<string, T>>[] args)
            where T : class
        {
            IDictionary<string, T> items = new Dictionary<string, T>();
            foreach (Expression<Func<string, T>> expression in args)
            {
                ConstantExpression constantExpression = expression.Body as ConstantExpression;

                T item = null;
                if (constantExpression != null)
                {
                    item = constantExpression.Value as T;
                }
                else
                {
                    item = Expression.Lambda<Func<T>>(expression.Body).Compile()();
                }
                items.Add(expression.Parameters[0].Name, item);
            }
            return items;
        }
    }
}