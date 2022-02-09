using System;
using System.Collections.Generic;

namespace SBaier.DI
{
    public class DIContainer 
    {
        private readonly Dictionary<BindingKey, Binding> _bindings = new Dictionary<BindingKey, Binding>();
        private readonly Dictionary<Binding, object> _singleInstances = new Dictionary<Binding, object>();
        
        public void AddBinding(BindingKey key, Binding binding)
        {
            ValidateNotBound(key);
            _bindings.Add(key, binding);
        }
        
        public Binding GetBinding<TContract>(IComparable iD = default)
        {
            return GetBinding(typeof(TContract), iD);
        }
        
        public Binding GetBinding(Type contractType, IComparable iD = default)
        {
            BindingKey key = new BindingKey(contractType, iD);
            return GetBinding(key);
        }
        
        public Binding GetBinding(BindingKey key)
        {
            ValidateBindingExists(key);
            return _bindings[key];
        }

        public IEnumerable<Binding> GetBindings()
		{
            return _bindings.Values;
		}

        public bool HasSingleInstanceOf<TContract>(IComparable iD = default)
        {
            BindingKey key = CreateKey<TContract>(iD);
            Binding binding = GetBinding(key);
            return HasSingleInstanceOf(binding);
        }

        public bool HasSingleInstanceOf(Binding binding)
        {
            return _singleInstances.ContainsKey(binding);
        }
        
        public TContract GetSingleInstance<TContract>(Binding binding)
        {
            ValidateHasSingleInstance(binding);
            return (TContract) _singleInstances[binding];
        }

        public void StoreSingleInstance<TContract>(Binding key, TContract instance)
        {
            ValidateHasNoSingleInstance(key);
            _singleInstances.Add(key, instance);
        }

		public bool HasBinding(BindingKey key)
		{
            return _bindings.ContainsKey(key);
        }

        private BindingKey CreateKey<TContract>(IComparable iD = default)
		{
            Type contract = typeof(TContract);
            return new BindingKey(contract, default);
        }

        private void ValidateBindingExists(BindingKey key)
        {
            if (!HasBinding(key))
                throw new MissingBindingException($"There is no Binding for Contract {key}");
        }

        private void ValidateNotBound(BindingKey key)
        {
            if (HasBinding(key))
                throw new AlreadyBoundException();
        }

        private void ValidateHasSingleInstance(Binding key)
        {
            if (!HasSingleInstanceOf(key))
                throw new MissingSingleInstanceException();
        }

        private void ValidateHasNoSingleInstance(Binding key)
        {
            if (HasSingleInstanceOf(key))
                throw new MissingSingleInstanceException();
        }
    }
}