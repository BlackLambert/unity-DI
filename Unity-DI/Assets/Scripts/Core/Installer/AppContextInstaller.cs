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
            binder.BindToSelf<DIContainers>().FromFactory();
            binder.Bind<Factory<DIContainers>>().ToNew<DIContainersFactory>();
            binder.BindToNewSelf<SceneLoader>();
            binder.BindToNewSelf<SceneContextProvider>().AsSingle();
            binder.Bind<Factory<ChildDIContext, DIContext>>().ToNew<ChildDIContextFactory>();
            new BindingValidationInstaller().InstallBindings(binder);
        }
    }
}
