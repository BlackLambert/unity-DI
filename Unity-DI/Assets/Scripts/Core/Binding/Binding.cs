using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class Binding
    {
        public Type ContractType;
        public Type ConcreteType;
        public InstanceCreationMode CreationMode;
        public InstanceAmountMode AmountMode;
        public Func<object> CreateInstance;

        public Binding(Type contractType)
        {
            ContractType = contractType;
            ConcreteType = contractType;
            CreationMode = InstanceCreationMode.FromNew;
            AmountMode = InstanceAmountMode.PerRequest;
            CreateInstance = null;
        }

        
    }
}