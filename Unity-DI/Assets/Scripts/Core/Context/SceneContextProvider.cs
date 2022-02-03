using System;
using System.Collections.Generic;

namespace SBaier.DI
{
    public class SceneContextProvider
    {
        private const string alreadyAddedExceptionMessage = "The scene with iD {0} has already been added to the provider.";
        private const string notAddedExceptionMessage = "The scene with iD {0} has not been added to the provider. Therefore removing it is not possible";
        private const string getExceptionMessage = "The scene with iD {0} has not been added to the provider. Therefore getting it is not possible";

        private Dictionary<string, SceneContext> _sceneContexts = new Dictionary<string, SceneContext>();

        public void Add(string iD, SceneContext context)
        {
            ValidateAdd(iD);
            _sceneContexts[iD] = context;
        }

        public void Remove(string iD)
        {
            ValidateRemove(iD);
            _sceneContexts.Remove(iD);
        }

		public SceneContext Get(string iD)
        {
            ValidateGet(iD);
            return _sceneContexts[iD];
        }

        private void ValidateAdd(string iD)
        {
            if (_sceneContexts.ContainsKey(iD))
                throw new AlreadyAddedException(iD);
        }

        private void ValidateRemove(string iD)
        {
            if (!_sceneContexts.ContainsKey(iD))
                throw new AlreadyAddedException(iD);
        }

        private void ValidateGet(string iD)
        {
            if (!_sceneContexts.ContainsKey(iD))
                throw new GetException(iD);
        }

        public class AlreadyAddedException : Exception
        {
            public AlreadyAddedException(string iD) : base(string.Format(alreadyAddedExceptionMessage, iD)) { }
        }

        public class NotAddedException : Exception
        {
            public NotAddedException(string iD) : base(string.Format(notAddedExceptionMessage, iD)) { }
        }

        public class GetException : Exception
        {
            public GetException(string iD) : base(string.Format(getExceptionMessage, iD)) { }
        }
    }
}
