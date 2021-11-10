using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public abstract class BasicInstanceResolver : Resolver
    {
        protected abstract Dictionary<BindingKey, object> Instances { get; }

        public TContract Resolve<TContract>()
        {
            return Resolve<TContract>(default);
        }

        public TContract Resolve<TContract>(IComparable iD)
        {
            return (TContract) Instances[new BindingKey(typeof(TContract), iD)];
        }
    }
}

