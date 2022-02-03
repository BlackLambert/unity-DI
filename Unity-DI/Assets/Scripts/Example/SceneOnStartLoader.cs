using UnityEngine;

namespace SBaier.DI.Examples
{
    public class SceneOnStartLoader : MonoBehaviour, Injectable
    {
		[SerializeField]
		private string _sceneName;

		private SceneLoader _sceneLoader;

		public void Inject(Resolver resolver)
		{
			_sceneLoader = resolver.Resolve<SceneLoader>();
		}

		private void Start()
		{
			UnityEngine.SceneManagement.LoadSceneParameters paramerters =
				new UnityEngine.SceneManagement.LoadSceneParameters(UnityEngine.SceneManagement.LoadSceneMode.Additive);
			_sceneLoader.Load(_sceneName, paramerters);
		}
	}
}
