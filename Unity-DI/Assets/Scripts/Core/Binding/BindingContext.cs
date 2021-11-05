namespace SBaier.DI
{
    public class BindingContext<TContract>
    {
        private Binding _binding;

        public BindingContext(Binding binding)
        {
            _binding = binding;
        }

        public BindingContext<TContract, TConcete> To<TConcete>() where TConcete : TContract
        {
            return new BindingContext<TContract, TConcete>(_binding);
        }

        public void FromInstance(TContract instance)
        {
            _binding.CreateInstance = () => instance;
        }
    }

    public class BindingContext<TContract, TConcrete> where TConcrete : TContract
    {
        private Binding _binding;

        public BindingContext(Binding binding)
        {
            _binding = binding;
            _binding.ConcreteType = typeof(TConcrete);
        }
    }
}