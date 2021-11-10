using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public enum InstanceCreationMode
    {
        Undefined = 0,
        FromNew = 1,
        FromInstance = 2,
        FromMethod = 4,
        FromFactory = 8
    }
}