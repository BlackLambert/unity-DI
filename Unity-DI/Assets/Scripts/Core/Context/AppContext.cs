using UnityEngine;

namespace SBaier.DI
{
    public class AppContext : MonoContext
    {
        private BasicDIContext _dIContext;
        protected override DIContext DIContext => _dIContext;

        private void Awake()
        {
            Init(new Bootstrapper().Resolver);
        }

        protected override void DoInit(Resolver resolver)
        {
            _dIContext = resolver.Resolve<BasicDIContext>();
            InstallSceneContextBindings();
            DIContext.ValidateBindings();
        }

        private void InstallSceneContextBindings()
        {
            AppContextInstaller installer = new AppContextInstaller(_dIContext);
            installer.InstallBindings(_dIContext);
        }

		protected override void DoInjection()
		{
            SceneContext sceneContext = FindObjectOfType<SceneContext>();
            if (sceneContext == null)
                throw new MissingSceneContextException();
            sceneContext.Init(_dIContext);
        }
	}
}
