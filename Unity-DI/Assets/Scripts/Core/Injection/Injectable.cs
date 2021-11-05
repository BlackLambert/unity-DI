using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public interface Injectable
    {
        void Inject(Resolver context);
    }
}