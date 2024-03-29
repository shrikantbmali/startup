using MovieManager.ContextModel;
using MovieManager.Core;
// <copyright file="MediaCollectionFactory.cs">Copyright ©  2015</copyright>

using System;
using Microsoft.Pex.Framework;

namespace MovieManager.Core
{
    /// <summary>A factory for MovieManager.Core.MediaCollection instances</summary>
    public static partial class MediaCollectionFactory
    {
        /// <summary>A factory for MovieManager.Core.MediaCollection instances</summary>
        [PexFactoryMethod(typeof(MediaCollection))]
        public static MediaCollection Create(
            IMediaLocatorService mediaLocatorService_iMediaLocatorService,
            IMediaLocationContext mediaLocationContext_iMediaLocationContext,
            IMediaItemContext mediaItemContext_iMediaItemContext
        )
        {
            MediaCollection mediaCollection
               = new MediaCollection(mediaLocatorService_iMediaLocatorService,
                                     mediaLocationContext_iMediaLocationContext,
                                     mediaItemContext_iMediaItemContext);
            return mediaCollection;

            // TODO: Edit factory method of MediaCollection
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
