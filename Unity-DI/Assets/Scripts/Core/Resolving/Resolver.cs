using System;

namespace SBaier.DI
{
    public interface Resolver
    {
        public TContract Resolve<TContract>();
        public TContract Resolve<TContract>(IComparable iD);
        public TContract ResolveOptional<TContract>();
        public TContract ResolveOptional<TContract>(IComparable iD);
    }
}