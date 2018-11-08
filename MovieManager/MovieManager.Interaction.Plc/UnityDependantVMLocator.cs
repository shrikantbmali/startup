using Microsoft.Practices.Unity;
using System.Mvvm;

namespace MovieManager.Interaction
{
    internal class UnityDependantVMLocator : IVMLocator
    {
        private readonly IUnityContainer _container;

        public UnityDependantVMLocator(IUnityContainer container)
        {
            _container = container;
        }

        public TViewModel GetViewModel<TViewModel>()
        {
            return _container.Resolve<TViewModel>();
        }

        public IVMLocator RegisterType<TKey, TValue>() where TValue : TKey
        {
            _container.RegisterType<TKey, TValue>();

            return this;
        }

        public IVMLocator RegisterInstance<TKey>(TKey instance)
        {
            _container.RegisterInstance(instance);

            return this;
        }
    }
}