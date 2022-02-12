using System;
using System.IO;
using UnityEngine;

namespace SBaier.DI
{
	public class ToComponentBindingContext<TConcrete> : ToBindingContext<TConcrete> where TConcrete : Component
	{
		public ToComponentBindingContext(BindingArguments arguments) : base(arguments) { }

		public FromBindingContext FromNewPrefabInstance(GameObject prefab)
		{
			ValidateHasComponent(prefab);
			_binding.CreationMode = InstanceCreationMode.FromPrefabInstance;
			_binding.CreateInstanceFunction = () => prefab;
			return new FromBindingContext(_arguments);
		}

		public FromBindingContext FromNewPrefabInstance(TConcrete prefab)
		{
			_binding.CreationMode = InstanceCreationMode.FromPrefabInstance;
			_binding.CreateInstanceFunction = () => prefab.gameObject;
			return new FromBindingContext(_arguments);
		}

		public FromBindingContext FromNewRessourcePrefabInstance(string path)
		{
			ValidateRessourcePath(path);
			_binding.CreationMode = InstanceCreationMode.FromRessourcePrefabInstance;
			_binding.CreateInstanceFunction = () => path;
			return new FromBindingContext(_arguments);
		}

		private void ValidateRessourcePath(string path)
		{
			if(Resources.Load<GameObject>(path) == null)
				throw new MissingComponentException();
		}

		private void ValidateHasComponent(GameObject gameObject)
		{
			TConcrete target = gameObject.GetComponent<TConcrete>();
			if (target == null)
				throw new MissingComponentException();
		}
	}
}
