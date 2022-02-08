using System;

namespace SBaier.DI
{
    public interface BindingStorage
    {
        public void Store<TContract>(Binding binding, IComparable iD = default);
    }
}
