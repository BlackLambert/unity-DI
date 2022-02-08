using System;

namespace SBaier.DI
{
    public class BindingContext<TContract>
    {
        private readonly Binding _binding;
        private readonly DIContainer _container;

        public BindingContext(Binding binding,
            DIContainer container)
        {
            _binding = binding;
            _container = container;
        }
        
        public ToBindingContext<TConcrete> To<TConcrete>() where TConcrete : TContract
        {
            return new ToBindingContext<TConcrete>(_binding);
        }

        public FromNewBindingContext<TConcrete> ToNew<TConcrete>() where TConcrete : TContract, new()
        {
            return new FromNewBindingContext<TConcrete>(_binding);
        }

        public BindingContext<TContract, TContract2> And<TContract2>(IComparable iD = default)
		{
            Type contractType = typeof(TContract2);
            BindingKey key = new BindingKey(contractType, iD);
            _container.AddBinding(key, _binding);
            return new BindingContext<TContract, TContract2>(_binding, _container);
        }
    }

    public class BindingContext<TContract1, TContract2>
	{
        private readonly Binding _binding;
        private readonly DIContainer _container;


        public BindingContext(Binding binding,
            DIContainer container)
        {
            _binding = binding;
            _container = container;
        }

        public ToBindingContext<TConcrete> To<TConcrete>() where TConcrete : TContract1, TContract2
        {
            return new ToBindingContext<TConcrete>(_binding);
        }

        public FromNewBindingContext<TConcrete> ToNew<TConcrete>() where TConcrete : TContract1, TContract2, new()
        {
            return new FromNewBindingContext<TConcrete>(_binding);
        }

        public BindingContext<TContract1, TContract2, TContract3> And<TContract3>(IComparable iD = default)
        {
            Type contractType = typeof(TContract3);
            BindingKey key = new BindingKey(contractType, iD);
            _container.AddBinding(key, _binding);
            return new BindingContext<TContract1, TContract2, TContract3>(_binding);
        }
    }

    public class BindingContext<TContract1, TContract2, TContract3>
	{
        private readonly Binding _binding;


        public BindingContext(Binding binding)
        {
            _binding = binding;
        }

        public ToBindingContext<TConcrete> To<TConcrete>() where TConcrete : TContract1, TContract2, TContract3
        {
            return new ToBindingContext<TConcrete>(_binding);
        }

        public FromNewBindingContext<TConcrete> ToNew<TConcrete>() where TConcrete : TContract1, TContract2, TContract3, new()
        {
            return new FromNewBindingContext<TConcrete>(_binding);
        }
    }
}