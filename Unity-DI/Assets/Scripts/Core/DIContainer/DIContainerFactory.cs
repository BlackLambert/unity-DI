using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class DIContainerFactory : Factory<DIContainer>
    {
        public DIContainer Create()
        {
            return new DIContainer();
        }
    } 
}

