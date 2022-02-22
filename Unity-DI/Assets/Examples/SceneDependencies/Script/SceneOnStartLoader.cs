using UnityEngine;

namespace SBaier.DI.Examples.SceneDependencies
{
    public class SceneOnStartLoader : MonoBehaviour, Injectable
    {
		[SerializeField]
		private SceneName _sceneName;
		[SerializeField]
		private UnityEngine.SceneManagement.LoadSceneMode _loadMode = UnityEngine.SceneManagement.LoadSceneMode.Additive;

		private SceneLoader _sceneLoader;

		public void Inject(Resolver resolver)
		{
			_sceneLoader = resolver.Resolve<SceneLoader>();
		}

		private void Start()
		{
			UnityEngine.SceneManagement.LoadSceneParameters paramerters =
				new UnityEngine.SceneManagement.LoadSceneParameters(_loadMode);
			_sceneLoader.Load(_sceneName.ToString(), paramerters);
		}
	}
}
