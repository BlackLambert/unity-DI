using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class GameObjectInjector
    {
        public void InjectIntoHierarchy(Transform root, DIContext context)
        {
            InjectInto(root, context);
            foreach (Transform child in root)
                InjectIntoHierarchy(child, context);
        }

        public void InjectInto(Transform root, DIContext context)
        {
            Injectable[] injectables = root.GetComponents<Injectable>();
            foreach (Injectable injectable in injectables)
                injectable.Inject(context);
        }
    }
}