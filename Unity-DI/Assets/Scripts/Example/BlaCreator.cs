using UnityEngine;

namespace SBaier.DI.Examples
{
    public class BlaCreator : MonoBehaviour, Injectable
    {
		private Camera _cam;
		private Factory<Bla> _factory;

		public void Inject(Resolver resolver)
		{
			_factory = resolver.Resolve<Factory<Bla>>();
			_cam = resolver.Resolve<Camera>();
		}

		private void Start()
		{
			Bla bla = _factory.Create();
			Debug.Log(bla.ToString());
			Debug.Log($"The Cam is {_cam}");
		}
	}
}
