using UnityEngine;
using UnityEngine.SceneManagement;

namespace SBaier.DI
{
    public class SceneContext : MonoContext
    {
        private ChildDIContext _dIContext;
        public override DIContext DIContext => _dIContext;

        [SerializeField]
        private string _iD = string.Empty;
        [SerializeField]
        private string _parentContextID = string.Empty;

        private SceneInjector _injector;
        private Scene _scene;
        private SceneContextProvider _sceneContextProvider;
        
        protected override void DoInit(Resolver resolver)
		{
            _dIContext = CreateDIContext(resolver);
            InstallSceneContextBindings();
			ResolveDependencies();
			AddToProvider();
        }

        private void OnDestroy()
        {
            RemoveFromProvider();
        }

        private void AddToProvider()
		{
            if(!string.IsNullOrEmpty(_iD))
			    _sceneContextProvider.Add(_iD, this);
		}

		private void RemoveFromProvider()
		{
            if(!string.IsNullOrEmpty(_iD))
			    _sceneContextProvider.Remove(_iD);
		}

        private ChildDIContext CreateDIContext(Resolver resolver)
		{
            Factory<ChildDIContext, DIContext> contextFactory = resolver.Resolve<Factory<ChildDIContext, DIContext>>();
            DIContext parent = GetParentContext(resolver);
            return contextFactory.Create(parent);
        }

        private DIContext GetParentContext(Resolver resolver)
		{
            if (string.IsNullOrEmpty(_parentContextID))
                return resolver.Resolve<DIContext>();
            else
                return resolver.Resolve<SceneContextProvider>().Get(_parentContextID).DIContext;
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
            _sceneContextProvider = _dIContext.Resolve<SceneContextProvider>();
        }

        protected override void DoInjection()
        {
            _injector.InjectRootObjectsOf(_scene, _dIContext);
        }
    }
}

