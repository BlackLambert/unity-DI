namespace SBaier.DI
{
    public class ChildDIContext : DIContextBase
    {
        private DIContext _baseContext;

		protected override void DoInjection(Resolver resolver)
		{
			base.DoInjection(resolver);
			_baseContext = resolver.Resolve<DIContext>();
		}

		protected override Resolver CreateResolver(DIContainer container, DIContext diContext)
		{
			Resolver containerResolver = new ChildResolver(_baseContext.GetResolver(), container, diContext);
			return new CircularDependencyDetector(containerResolver);
		}
	}
}