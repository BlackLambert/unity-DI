using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class BarInstaller : MonoInstaller
    {
        [SerializeField]
        private Baz _baz;

        public override void InstallBindings(Binder binder)
        {
            binder.Bind<Bar>().FromInstance(new Bar());
            binder.Bind<Baz>().FromInstance(_baz);
        }
    }
}