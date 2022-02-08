using UnityEngine;

namespace SBaier.DI.Examples
{
    public class Phi : MonoBehaviour, IPhi
    {
        [SerializeField]
        private string _name = "Default";

		public string Name => _name;
	}
}
