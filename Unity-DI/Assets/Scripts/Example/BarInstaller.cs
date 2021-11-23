using UnityEngine;

namespace SBaier.DI
{
    public class BarInstaller : MonoInstaller, Injectable
    {
        [SerializeField]
        private Baz _baz;

		public override void InstallBindings(Binder binder)
        {
            binder.BindToNewSelf<Bar>();
            binder.BindToSelf<Baz>().FromInstanceAsSingle(_baz);
        }

		void Injectable.Inject(Resolver resolver)
		{
            _baz.FromerBaz = resolver.ResolveOptional<Baz>();
        }
	}
}