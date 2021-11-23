namespace SBaier.DI
{
    public class BindingValidator : Injectable
    {
        private FromBindingValidator _fromValidator;

		void Injectable.Inject(Resolver resolver)
		{
            _fromValidator = resolver.Resolve<FromBindingValidator>();
        }

		public void Validate(Binding binding)
		{
            _fromValidator.Validate(binding);
        }
    }
}