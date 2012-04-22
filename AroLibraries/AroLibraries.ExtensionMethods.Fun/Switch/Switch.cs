namespace AroLibraries.ExtensionMethods.Fun
{
    public enum SwitchOperation
    {
        Go, Break
    }

    public class Switch<T, TResult>
    {
        public T Object { get; private set; }

        public TResult ReturnedValue { get; internal set; }

        public SwitchOperation Operation = SwitchOperation.Go;

        public Switch(T iObject)
        {
            Object = iObject;
        }
    }
}