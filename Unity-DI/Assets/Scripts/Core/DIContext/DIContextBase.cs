using System;
using System.Collections;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;
using UnityEngine.Rendering;

namespace SBaier.DI
{
    public abstract class DIContextBase : DIContext, Injectable
    {
        private DIContainer _container;
        private DIInstanceFactory _instanceFactory;

        public virtual void Inject(Resolver resolver)
        {
            _container = resolver.Resolve<DIContainer>();
            _instanceFactory = resolver.Resolve<DIInstanceFactory>();
        }

        public abstract BindingContext<TContract> Bind<TContract>();

        public TContract Resolve<TContract>()
        {
            return Resolve<TContract>(new BindingKey(typeof(TContract), default));
        }

        public TContract Resolve<TContract>(IComparable iD)
        {
            return Resolve<TContract>(new BindingKey(typeof(TContract), iD));
        }

        protected abstract TContract Resolve<TContract>(BindingKey key);

        public ToBindingContext<TContract, TContract> BindToSelf<TContract>() where TContract : new()
        {
            return Bind<TContract>().To<TContract>();
        }
        
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

