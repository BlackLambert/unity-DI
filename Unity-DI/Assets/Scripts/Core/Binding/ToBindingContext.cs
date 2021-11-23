using System;

namespace SBaier.DI
{
    public class ToBindingContext<TContract, TConcrete> where TConcrete : TContract
    {
        private Binding _binding;

        public ToBindingContext(Binding binding)
        {
            _binding = binding;
            _binding.ConcreteType = typeof(TConcrete);
        }
        
        public AsBindingContext FromInstanceAsSingle(TConcrete instance)
        {
            _binding.CreationMode = InstanceCreationMode.FromInstance;
            _binding.CreateInstanceFunction = () => instance;
            _binding.InjectionAllowed = false;
            _binding.AmountMode = InstanceAmountMode.Single;
            return new AsBindingContext(_binding);
        }

        public FromBindingContext FromMethod(Func<TConcrete> create)
        {
            _binding.CreationMode = InstanceCreationMode.FromMethod;
            _binding.CreateInstanceFunction = () => create();
            return new FromBindingContext(_binding);
        }

        public FromBindingContext FromFactory<TFactory>() where TFactory: Factory<TConcrete>
        {
            _binding.CreationMode = InstanceCreationMode.FromFactory;
            _binding.InstanceFactoryType = typeof(TFactory);
            return new FromBindingContext(_binding);
        }
    }
}
