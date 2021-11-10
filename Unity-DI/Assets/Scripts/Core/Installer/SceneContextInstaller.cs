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
            binder.BindToSelf<GameObjectInjector>();
            binder.BindToSelf<SceneInjector>();
            binder.BindToSelf<Scene>().FromInstance(_contextObject.scene).AsSingle();
            binder.Bind<DIContext>().To<BasicDIContext>().FromInstance(_diContext).AsSingle();
            binder.BindToSelf<DIInstanceFactory>();
            binder.Bind<DIContainer>().To<DIContainer>().FromFactory<DIContainerFactory>();
            binder.Bind<Factory<DIContainer>>().To<DIContainerFactory>();
            binder.Bind<Factory<ChildDIContext>>().To<ChildDIContextFactory>();
        }
    }
}

