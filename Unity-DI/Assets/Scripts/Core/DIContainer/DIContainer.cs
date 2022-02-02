using System;
using System.Collections.Generic;

namespace SBaier.DI
{
    public class DIContainer 
    {
        private readonly Dictionary<BindingKey, Binding> _bindings = new Dictionary<BindingKey, Binding>();
        private readonly Dictionary<BindingKey, object> _singleInstances = new Dictionary<BindingKey, object>();
        
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

        public bool HasSingleInstanceOf<TContract>()
        {
            Type contract = typeof(TContract);
            return _singleInstances.ContainsKey(new BindingKey(contract, default));
        }
        
        public bool HasSingleInstanceOf(BindingKey key)
        {
            return _singleInstances.ContainsKey(key);
        }
        
        public TContract GetSingleInstance<TContract>(IComparable iD = default)
        {
            BindingKey key = CreateKey<TContract>(iD);
            ValidateHasSingleInstance(key);
            return (TContract) _singleInstances[key];
        }

        public void StoreSingleInstance<TContract>(BindingKey key, TContract instance)
        {
            ValidateBindingExists(key);
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
                throw new MissingBindingException($"There is no Binding for Contract {key.Type}");
        }

        private void ValidateNotBound(BindingKey key)
        {
            if (HasBinding(key))
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