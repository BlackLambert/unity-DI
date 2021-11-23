namespace SBaier.DI
{
    public class BindingContext<TContract>
    {
        private readonly Binding _binding;

        public BindingContext(Binding binding)
        {
            _binding = binding;
        }
        
        public ToBindingContext<TContract, TConcrete> To<TConcrete>() where TConcrete : TContract
        {
            return new ToBindingContext<TContract, TConcrete>(_binding);
        }

        public FromNewBindingContext<TConcrete> ToNew<TConcrete>() where TConcrete : TContract, new()
        {
            return new FromNewBindingContext<TConcrete>(_binding);
        }
    }

    
}