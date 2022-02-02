using System;
using System.Collections.Generic;

namespace SBaier.DI
{
    public class BasicInstanceResolver : Resolver
    {
        private Dictionary<BindingKey, object> _instances =
            new Dictionary<BindingKey, object>();

        public void Add<TContract>(TContract instance, IComparable iD = default)
		{
            BindingKey key = CreateKey<TContract>(iD);
            _instances.Add(key, instance);
        }

        public TContract Resolve<TContract>()
        {
            return Resolve<TContract>(default);
        }

        public TContract Resolve<TContract>(IComparable iD)
        {
            BindingKey key = CreateKey<TContract>(iD);
            return (TContract) _instances[key];
        }

		public TContract ResolveOptional<TContract>()
		{
            return ResolveOptional<TContract>(default);
        }

		public TContract ResolveOptional<TContract>(IComparable iD)
		{
            BindingKey key = CreateKey<TContract>(iD);
            return _instances.ContainsKey(key) ? (TContract)_instances[key] : default;
        }

        private BindingKey CreateKey<TContract>(IComparable iD)
		{
            return new BindingKey(typeof(TContract), iD);
        }
	}
}

