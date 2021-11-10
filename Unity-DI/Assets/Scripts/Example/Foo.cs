using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI.Example
{
    public class Foo : MonoBehaviour, Injectable
    {
        private Bar _bar;
        private Baz _baz;

        public void Inject(Resolver context)
        {
            _bar = context.Resolve<Bar>();
            _baz = context.Resolve<Baz>();
            Debug.Log(_bar);
            Debug.Log($"Baz with name {_baz.Name}");
        }
    }
}