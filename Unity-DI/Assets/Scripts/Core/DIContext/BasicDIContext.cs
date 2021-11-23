

using System;

namespace SBaier.DI
{
    public class BasicDIContext : DIContextBase
    {
        public override BindingContext<TConcrete> Bind<TConcrete>()
        {
            return BindToContainer<TConcrete>();
        }

        protected override TContract Resolve<TContract>(BindingKey key)
        {
            return ResolveFromContainer<TContract>(key);
        }
    }
}