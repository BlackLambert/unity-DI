using NUnit.Framework;

namespace SBaier.DI.Tests
{
    public class BindingTests
    {
        [Test]
        public void Binding_ToString_ContainsAllPropertyValues()
        {
            Binding binding = GivenANewBinding();
            string bindingString = WhenToStringIsCalledOn(binding);
            ThenStringContainsAllPropertyValues(bindingString, binding);
        }

        [Test]
        public void Binding_ToString_ContainsClassName()
        {
            Binding binding = GivenANewBinding();
            string bindingString = WhenToStringIsCalledOn(binding);
            ThenStringContainsClassName(bindingString);
        }

		private Binding GivenANewBinding()
		{
            Binding binding = new Binding(typeof(Foo));
            binding.ConcreteType = typeof(Bar);
            binding.AmountMode = InstanceAmountMode.Single;
            binding.CreateInstanceFunction = () => new Bar();
            binding.CreationMode = InstanceCreationMode.FromMethod;
            binding.Id = 21;
            binding.InjectionAllowed = true;
            binding.InstanceFactoryType = typeof(FooFactory);
            return binding;
        }

        private string WhenToStringIsCalledOn(Binding binding)
        {
            return binding.ToString();
        }

        private void ThenStringContainsAllPropertyValues(string bindingString, Binding binding)
        {
            Assert.IsTrue(bindingString.Contains(binding.ContractType.ToString()));
            Assert.IsTrue(bindingString.Contains(binding.ConcreteType.ToString()));
            Assert.IsTrue(bindingString.Contains(binding.AmountMode.ToString()));
            Assert.IsTrue(bindingString.Contains(binding.CreateInstanceFunction.ToString()));
            Assert.IsTrue(bindingString.Contains(binding.CreationMode.ToString()));
            Assert.IsTrue(bindingString.Contains(binding.Id.ToString()));
            Assert.IsTrue(bindingString.Contains(binding.InjectionAllowed.ToString()));
            Assert.IsTrue(bindingString.Contains(binding.InstanceFactoryType.ToString()));
        }

        private void ThenStringContainsClassName(string bindingString)
        {
            Assert.IsTrue(bindingString.Contains(nameof(Binding)));
        }

        private abstract class Foo { }
        private class FooFactory { }
        private class Bar : Foo { }
	}
}