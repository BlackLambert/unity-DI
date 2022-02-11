using System;
using UnityEngine;

namespace SBaier.DI
{

    public class BindingContext<C1> : BindingContext<C1, C1, C1, C1, C1, C1, C1, C1>
    {
        public BindingContext(Binding binding, BindingStorage toContainerBinder) : base(binding, toContainerBinder) { }

        public BindingContext<C1, TContract2> And<TContract2>(IComparable iD = default)
		{
            _toContainerBinder.Store<TContract2>(_binding, iD);
            return new BindingContext<C1, TContract2>(_binding, _toContainerBinder);
        }
    }

    public class BindingContext<C1, C2> : BindingContext<C1, C2, C2, C2, C2, C2, C2, C2>
    {
        public BindingContext(Binding binding, BindingStorage toContainerBinder) : base(binding, toContainerBinder) { }

        public BindingContext<C1, C2, TContract3> And<TContract3>(IComparable iD = default)
        {
            _toContainerBinder.Store<TContract3>(_binding, iD);
            return new BindingContext<C1, C2, TContract3>(_binding, _toContainerBinder);
        }
    }

    public class BindingContext<C1, C2, C3> : BindingContext<C1, C2, C3, C3, C3, C3, C3, C3>
    {
        public BindingContext(Binding binding, BindingStorage toContainerBinder) : base(binding, toContainerBinder) { }

        public BindingContext<C1, C2, C3, TContract4> And<TContract4>(IComparable iD = default)
        {
            _toContainerBinder.Store<TContract4>(_binding, iD);
            return new BindingContext<C1, C2, C3, TContract4>(_binding, _toContainerBinder);
        }
    }

    public class BindingContext<C1, C2, C3, C4> : BindingContext<C1, C2, C3, C4, C4, C4, C4, C4>
    {
        public BindingContext(Binding binding, BindingStorage toContainerBinder) : base(binding, toContainerBinder) { }
    }

    public class BindingContext<C1, C2, C3, C4, C5, C6, C7, C8> : BindingContext
	{
        public BindingContext(Binding binding, BindingStorage toContainerBinder) : base(binding, toContainerBinder) { }

        public ToBindingContext<TConcrete> To<TConcrete>() where TConcrete : C1, C2, C3, C4, C5, C6, C7, C8
        {
            return new ToBindingContext<TConcrete>(_binding);
        }

        public FromNewBindingContext<TConcrete> ToNew<TConcrete>() where TConcrete : C1, C2, C3, C4, C5, C6, C7, C8, new()
        {
            return new FromNewBindingContext<TConcrete>(_binding);
        }

        public ToComponentBindingContext<TConcrete> ToComponent<TConcrete>() where TConcrete : Component, C1, C2, C3, C4, C5, C6, C7, C8
        {
            return new ToComponentBindingContext<TConcrete>(_binding);
        }
    }

    public abstract class BindingContext
    {
        protected readonly Binding _binding;
        protected readonly BindingStorage _toContainerBinder;

        public BindingContext(Binding binding,
            BindingStorage toContainerBinder)
        {
            _binding = binding;
            _toContainerBinder = toContainerBinder;
        }
    }
}