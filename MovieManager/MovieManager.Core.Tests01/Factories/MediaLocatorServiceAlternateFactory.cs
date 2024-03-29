using MovieManager.Core;
// <copyright file="MediaLocatorServiceAlternateFactory.cs">Copyright ©  2015</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Practices.Unity;

namespace MovieManager.Core
{
    /// <summary>A factory for MovieManager.Core.MediaLocatorServiceAlternate instances</summary>
    public static partial class MediaLocatorServiceAlternateFactory
    {
        /// <summary>A factory for MovieManager.Core.MediaLocatorServiceAlternate instances</summary>
        [PexFactoryMethod(typeof(CoreModule), "MovieManager.Core.MediaLocatorServiceAlternate")]
        public static IMediaLocatorService Create()
        {
            var coreModule = new CoreModule();
            var unityContainer = new UnityContainer();
            coreModule.Intialize(unityContainer);

            return unityContainer.Resolve<IMediaLocatorService>();
            // TODO: Edit factory method of MediaLocatorServiceAlternate
            // This method should be able to configure the object in all possible ways.
            // Add as many parameters as needed,
            // and assign their values to each field by using the API.
        }
    }
}
