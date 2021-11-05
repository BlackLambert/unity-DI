
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SBaier.DI
{
    public class SceneInjector
    {
        private GameObjectInjector _injector;

        public SceneInjector(GameObjectInjector injector)
        {
            _injector = injector;
        }

        public void InjectRootObjectsOf(Scene scene, DIContext context)
        { 
            GameObject[] sceneRootObjects = scene.GetRootGameObjects();
            foreach (GameObject rootObject in sceneRootObjects)
                _injector.InjectIntoHierarchy(rootObject.transform, context);
        }
    }
}