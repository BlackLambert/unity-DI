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
            binder.BindToSelf<Bar>();
            binder.BindToSelf<Baz>().FromInstance(_baz).AsSingle();
        }
    }
}