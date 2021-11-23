namespace SBaier.DI
{
    public interface DIContext: Binder, Resolver
    {
        void ValidateBindings();
    }
}