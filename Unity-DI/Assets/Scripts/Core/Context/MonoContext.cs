using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SBaier.DI
{
    [DisallowMultipleComponent]
    public abstract class MonoContext : MonoBehaviour, Context
    {
        [SerializeField]
        [FormerlySerializedAs("_installers")]
        private MonoInstaller[] _monoInstallers;

        List<Installer> _installers = new();
        protected abstract DIContext DIContext { get; }


        public virtual void Init(Resolver baseResolver)
        {
            DoInit(baseResolver);
            InstallBindings(baseResolver);
            DoInjection();
        }

        protected abstract void DoInit(Resolver resolver);

        private void InstallBindings(Resolver resolver)
        {
            foreach (Installer installer in GetAllInstallers())
                InstallBindings(installer, resolver);
        }

        private List<Installer> GetAllInstallers()
        {
            List<Installer> result = new List<Installer>(_monoInstallers);
            result.AddRange(_installers);
            return result;
        }

        private void InstallBindings(Installer installer, Resolver resolver)
        {
            (installer as Injectable)?.Inject(resolver);
            installer.InstallBindings(DIContext);
        }

        public void AddInstaller(Installer installer)
        {
            _installers.Add(installer);
        }

        protected abstract void DoInjection();
    }
}

