

namespace SBaier.DI
{
    public class BasicDIContext : DIContext
    {
        private DIContainer _container;

        public BasicDIContext(DIContainer container)
        {
            _container = container;
        }

        public BindingContext<TContract> Bind<TContract>()
        {
            return _container.Bind<TContract>();
        }

        public TContract Resolve<TContract>()
        {
            return _container.Resolve<TContract>();
        }
    }
}