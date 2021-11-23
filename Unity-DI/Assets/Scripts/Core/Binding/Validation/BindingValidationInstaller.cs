using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
	public class BindingValidationInstaller : Installer
	{
		public void InstallBindings(Binder binder)
		{
			binder.BindToNewSelf<FromBindingValidator>();
			binder.BindToNewSelf<BindingValidator>();
		}
	}
}