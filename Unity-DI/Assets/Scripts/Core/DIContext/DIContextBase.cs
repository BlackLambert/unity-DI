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

        protected virtual void DoInject(Resolver resolver)
        {
            _container = resolver.Resolve<DIContainer>();
            _instanceFactory = resolver.Resolve<DIInstanceFactory>();
            _bindingValidator = resolver.Resolve<BindingValidator>();
        }

        public BindingContext<TContract> Bind<TContract>(IComparable iD = default)
		{
            return BindToContainer<TContract>(iD);
		}

        public ToBindingContext<TContract, TContract> BindToSelf<TContract>(IComparable iD = default)
        {
            return Bind<TContract>(iD).To<TContract>();
        }

        public FromNewBindingContext<TContract> BindToNewSelf<TContract>(IComparable iD = default) where TContract : new()
        {
            return Bind<TContract>(iD).ToNew<TContract>();
        }

        public TContract Resolve<TContract>()
        {
            return Resolve<TContract>((IComparable)default);
        }

        public TContract Resolve<TContract>(IComparable iD)
        {
            BindingKey key = CreateKey<TContract>(iD);
            return Resolve<TContract>(key);
        }

        public TContract ResolveOptional<TContract>()
        {
            return ResolveOptional<TContract>(default);
        }

        public TContract ResolveOptional<TContract>(IComparable iD)
        {
            BindingKey key = CreateKey<TContract>(iD);
            return _container.HasBinding(key) ? Resolve<TContract>(iD) : default;
        }

        public void ValidateBindings()
		{
            IEnumerable<Binding> bindings = _container.GetBindings();
            foreach (Binding binding in bindings)
                _bindingValidator.Validate(binding);
        }

        protected virtual TContract Resolve<TContract>(BindingKey key)
		{
            return ResolveFromContainer<TContract>(key);
        }

        protected BindingContext<TContract> BindToContainer<TContract>(IComparable iD)
        {
            Type contractType = typeof(TContract);
            Binding binding = new Binding(contractType);
            BindingKey key = CreateKey<TContract>(iD);
            _container.AddBinding(key, binding);
            return new BindingContext<TContract>(binding);
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
            TryInjection(instance, binding);
            return instance;
        }

        private void TryInjection<TContract>(TContract obj, Binding binding)
        {
            Injectable injectable = obj as Injectable;
            if (injectable == null || !binding.InjectionAllowed)
                return;
            ArgumentsResolver argumentsResolver = new ArgumentsResolver(this);
            argumentsResolver.AddArguments(binding.Arguments);
            injectable.Inject(argumentsResolver);
        }

        private BindingKey CreateKey<TContract>(IComparable iD = default)
		{
            return new BindingKey(typeof(TContract), iD);
		}
	}
}

