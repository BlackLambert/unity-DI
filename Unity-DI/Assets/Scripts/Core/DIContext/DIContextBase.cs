using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public abstract class DIContextBase : DIContext, Injectable, BindingStorage
    {
        private DIContainer _container;
        private DIInstanceFactory _instanceFactory;
        private BindingValidator _bindingValidator;
        private GameObjectInjector _gameObjectInjector;

        private HashSet<Binding> _nonLazyBindings = new HashSet<Binding>();

        void Injectable.Inject(Resolver resolver)
        {
            DoInject(resolver);
        }

        protected virtual void DoInject(Resolver resolver)
        {
            _container = resolver.Resolve<DIContainer>();
            _instanceFactory = resolver.Resolve<DIInstanceFactory>();
            _bindingValidator = resolver.Resolve<BindingValidator>();
            _gameObjectInjector = resolver.Resolve<GameObjectInjector>();
        }

        public BindingContext<TContract> Bind<TContract>(IComparable iD = default)
		{
            Binding binding = CreateBinding<TContract>();
            Store<TContract>(binding, iD);
            return new BindingContext<TContract>(new BindingArguments(binding, this));
		}

        public ToBindingContext<TContract> BindToSelf<TContract>(IComparable iD = default)
        {
            return Bind<TContract>(iD).To<TContract>();
        }

        public FromNewBindingContext<TContract> BindToNewSelf<TContract>(IComparable iD = default) where TContract : new()
        {
            return Bind<TContract>(iD).ToNew<TContract>();
        }

        public AsBindingContext BindInstance<TContract>(TContract instance, IComparable iD = null)
        {
            return BindToSelf<TContract>(iD).FromInstanceAsSingle(instance);
        }

        public NonResolvableBindingContext CreateNonResolvableInstance()
        {
            Binding binding = CreateBinding<object>();
            return new NonResolvableBindingContext(new BindingArguments(binding, this));
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

        public void Store<TContract>(Binding binding, IComparable iD = null)
        {
            BindingKey key = CreateKey<TContract>(iD);
            _container.AddBinding(key, binding);
        }

        public void AddNonLazy(Binding binding)
        {
            _nonLazyBindings.Add(binding);
        }

        public void CreateNonLazyInstances()
        {
            foreach(Binding binding in new List<Binding>(_nonLazyBindings))
                CreateNonLazyInstance(binding);
        }

		private void CreateNonLazyInstance(Binding binding)
		{
            if (!_nonLazyBindings.Contains(binding))
                return;
            if(binding.IsUnityComponent)
                CreateInstance<Component>(binding);
            else
                CreateInstance<object>(binding);
		}

        protected bool HasBinding(BindingKey key)
		{
            return _container.HasBinding(key);
		}

		protected virtual TContract Resolve<TContract>(BindingKey key)
		{
            return ResolveFromContainer<TContract>(key);
        }

        private Binding CreateBinding<TContract>()
		{
            Type contractType = typeof(TContract);
            return new Binding(contractType);
        }

        protected TContract ResolveFromContainer<TContract>(BindingKey key)
        {
            Binding binding = _container.GetBinding(key);
            return GetInstance<TContract>(binding);
        }

		private TContract GetInstance<TContract>(Binding binding)
        {
            RemoveFromNonLazy(binding);
            return binding.AmountMode switch
            {
                InstanceAmountMode.Single => ResolveSingleInstance<TContract>(binding),
                InstanceAmountMode.PerRequest => CreateInstance<TContract>(binding),
                InstanceAmountMode.Undefined => throw new ArgumentException(),
                _ => throw new NotImplementedException()
            };
        }

		private void RemoveFromNonLazy(Binding binding)
		{
            if (_nonLazyBindings.Contains(binding))
                _nonLazyBindings.Remove(binding);
        }

		private TContract ResolveSingleInstance<TContract>(Binding binding)
        {
            if (_container.HasSingleInstanceOf(binding))
                return _container.GetSingleInstance<TContract>(binding);
            return CreateSingleInstance<TContract>(binding);
        }

        private TContract CreateSingleInstance<TContract>(Binding binding)
        {
            TContract instance = CreateInstance<TContract>(binding);
            _container.StoreSingleInstance(binding, instance);
            return instance;
        }

        private TContract CreateInstance<TContract>(Binding binding)
        {
            TContract instance = _instanceFactory.Create<TContract>(this, binding);
            TryInjection(instance, binding);
            return instance;
        }

        private void TryInjection<TContract>(TContract instance, Binding binding)
        {
            if (!binding.InjectionAllowed)
                return;
            if (instance is Component)
                InjectIntoComponent(instance as Component, binding);
            else if (instance is GameObject)
                InjectIntoGameObject(instance as GameObject, binding);
            else
                InjectIntoInstance(instance, binding);
        }

        private void InjectIntoGameObject(GameObject gameObject, Binding binding)
        {
            _gameObjectInjector.InjectIntoContextHierarchy(gameObject.transform, CreateResolver(binding));
        }

        private void InjectIntoComponent(Component component, Binding binding)
		{
            _gameObjectInjector.InjectIntoContextHierarchy(component.transform, CreateResolver(binding));
        }

        private void InjectIntoInstance<TContract>(TContract instance, Binding binding)
		{
            Injectable injectable = instance as Injectable;
            if (injectable == null)
                return;
            ArgumentsResolver argumentsResolver = new ArgumentsResolver(this);
            argumentsResolver.AddArguments(binding.Arguments);
            injectable.Inject(CreateResolver(binding));
        }

        private ArgumentsResolver CreateResolver(Binding binding)
		{
            ArgumentsResolver result = new ArgumentsResolver(this);
            result.AddArguments(binding.Arguments);
            return result;
        }

        private BindingKey CreateKey<TContract>(IComparable iD = default)
		{
            return new BindingKey(typeof(TContract), iD);
		}
	}
}

