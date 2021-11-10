using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class AsBindingContext
    {
        private Binding _binding;

        public AsBindingContext(Binding binding)
        {
            _binding = binding;
        }

        public void WithID(IComparable iD)
        {
            _binding.Id = iD;
        }
    }
}

