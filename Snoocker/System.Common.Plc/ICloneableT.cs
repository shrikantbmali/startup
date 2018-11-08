namespace System.Common
{
    public interface ICloneableT<out TClonable>
    {
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        ///
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        TClonable Clone();
    }
}