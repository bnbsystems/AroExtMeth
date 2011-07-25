using System;

namespace AroLibraries.ExtensionMethods.Fun
{
    public static class SwitchExt
    {
        public static Switch<T, TResult> New<T, TResult>(this Switch<T, TResult> iSwitch)
        {
            iSwitch.ReturnedValue = default(TResult);
            iSwitch.Operation = SwitchOperation.Go;
            return iSwitch;
        }

        #region Throw

        public static Switch<T, TResult> Throw<T, TResult, TException>(this Switch<T, TResult> iSwitch, TException iException)
            where TException : Exception
        {
            if (iSwitch.Operation == SwitchOperation.Go)
            {
                throw iException;
            }
            return iSwitch;
        }

        public static Switch<T, TResult> Throw<T, TResult, TException>(this Switch<T, TResult> iSwitch, TException iException, Predicate<T> predicate)
            where TException : Exception
        {
            if (iSwitch.Operation == SwitchOperation.Go && predicate(iSwitch.Object))
            {
                throw iException;
            }
            return iSwitch;
        }

        #endregion Throw

        #region Case

        public static Switch<T, TResult> Case<T, TResult>(this Switch<T, TResult> iSwitch, Predicate<T> predicate, Func<T, TResult> func)
        {
            if (iSwitch.Operation == SwitchOperation.Go && predicate(iSwitch.Object))
            {
                iSwitch.ReturnedValue = func(iSwitch.Object);
                iSwitch.Operation = SwitchOperation.Break;
            }
            return iSwitch;
        }

        public static Switch<T, bool> Case<T, TResult>(this Switch<T, bool> iSwitch, Predicate<T> predicate, Action<T> action)
        {
            if (iSwitch.Operation == SwitchOperation.Go && predicate(iSwitch.Object))
            {
                action(iSwitch.Object);
                iSwitch.ReturnedValue = true;
                iSwitch.Operation = SwitchOperation.Break;
            }
            return iSwitch;
        }

        public static Switch<T, TResult> Case<T, TResult>(this Switch<T, TResult> iSwitch, IEquatable<T> secondValue, Func<T, TResult> func)
        {
            if (iSwitch.Operation == SwitchOperation.Go && secondValue.Equals(secondValue))
            {
                iSwitch.ReturnedValue = func(iSwitch.Object);
                iSwitch.Operation = SwitchOperation.Break;
            }
            return iSwitch;
        }

        public static Switch<T, TResult> Case<T, TResult>(this Switch<T, TResult> iSwitch, IEquatable<T> secondValue, Action<T> action)
            where TResult : IEquatable<T>
        {
            if (iSwitch.Operation == SwitchOperation.Go && secondValue.Equals(secondValue))
            {
                action(iSwitch.Object);
                iSwitch.ReturnedValue = (TResult)secondValue;
                iSwitch.Operation = SwitchOperation.Break;
            }
            return iSwitch;
        }

        #endregion Case

        #region Default

        public static TResult Default<T, TResult>(this Switch<T, TResult> iSwitch, Func<T, TResult> func)
        {
            if (iSwitch.Operation == SwitchOperation.Go)
            {
                iSwitch.ReturnedValue = func(iSwitch.Object);
                iSwitch.Operation = SwitchOperation.Break;
            }
            return iSwitch.ReturnedValue;
        }

        public static TResult Default<T, TResult>(this Switch<T, TResult> iSwitch, Action<T> action)
        {
            if (iSwitch.Operation == SwitchOperation.Go)
            {
                action(iSwitch.Object);
                iSwitch.ReturnedValue = default(TResult);
                iSwitch.Operation = SwitchOperation.Break;
            }
            return iSwitch.ReturnedValue;
        }

        public static TResult Default<T, TResult>(this Switch<T, TResult> iSwitch)
        {
            if (iSwitch.Operation == SwitchOperation.Go)
            {
                return default(TResult);
            }
            return iSwitch.ReturnedValue;
        }

        #endregion Default
    }
}