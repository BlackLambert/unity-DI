namespace SBaier.DI
{
    public class ChildResolver : DIContainerResolver
	{
		private readonly Resolver _parent;

		public ChildResolver(Resolver parent, BindingsContainer container, DIContext context) : base(container, context)
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
