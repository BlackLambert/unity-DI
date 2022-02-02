using System;

namespace SBaier.DI
{
    public class AsBindingContext
    {
        private Binding _binding;

        public AsBindingContext(Binding binding)
        {
            _binding = binding;
        }

        public AsBindingContext WithArgument<TArg>(TArg argument, IComparable iD = default)
        {
            BindingKey key = new BindingKey(typeof(TArg), iD);
            _binding.Arguments.Add(key, argument);
            return this;
        }
    }
}

