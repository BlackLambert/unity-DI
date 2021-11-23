namespace SBaier.DI
{
    public interface Binder
    {
        public BindingContext<TContract> Bind<TContract>();
        public ToBindingContext<TContract, TContract> BindToSelf<TContract>();
        public FromNewBindingContext<TContract> BindToNewSelf<TContract>() where TContract : new();
    }
}