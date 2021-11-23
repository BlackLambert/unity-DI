using System;
using System.Collections.Generic;

namespace SBaier.DI
{
    public abstract class DIContextBase : DIContext, Injectable
    {
        private DIContainer _container;
        private DIInstanceFactory _instanceFactory;
        private BindingValidator _bindingValidator;

        void Injectable.Inject(Resolver resolver)
        {
            DoInject(resolver);
        }

        public abstract BindingContext<TContract> Bind<TContract>();

        public ToBindingContext<TContract, TContract> BindToSelf<TContract>()
        {
            return Bind<TContract>().To<TContract>();
        }

        public FromNewBindingContext<TContract> BindToNewSelf<TContract>() where TContract : new()
        {
            return Bind<TContract>().ToNew<TContract>();
        }

        public TContract Resolve<TContract>()
        {
            return Resolve<TContract>((IComparable)default);
        }

        public TContract Resolve<TContract>(IComparable iD)
        {
            return Resolve<TContract>(new BindingKey(typeof(TContract), iD));
        }

        public TContract ResolveOptional<TContract>()
        {
            return ResolveOptional<TContract>(default);
        }

        public TContract ResolveOptional<TContract>(IComparable iD)
        {
            BindingKey key = new BindingKey(typeof(TContract), iD);
            return _container.HasBinding(key) ? Resolve<TContract>(iD) : default;
        }

        public void ValidateBindings()
		{
            IEnumerable<Binding> bindings = _container.GetBindings();
            foreach (Binding binding in bindings)
                _bindingValidator.Validate(binding);
        }

        protected virtual void DoInject(Resolver resolver)
        {
            _container = resolver.Resolve<DIContainer>();
            _instanceFactory = resolver.Resolve<DIInstanceFactory>();
            _bindingValidator = resolver.Resolve<BindingValidator>();
        }

        protected abstract TContract Resolve<TContract>(BindingKey key);

        protected BindingContext<TConcrete> BindToContainer<TConcrete>()
        {
            Type contractType = typeof(TConcrete);
            Binding binding = new Binding(contractType);
            _container.AddBinding(binding);
            return new BindingContext<TConcrete>(binding);
        }

        protected TContract ResolveFromContainer<TContract>(BindingKey key)
        {
            Binding binding = _container.GetBinding(key);
            return GetInstance<TContract>(binding, key);
        }

        private TContract GetInstance<TContract>(Binding binding, BindingKey key)
        {
            return binding.AmountMode switch
            {
                InstanceAmountMode.Single => ResolveSingleInstance<TContract>(binding, key),
                InstanceAmountMode.PerRequest => CreateInstance<TContract>(binding),
                InstanceAmountMode.Undefined => throw new ArgumentException(),
                _ => throw new NotImplementedException()
            };
        }

        private TContract ResolveSingleInstance<TContract>(Binding binding, BindingKey key)
        {
            if (_container.HasSingleInstanceOf<TContract>())
                return _container.GetSingleInstance<TContract>();
            return CreateSingleInstance<TContract>(binding, key);
        }

        private TContract CreateSingleInstance<TContract>(Binding binding, BindingKey key)
        {
            TContract instance = CreateInstance<TContract>(binding);
            _container.StoreSingleInstance(key, instance);
            return instance;
        }

        private TContract CreateInstance<TContract>(Binding binding)
        {
            TContract instance = _instanceFactory.Create<TContract>(this, binding);
            if(binding.InjectionAllowed)
                TryInjection(instance);
            return instance;
        }

        private void TryInjection<TContract>(TContract obj)
        {
            Injectable injectable = obj as Injectable;
            injectable?.Inject(this);
        }
	}
}

