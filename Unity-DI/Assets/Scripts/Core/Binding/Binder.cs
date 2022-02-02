using System;

namespace SBaier.DI
{
    public interface Binder
    {
        public BindingContext<TContract> Bind<TContract>(IComparable iD = default);
        public ToBindingContext<TContract, TContract> BindToSelf<TContract>(IComparable iD = default);
        public FromNewBindingContext<TContract> BindToNewSelf<TContract>(IComparable iD = default) where TContract : new();
    }
}