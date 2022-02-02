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
            (result as Injectable).Inject(_dIContext);
            return result;
        }
    }
}

