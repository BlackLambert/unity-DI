namespace SBaier.DI
{
    public interface Binder
    {
        public BindingContext<TContract> Bind<TContract>();
        public ToBindingContext<TContract, TContract> BindToSelf<TContract>() where TContract : new();
    }
}