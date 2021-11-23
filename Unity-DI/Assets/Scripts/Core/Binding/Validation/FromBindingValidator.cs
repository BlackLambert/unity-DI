using System;

namespace SBaier.DI
{
    public class FromBindingValidator
    {
        internal void Validate(Binding binding)
        {
            switch(binding.CreationMode)
			{
                case InstanceCreationMode.Undefined:
                    throw new InvalidBindingException($"{binding} has no creation mode defined." +
                        $"Please specify the creation mode.");
                case InstanceCreationMode.FromFactory:
                case InstanceCreationMode.FromInstance:
                case InstanceCreationMode.FromMethod:
                case InstanceCreationMode.FromNew:
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }

}
