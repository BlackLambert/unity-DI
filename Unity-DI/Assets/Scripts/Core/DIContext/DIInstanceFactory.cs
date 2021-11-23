using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  SBaier.DI
{
    public class DIInstanceFactory
    {
        public TInstance Create<TInstance>(DIContext context, Binding binding)
        {
            switch (binding.CreationMode)
            {
                case InstanceCreationMode.FromNew:
                case InstanceCreationMode.FromMethod:
                case InstanceCreationMode.FromInstance:
                    return (TInstance) binding.CreateInstanceFunction();
                case InstanceCreationMode.FromFactory:
                    return CreateByFactory<TInstance>(context);
                case InstanceCreationMode.Undefined:
                    throw new ArgumentException($"Creation of {typeof(TInstance)} failed. " +
                        $"Can't create an instance with creation Mode {binding.CreationMode}");
                default:
                    throw new NotImplementedException();
            }
        }

        private TInstance CreateByFactory<TInstance>(DIContext context)
        {
            Factory<TInstance> factory = context.Resolve<Factory<TInstance>>();
            return factory.Create();
        }
    }

}
