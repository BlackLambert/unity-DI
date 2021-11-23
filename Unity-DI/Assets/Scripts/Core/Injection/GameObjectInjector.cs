using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

namespace SBaier.DI
{
    public class GameObjectInjector
    {
        
        
        public void InjectIntoHierarchy(Transform root, DIContext context)
        {
            InjectInto(root, context);
            InjectIntoChildren(root, context);
        }

        public void InjectIntoContextHierarchy(Transform root, DIContext context)
        {
            GameObjectContext gameObjectContext = root.GetComponent<GameObjectContext>();
            if (gameObjectContext != null)
                gameObjectContext.Init(context);
            else
                InjectIntoHierarchy(root, context);
        }

        private void InjectIntoChildren(Transform root, DIContext context)
        {
            foreach (Transform child in root)
                InjectIntoContextHierarchy(child, context);
        }

        public void InjectInto(Transform root, DIContext context)
        {
            Injectable[] injectables = root.GetComponents<Injectable>();
            foreach (Injectable injectable in injectables)
                InjectInto(injectable, context);
        }

        private void InjectInto(Injectable injectable, DIContext context)
        {
            if (injectable is Installer)
                return;
            injectable.Inject(context);
        }
    }
}