namespace SBaier.DI
{
	public class DIContainerResolver : ResolverBase
	{
		private DIContainer _container;
		private DIContext _diContext;

		public DIContainerResolver(DIContainer container,
			DIContext diContext)
		{
			_container = container;
			_diContext = diContext;
		}

		public override bool IsResolvable(BindingKey key)
		{
			return _container.HasBinding(key);
		}

		protected override TContract DoResolve<TContract>(BindingKey key)
		{
			Binding binding = _container.GetBinding(key);
			return _diContext.GetInstance<TContract>(binding);
		}
	}
}
