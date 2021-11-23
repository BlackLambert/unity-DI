using System;
using System.Collections.Generic;

namespace SBaier.DI
{
    public abstract class BasicInstanceResolver : Resolver
    {
        protected abstract Dictionary<BindingKey, object> Instances { get; }

        public TContract Resolve<TContract>()
        {
            return Resolve<TContract>(default);
        }

        public TContract Resolve<TContract>(IComparable iD)
        {
            return (TContract) Instances[new BindingKey(typeof(TContract), iD)];
        }

		public TContract ResolveOptional<TContract>()
		{
            return ResolveOptional<TContract>(default);
        }

		public TContract ResolveOptional<TContract>(IComparable iD)
		{
            BindingKey key = new BindingKey(typeof(TContract), iD);
            return Instances.ContainsKey(key) ? (TContract)Instances[key] : default;
        }
	}
}

