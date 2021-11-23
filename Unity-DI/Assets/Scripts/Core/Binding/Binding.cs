using System;

namespace SBaier.DI
{
    public class Binding
    {
        public Type ContractType;
        public Type ConcreteType;
        public InstanceCreationMode CreationMode;
        public InstanceAmountMode AmountMode;
        public Func<object> CreateInstanceFunction;
        public Type InstanceFactoryType;
        public IComparable Id;
        public bool InjectionAllowed;

        public Binding(Type contractType)
        {
            ContractType = contractType;
            ConcreteType = contractType;
            CreationMode = InstanceCreationMode.Undefined;
            AmountMode = InstanceAmountMode.PerRequest;
            CreateInstanceFunction = null;
            InstanceFactoryType = null;
            Id = default;
            InjectionAllowed = true;
        }

		public override string ToString()
		{
            return $"Binding (Contract: {ContractType} | Concrete: {ConcreteType} | CreationMode: {CreationMode} | " +
                $" CreateInstanceFunction: {CreateInstanceFunction} | InstanceFactoryType: {null} | AmountMode: {AmountMode}" +
                $" | ID: {Id} | InjectionAllowed: {InjectionAllowed})";
		}
	}
}