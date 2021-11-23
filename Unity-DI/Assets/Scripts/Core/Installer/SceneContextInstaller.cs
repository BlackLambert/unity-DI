using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SBaier.DI
{
    public class SceneContextInstaller : Installer
    {
        private GameObject _contextObject;
        private BasicDIContext _diContext;

        public SceneContextInstaller(GameObject contextObject, BasicDIContext dIContext)
        {
            _contextObject = contextObject;
            _diContext = dIContext;
        }
        
        public void InstallBindings(Binder binder)
        {
            binder.BindToNewSelf<GameObjectInjector>();
            binder.BindToNewSelf<SceneInjector>();
            binder.BindToSelf<Scene>().FromInstanceAsSingle(_contextObject.scene);
            binder.BindToSelf<DIContext>().FromInstanceAsSingle(_diContext);
            binder.BindToNewSelf<DIInstanceFactory>();
            binder.Bind<DIContainer>().To<DIContainer>().FromFactory<DIContainerFactory>();
            binder.Bind<Factory<DIContainer>>().ToNew<DIContainerFactory>();
            binder.Bind<Factory<ChildDIContext>>().ToNew<ChildDIContextFactory>();
            new BindingValidationInstaller().InstallBindings(binder);
        }
    }
}

