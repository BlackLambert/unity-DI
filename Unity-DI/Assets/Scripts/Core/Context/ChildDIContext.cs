using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class ChildDIContext : DIContext
    {
        private DIContainer _container;
        private DIContext _baseContext;

        public ChildDIContext(DIContext baseContext, DIContainer container)
        {
            _baseContext = baseContext;
            _container = container;
        }

        public BindingContext<TContract> Bind<TContract>()
        {
            return _container.Bind<TContract>();
        }

        public TContract Resolve<TContract>()
        {
            try
            {
                return _container.Resolve<TContract>();
            }
            catch(MissingBindingException)
            {
                return _baseContext.Resolve<TContract>();
            }
        }
    }
}