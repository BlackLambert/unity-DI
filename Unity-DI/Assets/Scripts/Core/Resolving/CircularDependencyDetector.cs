using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
	public class CircularDependencyDetector : ResolverBase
	{
		private HashSet<BindingKey> resolveStack = new HashSet<BindingKey>();
		private Resolver _baseResolver;

		public CircularDependencyDetector(Resolver context)
		{
			_baseResolver = context;
		}

		public override bool IsResolvable(BindingKey key)
		{
			return _baseResolver.IsResolvable(key);
		}

		protected override TContract DoResolve<TContract>(BindingKey key)
		{
			ValidateIsNoCircularDependeny(key);
			resolveStack.Add(key);
			//PrintStack($"Stack before when resolving {typeof(TContract)}: ");
			TContract result = _baseResolver.Resolve<TContract>(key);
			resolveStack.Remove(key);
			//PrintStack($"Stack after when resolving {typeof(TContract)}: ");
			return result;
		}

		private void ValidateIsNoCircularDependeny(BindingKey key)
		{
			if (resolveStack.Contains(key))
				throw new CircularDependencyException(key);
		}

		private void PrintStack(string message)
		{
			string addition = string.Empty;
			foreach (BindingKey key in resolveStack)
				addition += $"{key} | ";
			Debug.Log(message + addition);
		}
	}
}
