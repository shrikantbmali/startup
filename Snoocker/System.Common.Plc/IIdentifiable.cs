namespace System.Common
{
    public interface IIdentifiable<out TIdType>
    {
        TIdType Id { get; }
    }
}