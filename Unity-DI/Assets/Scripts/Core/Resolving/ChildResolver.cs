using UnityEngine;

namespace SBaier.DI
{
    public class ChildResolver : DIContainerResolver
	{
		private readonly Resolver _parent;

		public ChildResolver(Resolver parent, DIContainer dIContainer, DIContext context) : base(dIContainer, context)
		{
			_parent = parent;
		}

		public override bool IsResolvable(BindingKey key)
		{
			return base.IsResolvable(key) || _parent.IsResolvable(key);
		}

		protected override TContract DoResolve<TContract>(BindingKey key)
		{
			if (base.IsResolvable(key))
				return base.DoResolve<TContract>(key);
			return _parent.Resolve<TContract>(key);
		}
	}
}
