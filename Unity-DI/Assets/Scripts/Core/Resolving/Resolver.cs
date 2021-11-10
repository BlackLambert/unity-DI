using System;

namespace SBaier.DI
{
    public interface Resolver
    {
        public TContract Resolve<TContract>();
        public TContract Resolve<TContract>(IComparable iD);
    }
}