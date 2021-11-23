using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class BasicDIContextDependencyResolver : BasicInstanceResolver
    {
        private Dictionary<BindingKey, object> _instances = new();
        protected override Dictionary<BindingKey, object> Instances => _instances;

        public BasicDIContextDependencyResolver()
        {
            BindingValidator validator = new BindingValidator();
            (validator as Injectable).Inject(new BindingValidatorDependencyResolver());
            _instances.Add(new BindingKey(typeof(BindingValidator), default), validator);
            _instances.Add(new BindingKey(typeof(DIContainer), default), new DIContainer());
            _instances.Add(new BindingKey(typeof(DIInstanceFactory), default), new DIInstanceFactory());
        }
    }
}

