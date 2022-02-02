using UnityEngine.SceneManagement;

namespace SBaier.DI
{
    public class SceneContext : MonoContext
    {
        private BasicDIContext _dIContext;
        protected override DIContext DIContext => _dIContext;

        private SceneInjector _injector;
        private Scene _scene;

        private void Awake()
        {
            Init(new Bootstrapper().Resolver);
        }
        
        protected override void DoInit(Resolver resolver)
        {
            _dIContext = resolver.Resolve<BasicDIContext>();
            InstallSceneContextBindings();
            DIContext.ValidateBindings();
            ResolveDependencies();
        }

        private void InstallSceneContextBindings()
        {
            SceneContextInstaller installer = new SceneContextInstaller(gameObject, _dIContext);
            installer.InstallBindings(_dIContext);
        }

        private void ResolveDependencies()
        {
            _injector = _dIContext.Resolve<SceneInjector>();
            _scene = _dIContext.Resolve<Scene>();
        }

        protected override void DoInjection()
        {
            _injector.InjectRootObjectsOf(_scene, _dIContext);
        }
    }
}

