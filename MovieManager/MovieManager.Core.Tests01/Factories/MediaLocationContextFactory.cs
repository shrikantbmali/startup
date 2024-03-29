using MovieManager.ContextModel;
// <copyright file="MediaLocationContextFactory.cs">Copyright ©  2015</copyright>

using System;
using Microsoft.Pex.Framework;

namespace MovieManager.ContextModel
{
    /// <summary>A factory for MovieManager.ContextModel.MediaLocationContext instances</summary>
    public static partial class MediaLocationContextFactory
    {
        /// <summary>A factory for MovieManager.ContextModel.MediaLocationContext instances</summary>
        [PexFactoryMethod(typeof(Context<,>), "MovieManager.ContextModel.MediaLocationContext")]
        public static object Create()
        {
            throw new NotImplementedException();

            // TODO: Edit factory method of MediaLocationContext
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
