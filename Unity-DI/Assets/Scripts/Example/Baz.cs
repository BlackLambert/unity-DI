using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class Baz : MonoBehaviour
    {
        [SerializeField]
        private string _name = "Baz";
        public string Name => _name;
    }
}