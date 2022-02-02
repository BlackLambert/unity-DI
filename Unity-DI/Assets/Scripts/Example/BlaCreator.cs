using UnityEngine;

namespace SBaier.DI.Examples
{
    public class BlaCreator : MonoBehaviour, Injectable
    {
		private Factory<Bla> _factory;

		public void Inject(Resolver resolver)
		{
			_factory = resolver.Resolve<Factory<Bla>>();
		}

		private void Start()
		{
			Bla bla = _factory.Create();
			Debug.Log(bla.ToString());
		}
	}
}
