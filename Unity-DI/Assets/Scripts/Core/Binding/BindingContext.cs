namespace SBaier.DI
{
    public class BindingContext<TContract>
    {
        private readonly Binding _binding;

        public BindingContext(Binding binding)
        {
            _binding = binding;
        }
        
        public ToBindingContext<TContract, TConcrete> To<TConcrete>() where TConcrete : TContract, new()
        {
            return new ToBindingContext<TContract, TConcrete>(_binding);
        }
    }

    
}