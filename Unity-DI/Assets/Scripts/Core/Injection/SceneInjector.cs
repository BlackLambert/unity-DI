
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SBaier.DI
{
    public class SceneInjector : Injectable
    {
        private GameObjectInjector _injector;

        public SceneInjector(){}

        public void Inject(Resolver resolver)
        {
            _injector = resolver.Resolve<GameObjectInjector>();
        }

        public void InjectRootObjectsOf(Scene scene, DIContext context)
        { 
            GameObject[] sceneRootObjects = scene.GetRootGameObjects();
            foreach (GameObject rootObject in sceneRootObjects)
                _injector.InjectIntoContextHierarchy(rootObject.transform, context);
        }
    }
}