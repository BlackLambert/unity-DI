using System.Collections;
using System.Collections.Generic;
using SBaier.DI;
using UnityEngine;

namespace SBaier.DI
{
    public class ChildDIContextFactory : Factory<ChildDIContext>, Injectable
    {
        private DIContext _dIContext;

        public void Inject(Resolver resolver)
        {
            _dIContext = resolver.Resolve<DIContext>();
        }
        
        public ChildDIContext Create()
        {
            ChildDIContext result = new ChildDIContext();
            result.Inject(_dIContext);
            return result;
        }
    }
}

