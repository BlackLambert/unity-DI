using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class ChildDIContext : DIContextBase
    {
        private DIContext _baseContext;

        public override void Inject(Resolver resolver)
        {
            base.Inject(resolver);
            _baseContext = resolver.Resolve<DIContext>();
        }

        public override BindingContext<TContract> Bind<TContract>()
        {
            return BindToContainer<TContract>();
        }

        protected override TContract Resolve<TContract>(BindingKey key)
        {
            try
            {
                return ResolveFromContainer<TContract>(key);
            }
            catch(MissingBindingException)
            {
                return _baseContext.Resolve<TContract>(key.ID);
            }
        }
    }
}