using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class Bar: Injectable
    {
        private Baz _baz;
        
        public void Inject(Resolver context)
        {
            _baz = context.Resolve<Baz>();
            Debug.Log($"Bar: Baz {_baz}");
        }
    }
}
