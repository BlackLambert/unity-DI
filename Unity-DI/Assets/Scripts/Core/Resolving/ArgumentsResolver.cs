using System;
using System.Collections.Generic;

namespace SBaier.DI
{
    public class ArgumentsResolver : Resolver
    {
        private Resolver _baseResolver;
        private Dictionary<BindingKey, object> _arguments =
            new Dictionary<BindingKey, object>();

        public ArgumentsResolver(Resolver context)
		{
            _baseResolver = context;
        }

        public void AddArgument<TContract>(TContract argument, IComparable iD = default)
		{
            _arguments.Add(CreateKey<TContract>(iD), argument);
        }

        public void AddArguments(Dictionary<BindingKey, object> arguments)
		{
			foreach (KeyValuePair<BindingKey, object> pair in arguments)
				_arguments.Add(pair.Key, pair.Value);
		}

		public TContract Resolve<TContract>()
		{
			return Resolve<TContract>(default);
		}

		public TContract Resolve<TContract>(IComparable iD)
		{
			BindingKey key = CreateKey<TContract>(iD);
			return _arguments.ContainsKey(key) ? 
				(TContract)_arguments[key] : 
				_baseResolver.Resolve<TContract>();
		}

		public TContract ResolveOptional<TContract>()
		{
			return ResolveOptional<TContract>(default);
		}

		public TContract ResolveOptional<TContract>(IComparable iD)
		{
			BindingKey key = CreateKey<TContract>(iD);
			return _arguments.ContainsKey(key) ?
				(TContract)_arguments[key] :
				_baseResolver.ResolveOptional<TContract>();
		}

		private BindingKey CreateKey<TContract>(IComparable iD)
		{
			return new BindingKey(typeof(TContract), iD);
		}
    }
}
