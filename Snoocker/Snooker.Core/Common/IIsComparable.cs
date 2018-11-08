namespace Snoocker.Core.Common
{
    public interface IIsComparable<T>
    {
        bool Is(T other);
    }
}