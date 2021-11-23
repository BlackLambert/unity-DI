using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
	public class BindingValidatorDependencyResolver : BasicInstanceResolver
	{
		protected override Dictionary<BindingKey, object> Instances { get; } = new();

		public BindingValidatorDependencyResolver()
		{
			Instances.Add(new BindingKey(typeof(FromBindingValidator), default), new FromBindingValidator());
		}
	}
}