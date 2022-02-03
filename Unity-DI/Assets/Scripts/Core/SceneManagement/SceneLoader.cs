using System;
using System.Collections;
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
            IEnumerable<GameObject> contextObjects = sceneRootObjects.Where(g => g.GetComponent<SceneContext>() != null);
            List<SceneContext> contexts = contextObjects.Select(g => g.GetComponent<SceneContext>()).ToList();
            Validate(contexts, scene);
            contexts.First().Init(_context);
        }

		private void Validate(List<SceneContext> contexts, Scene scene)
		{
            if (contexts.Count <= 0)
                throw new MissingSceneContextException(scene.name);
            if (contexts.Count > 1)
                throw new MultipleSceneContextsException(scene.name);
		}

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            InitSceneContextOf(scene);
        }
    }
}
