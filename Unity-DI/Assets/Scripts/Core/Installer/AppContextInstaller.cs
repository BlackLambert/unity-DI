using UnityEngine;

namespace SBaier.DI
{
    public class AppContextInstaller : Installer
    {
        private DIContext _diContext;

        public AppContextInstaller(DIContext dIContext)
        {
            _diContext = dIContext;
        }

        public void InstallBindings(Binder binder)
        {
            binder.BindToNewSelf<GameObjectInjector>();
            binder.BindToNewSelf<SceneInjector>();
            binder.BindToSelf<DIContext>().FromInstanceAsSingle(_diContext);
            binder.BindToNewSelf<DIInstanceFactory>();
            binder.BindToSelf<DIContainer>().FromFactory();
            binder.Bind<Factory<DIContainer>>().ToNew<DIContainerFactory>();
            binder.Bind<Factory<ChildDIContext>>().ToNew<ChildDIContextFactory>();
            new BindingValidationInstaller().InstallBindings(binder);
        }
    }
}
