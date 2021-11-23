using System.Collections.Generic;

namespace SBaier.DI
{
    public class Bootstrapper : BasicInstanceResolver
    {
        private Dictionary<BindingKey, object> _instances = new();
        protected override Dictionary<BindingKey, object> Instances => _instances;

        public Bootstrapper()
        {
            BasicDIContext context = new BasicDIContext();
            (context as Injectable).Inject(new BasicDIContextDependencyResolver());
            _instances.Add(new BindingKey(typeof(BasicDIContext), default), context);
        }
    }
}

