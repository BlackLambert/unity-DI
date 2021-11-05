namespace SBaier.DI
{
    public interface Binder
    {
        public BindingContext<TContract> Bind<TContract>();
    }
}