namespace SBaier.DI
{
    public interface Resolver
    {
        public TContract Resolve<TContract>();
    }
}