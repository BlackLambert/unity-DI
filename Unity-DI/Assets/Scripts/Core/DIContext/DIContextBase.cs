using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public abstract class DIContextBase : DIContext, Injectable
    {
        private DIContainer _container;
        private DIInstanceFactory _instanceFactory;
        private BindingValidator _bindingValidator;
        private GameObjectInjector _gameObjectInjector;

        private Resolver _dIContainerResolver;
        private Binder _dIContainerBinder;

        void Injectable.Inject(Resolver resolver)
        {
            DoInjection(resolver);
            _dIContainerResolver = CreateResolver(_container, this);
            _dIContainerBinder = new DIContainerBinder(_container);
        }

        protected virtual void DoInjection(Resolver resolver)
        {
            _container = resolver.Resolve<DIContainer>();
            _instanceFactory = resolver.Resolve<DIInstanceFactory>();
            _bindingValidator = resolver.Resolve<BindingValidator>();
            _gameObjectInjector = resolver.Resolve<GameObjectInjector>();
        }

        public void ValidateBindings()
		{
            IEnumerable<Binding> bindings = _container.GetBindings();
            foreach (Binding binding in bindings)
                _bindingValidator.Validate(binding);
        }

        public void CreateNonLazyInstances()
        {
            foreach(Binding binding in new List<Binding>(_container.NonLazyBindings))
                CreateNonLazyInstance(binding);
        }

        public Resolver GetResolver()
        {
            return _dIContainerResolver;
        }

        public Binder GetBinder()
        {
            return _dIContainerBinder;
        }

        public TContract GetInstance<TContract>(Binding binding)
        {
            _container.RemoveFromNonLazy(binding);
            return binding.AmountMode switch
            {
                InstanceAmountMode.Single => ResolveSingleInstance<TContract>(binding),
                InstanceAmountMode.PerRequest => CreateInstance<TContract>(binding),
                InstanceAmountMode.Undefined => throw new ArgumentException(),
                _ => throw new NotImplementedException()
            };
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
            TContract instance = _instanceFactory.Create<TContract>(GetResolver(), binding);
            TryInjection(instance, binding);
            return instance;
        }

        private void CreateNonLazyInstance(Binding binding)
		{
            if (!_container.NonLazyBindings.Contains(binding))
                return;
            if(binding.IsUnityComponent)
                CreateInstance<Component>(binding);
            else
                CreateInstance<object>(binding);
            _container.RemoveFromNonLazy(binding);
        }

        protected bool HasBinding(BindingKey key)
		{
            return _container.HasBinding(key);
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
            InjectIntoTransform(gameObject.transform, binding);
        }

        private void InjectIntoComponent(Component component, Binding binding)
		{
            InjectIntoTransform(component.transform, binding);
        }

        private void InjectIntoTransform(Transform transform, Binding binding)
        {
            _gameObjectInjector.InjectIntoContextHierarchy(transform, GetResolverFor(binding));
        }

        private void InjectIntoInstance<TContract>(TContract instance, Binding binding)
		{
            Injectable injectable = instance as Injectable;
            if (injectable == null)
                return;
            injectable.Inject(GetResolverFor(binding));
        }

        private Resolver GetResolverFor(Binding binding)
        {
            ArgumentsResolver result = new ArgumentsResolver(GetResolver());
            result.AddArguments(binding.Arguments);
            return result;
        }

        protected abstract Resolver CreateResolver(DIContainer container, DIContext diContext);
    }
}

