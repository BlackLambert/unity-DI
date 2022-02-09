using System;
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
                case InstanceCreationMode.FromPrefabInstance:
                    return CreatePrefabInstance<TInstance>(context, binding);
                case InstanceCreationMode.FromRessourcePrefabInstance:
                    return CreatePrefabInstanceFromRessources<TInstance>(context, binding);
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

        private TInstance CreatePrefabInstance<TInstance>(DIContext context, Binding binding)
        {
            GameObject prefab = binding.CreateInstanceFunction() as GameObject;
            return CreatePrefabInstance<TInstance>(prefab);
        }

        private TInstance CreatePrefabInstanceFromRessources<TInstance>(DIContext context, Binding binding)
        {
            string path = binding.CreateInstanceFunction() as string;
            return CreatePrefabInstance<TInstance>(Resources.Load<GameObject>(path));
        }

        private TInstance CreatePrefabInstance<TInstance>(GameObject prefab)
		{
            GameObject instance = GameObject.Instantiate(prefab);
            return instance.GetComponent<TInstance>();
		}
    }

}
