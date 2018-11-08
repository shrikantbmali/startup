namespace System.Common.EventArguments
{
    public class AssociatableEventArgs<TAssociatedType> : EventArgs
    {
        public TAssociatedType AssociatedObject { get; private set; }

        public AssociatableEventArgs(TAssociatedType associatedObject)
        {
            AssociatedObject = associatedObject;
        }
    }
}