namespace SBaier.DI
{
    public class ChildDIContext : DIContextBase
    {
        private DIContext _baseContext;

		protected override void DoInject(Resolver resolver)
		{
			base.DoInject(resolver);
            _baseContext = resolver.Resolve<DIContext>();
        }

        protected override TContract Resolve<TContract>(BindingKey key)
        {
            try
            {
                return base.Resolve<TContract>(key);
            }
            catch(MissingBindingException)
            {
                return _baseContext.Resolve<TContract>(key.ID);
            }
        }
    }
}