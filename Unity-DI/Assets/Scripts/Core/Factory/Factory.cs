using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public interface Factory
    {
        
    }
    
    public interface Factory<out T> : Factory
    {
        public T Create();
    }
}

