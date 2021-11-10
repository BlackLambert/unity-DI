using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class ToBindingContext<TContract, TConcrete> where TConcrete : TContract, new()
    {
        private Binding _binding;

        public ToBindingContext(Binding binding)
        {
            _binding = binding;
            _binding.ConcreteType = typeof(TConcrete);
            _binding.CreateInstance = () => new TConcrete();
        }

        public FromBindingContext FromNew()
        {
            _binding.CreationMode = InstanceCreationMode.FromNew;
            _binding.CreateInstance = () => new TConcrete();
            return new FromBindingContext(_binding);
        }
        
        public FromBindingContext FromInstance(TConcrete instance)
        {
            _binding.CreationMode = InstanceCreationMode.FromInstance;
            _binding.CreateInstance = () => instance;
            return new FromBindingContext(_binding);
        }

        public FromBindingContext FromMethod(Func<TConcrete> createMethod)
        {
            _binding.CreationMode = InstanceCreationMode.FromMethod;
            _binding.CreateInstance = () => createMethod();
            return new FromBindingContext(_binding);
        }

        public FromBindingContext FromFactory<TFactory>() where TFactory: Factory<TConcrete>
        {
            _binding.CreationMode = InstanceCreationMode.FromFactory;
            _binding.InstanceFactoryType = typeof(TFactory);
            return new FromBindingContext(_binding);
        }
    }
}
