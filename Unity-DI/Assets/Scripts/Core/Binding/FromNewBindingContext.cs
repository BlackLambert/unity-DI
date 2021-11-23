using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{

	public class FromNewBindingContext<TConcrete> : FromBindingContext where TConcrete : new()
	{
		public FromNewBindingContext(Binding binding) : base(binding)
		{
			binding.ConcreteType = typeof(TConcrete);
			binding.CreationMode = InstanceCreationMode.FromNew;
			binding.CreateInstanceFunction = () => new TConcrete();
		}
	}
}
