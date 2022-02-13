using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    [DisallowMultipleComponent]
    public abstract class MonoContext : MonoBehaviour, Context
    {
        [SerializeField]
        private MonoInstaller[] _monoInstallers = new MonoInstaller[0];

        [SerializeField]
        private ScriptableObjectInstaller[] _scriptableObjectInstallers = new ScriptableObjectInstaller[0];

        private List<Installer> _installers = new List<Installer>();
        public abstract DIContext DIContext { get; }


        public virtual void Init(Resolver baseResolver)
        {
            DoInit(baseResolver);
            InjectIntoInstallers();
            InstallBindings(baseResolver);
            DIContext.ValidateBindings();
            DoInjection();
            DIContext.CreateNonLazyInstances();
        }

		public void AddInstaller(Installer installer)
        {
            _installers.Add(installer);
        }

        protected abstract void DoInit(Resolver resolver);

        protected abstract void DoInjection();

        private void InstallBindings(Resolver resolver)
        {
            foreach (Installer installer in GetAllInstallers())
                InstallBindings(installer, resolver);
        }

        private List<Installer> GetAllInstallers()
        {
            List<Installer> result = new List<Installer>(_monoInstallers);
            result.AddRange(_installers);
            result.AddRange(_scriptableObjectInstallers);
            return result;
        }

        private void InstallBindings(Installer installer, Resolver resolver)
        {
            (installer as Injectable)?.Inject(resolver);
            installer.InstallBindings(DIContext);
        }

        private void InjectIntoInstallers()
        {
            List<Installer> installers = GetAllInstallers();
            foreach (Installer installer in installers)
                (installer as Injectable)?.Inject(DIContext);
        }
    }
}

