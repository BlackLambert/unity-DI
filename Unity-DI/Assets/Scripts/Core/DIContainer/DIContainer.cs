using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class DIContainer 
    {
        private readonly Dictionary<BindingKey, Binding> _bindings = new();
        private readonly Dictionary<BindingKey, object> _singleInstances = new();
        
        public void AddBinding(Binding binding)
        {
            BindingKey key = new BindingKey(binding.ContractType, binding.Id);
            ValidateNotBound(key);
            _bindings.Add(key, binding);
        }
        
        public Binding GetBinding<TContract>()
        {
            return GetBinding(typeof(TContract));
        }
        
        public Binding GetBinding(Type contractType)
        {
            BindingKey key = new BindingKey(contractType, default);
            return GetBinding(key);
        }
        
        public Binding GetBinding(BindingKey key)
        {
            ValidateBindingExists(key);
            return _bindings[key];
        }

        public bool HasSingleInstanceOf<TContract>()
        {
            Type contract = typeof(TContract);
            return _singleInstances.ContainsKey(new BindingKey(contract, default));
        }
        
        public bool HasSingleInstanceOf(BindingKey key)
        {
            return _singleInstances.ContainsKey(key);
        }
        
        public TContract GetSingleInstance<TContract>()
        {
            Type contract = typeof(TContract);
            BindingKey key = new BindingKey(contract, default);
            ValidateHasSingleInstance(key);
            return (TContract) _singleInstances[key];
        }

        public void StoreSingleInstance<TContract>(BindingKey key, TContract instance)
        {
            ValidateBindingExists(key);
            ValidateHasNoSingleInstance(key);
            _singleInstances.Add(key, instance);
        }

        private void ValidateBindingExists(BindingKey key)
        {
            if (!_bindings.ContainsKey(key))
                throw new MissingBindingException();
        }

        private void ValidateNotBound(BindingKey key)
        {
            if (_bindings.ContainsKey(key))
                throw new AlreadyBoundException();
        }

        private void ValidateHasSingleInstance(BindingKey key)
        {
            if (!HasSingleInstanceOf(key))
                throw new MissingSingleInstanceException();
        }

        private void ValidateHasNoSingleInstance(BindingKey key)
        {
            if (HasSingleInstanceOf(key))
                throw new MissingSingleInstanceException();
        }
    }
}