using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace SBaier.DI
{
    public class Bootstrapper : BasicInstanceResolver
    {
        private Dictionary<BindingKey, object> _instances = new();
        protected override Dictionary<BindingKey, object> Instances => _instances;

        public Bootstrapper()
        {
            BasicDIContext context = new BasicDIContext();
            context.Inject(new BasicDIContextDependencyResolver());
            _instances.Add(new BindingKey(typeof(BasicDIContext), default), context);
        }
    }
}

