using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SBaier.DI
{
    public class SceneLoader : Injectable
    {
		private DIContext _context;

		public void Inject(Resolver resolver)
		{
            _context = resolver.Resolve<DIContext>();
        }

        public void Load(string sceneName, LoadSceneParameters parameter)
		{
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(sceneName, parameter);
        }

        public AsyncOperation Unload(string sceneName)
        {
            return SceneManager.UnloadSceneAsync(sceneName);
        }

		private void InitSceneContextOf(Scene scene)
		{
            GameObject[] sceneRootObjects = scene.GetRootGameObjects();
            List<SceneContext> contexts = new List<SceneContext>();
            foreach (GameObject sceneRootObject in sceneRootObjects)
                contexts.AddRange(sceneRootObject.GetComponentsInChildren<SceneContext>());
            Validate(contexts, scene);
            contexts[0]?.Init(_context);
        }

		private void Validate(List<SceneContext> contexts, Scene scene)
		{
            int count = contexts.Count;
            if (count <= 0)
                Debug.LogWarning($"There is no SceneContext present within scene {scene.name}. " +
                    $"DIContexts of this scene won't be initialized.");
            if (count > 1)
                throw new MultipleSceneContextsException(scene.name);
		}

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            InitSceneContextOf(scene);
        }
    }
}
