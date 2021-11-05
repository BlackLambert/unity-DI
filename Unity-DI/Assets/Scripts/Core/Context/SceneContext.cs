using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SBaier.DI
{
    public class SceneContext : MonoBehaviour
    {
        [SerializeField]
        private MonoInstaller[] _installers;

        private DIContext _dIContext;
        private SceneInjector _injector;

        private void Awake()
        {
            DIContainer container = new DIContainer();
            _dIContext = new BasicDIContext(container);
            GameObjectInjector injector = new GameObjectInjector();
            _injector = new SceneInjector(injector);

            foreach (Installer installer in _installers)
                installer.InstallBindings(_dIContext);

            Scene scene = gameObject.scene;
            _injector.InjectRootObjectsOf(scene, _dIContext);
        }
    }
}

