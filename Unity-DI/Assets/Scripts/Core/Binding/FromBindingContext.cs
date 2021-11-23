namespace SBaier.DI
{
    public class FromBindingContext
    {
        private Binding _binding;

        public FromBindingContext(Binding binding)
        {
            _binding = binding;
        }

        public AsBindingContext AsSingle()
        {
            _binding.AmountMode = InstanceAmountMode.Single;
            return new AsBindingContext(_binding);
        }

        public AsBindingContext PerRequest()
        {
            _binding.AmountMode = InstanceAmountMode.PerRequest;
            return new AsBindingContext(_binding);
        }
    }
}

