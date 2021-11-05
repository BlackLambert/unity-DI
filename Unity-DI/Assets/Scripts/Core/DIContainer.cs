using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class DIContainer 
    {
        private Dictionary<Type, Binding> _bindings = new Dictionary<Type, Binding>();
        private Dictionary<Type, object> _instances = new Dictionary<Type, object>();

        public BindingContext<TConcrete> Bind<TConcrete>()
        {
            Type contractType = typeof(TConcrete);
            ValidateNotBound(contractType);
            Binding binding = new Binding(contractType);
            _bindings.Add(contractType, binding);
            return new BindingContext<TConcrete>(binding);
        }

        public TContract Resolve<TContract>()
        {
            Type contractType = typeof(TContract);
            if (_instances.ContainsKey(contractType))
                return (TContract)_instances[contractType];
            TContract result = CreateInstance<TContract>();
            return result;
        }

        private TContract CreateInstance<TContract>()
        {
            Type contractType = typeof(TContract);
            Binding binding = GetBinding(contractType);
            return CreateInstance<TContract>(binding);
        }

        private TContract CreateInstance<TContract>(Binding binding)
        {
            Type contractType = typeof(TContract);
            TContract instance = (TContract)binding.CreateInstance();
            StoreInstance(binding, contractType, instance);
            return instance;
        }

        private void StoreInstance<TContract>(Binding binding, Type contractType, TContract instance)
        {
            switch (binding.AmountMode)
            {
                case InstanceAmountMode.PerRequest:
                    break;
                case InstanceAmountMode.Single:
                    _instances.Add(contractType, instance);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private Binding GetBinding(Type contractType)
        {
            ValidateBindingExists(contractType);
            return _bindings[contractType];
        }

        private void ValidateBindingExists(Type contractType)
        {
            if (!_bindings.ContainsKey(contractType))
                throw new MissingBindingException();
        }

        private void ValidateNotBound(Type contractType)
        {
            if (_bindings.ContainsKey(contractType))
                throw new AlreadyBoundException();
        }
    }
}