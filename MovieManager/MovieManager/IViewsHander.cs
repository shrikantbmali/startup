using System;

namespace MovieManager
{
	internal interface IViewsHander : IDisposable
	{
		void Intialize();

		void Start();
	}
}