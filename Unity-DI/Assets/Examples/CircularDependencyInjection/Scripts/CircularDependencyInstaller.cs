using UnityEngine;

namespace SBaier.DI.Examples.CircularDependencyInjection
{
	public class CircularDependencyInstaller : MonoInstaller
	{
		public override void InstallBindings(Binder binder)
		{
			binder.BindToNewSelf<Foo>().AsSingle().NonLazy();
			binder.BindToNewSelf<Bar>().AsSingle().NonLazy();
		}
	}
}
