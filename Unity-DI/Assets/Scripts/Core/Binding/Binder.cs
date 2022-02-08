using System;

namespace SBaier.DI
{
    public interface Binder
    {
        public BindingContext<TContract> Bind<TContract>(IComparable iD = default);
        public AsBindingContext BindInstance<TContract>(TContract instance, IComparable iD = null);
        public ToBindingContext<TContract> BindToSelf<TContract>(IComparable iD = default);
        public FromNewBindingContext<TContract> BindToNewSelf<TContract>(IComparable iD = default) where TContract : new();
    }
}