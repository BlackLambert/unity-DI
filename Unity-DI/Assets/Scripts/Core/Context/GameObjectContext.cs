using UnityEngine;

namespace SBaier.DI
{
    public class GameObjectContext : MonoContext
    {
        private ChildDIContext _currentContext;
        private GameObjectInjector _injector;
        protected override DIContext DIContext => _currentContext;
        
        protected override void DoInit(Resolver resolver)
        {
            _injector = resolver.Resolve<GameObjectInjector>();
            Factory<ChildDIContext> contextFactory = resolver.Resolve<Factory<ChildDIContext>>();
            _currentContext = contextFactory.Create();
        }

        protected override void DoInjection()
        {
            Debug.Log($"{name} injecting into hierarchie");
            _injector.InjectIntoHierarchy(transform, _currentContext);
        }
    }
}

