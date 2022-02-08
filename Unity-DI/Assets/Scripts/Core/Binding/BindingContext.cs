using System;
using UnityEngine;

namespace SBaier.DI
{
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

    public class BindingContext<TContract> : BindingContext
    {
        public BindingContext(Binding binding, BindingStorage toContainerBinder) : base(binding, toContainerBinder) { }

		public ToBindingContext<TConcrete> To<TConcrete>() where TConcrete : TContract
        {
            return new ToBindingContext<TConcrete>(_binding);
        }

        public FromNewBindingContext<TConcrete> ToNew<TConcrete>() where TConcrete : TContract, new()
        {
            return new FromNewBindingContext<TConcrete>(_binding);
        }

        public ToComponentBindingContext<TConcrete> ToComponent<TConcrete>() where TConcrete : Component, TContract
        {
            return new ToComponentBindingContext<TConcrete>(_binding);
        }

        public BindingContext<TContract, TContract2> And<TContract2>(IComparable iD = default)
		{
            _toContainerBinder.Store<TContract2>(_binding, iD);
            return new BindingContext<TContract, TContract2>(_binding, _toContainerBinder);
        }
    }

    public class BindingContext<TContract1, TContract2> : BindingContext
	{
        public BindingContext(Binding binding, BindingStorage toContainerBinder) : base(binding, toContainerBinder) { }

        public ToBindingContext<TConcrete> To<TConcrete>() where TConcrete : TContract1, TContract2
        {
            return new ToBindingContext<TConcrete>(_binding);
        }

        public FromNewBindingContext<TConcrete> ToNew<TConcrete>() where TConcrete : TContract1, TContract2, new()
        {
            return new FromNewBindingContext<TConcrete>(_binding);
        }

        public ToComponentBindingContext<TConcrete> ToComponent<TConcrete>() where TConcrete : Component, TContract1, TContract2
        {
            return new ToComponentBindingContext<TConcrete>(_binding);
        }

        public BindingContext<TContract1, TContract2, TContract3> And<TContract3>(IComparable iD = default)
        {
            _toContainerBinder.Store<TContract3>(_binding, iD);
            return new BindingContext<TContract1, TContract2, TContract3>(_binding, _toContainerBinder);
        }
    }

    public class BindingContext<TContract1, TContract2, TContract3> : BindingContext
	{
        public BindingContext(Binding binding, BindingStorage toContainerBinder) : base(binding, toContainerBinder) { }

        public ToBindingContext<TConcrete> To<TConcrete>() where TConcrete : TContract1, TContract2, TContract3
        {
            return new ToBindingContext<TConcrete>(_binding);
        }

        public FromNewBindingContext<TConcrete> ToNew<TConcrete>() where TConcrete : TContract1, TContract2, TContract3, new()
        {
            return new FromNewBindingContext<TConcrete>(_binding);
        }

        public ToComponentBindingContext<TConcrete> ToComponent<TConcrete>() where TConcrete : Component, TContract1, TContract2, TContract3
        {
            return new ToComponentBindingContext<TConcrete>(_binding);
        }

        public BindingContext<TContract1, TContract2, TContract3, TContract4> And<TContract4>(IComparable iD = default)
        {
            _toContainerBinder.Store<TContract3>(_binding, iD);
            return new BindingContext<TContract1, TContract2, TContract3, TContract4>(_binding, _toContainerBinder);
        }
    }

    public class BindingContext<TContract1, TContract2, TContract3, TContract4> : BindingContext
	{
        public BindingContext(Binding binding, BindingStorage toContainerBinder) : base(binding, toContainerBinder) { }

        public ToBindingContext<TConcrete> To<TConcrete>() where TConcrete : TContract1, TContract2, TContract3, TContract4
        {
            return new ToBindingContext<TConcrete>(_binding);
        }

        public FromNewBindingContext<TConcrete> ToNew<TConcrete>() where TConcrete : TContract1, TContract2, TContract3, TContract4, new()
        {
            return new FromNewBindingContext<TConcrete>(_binding);
        }

        public ToComponentBindingContext<TConcrete> ToComponent<TConcrete>() where TConcrete : Component, TContract1, TContract2, TContract3, TContract4
        {
            return new ToComponentBindingContext<TConcrete>(_binding);
        }
    }
}