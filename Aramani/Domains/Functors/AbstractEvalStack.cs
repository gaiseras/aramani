﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Cecil;
using Mono.Cecil.Cil;


namespace Aramani.Domains
{

    /// <summary><
    /// Models an eval stack of a method frame.
    /// The top of the stack is always at index 0; this complicates push and pop,
    /// but facilitates the union and join operations (which are the same as 
    /// for ordinary elements of the tuple domain).
    /// </summary>
    /// <typeparam name="C"></typeparam>
    class AbstractEvalStack<C> : AbstractTuple<C>, IDomainElement<AbstractEvalStack<C>>, 
        AbstractMachine.IEvalStack<C>
        where C: class, IDomainElement<C>, new()
    {

        public AbstractEvalStack()
            : base(1)
        {
        }

        public AbstractEvalStack(int size)
            : base(size)
        {
        }

        public void Push(C element)
        {
            C buffer = (C)element.Clone();
            for (int i = 0; i < this.Arity; i++)
            {
                var temp = this[i];
                this[i] = buffer;
                buffer = temp;
            }
        }

        public void PushUnknownElement()
        {
            C topElement = new C();
            topElement.ToTopElement();
            Push(topElement);
        }

        public C Top()
        {
            return this[0];
        }

        public C Pop()
        {
            var result = this[0];
            C buffer = new C(); // !!!
            buffer.ToTopElement();
            for (int i = this.Arity - 1; i >= 0; i--)
            {
                var temp = this[i];
                this[i] = buffer;
                buffer = temp;
            }
            return result;
        }

        public override object Clone()
        {
            var result = new AbstractEvalStack<C>(Arity);
            for (int i = 0; i < Arity; i++)
            {
                result[i] = this[i].Clone() as C;
            }
            return result;
        }

        public override void ToTopElement()
        {
            base.ToTopElement();
        }

        public virtual void ToUnknown()
        {
            ToTopElement();
        }

        public void UnionWith(AbstractEvalStack<C> element)
        {
            base.UnionWith(element);
        }

        public void JoinWith(AbstractEvalStack<C> element)
        {
            base.JoinWith(element);
        }

        public void WidenWith(AbstractEvalStack<C> element)
        {
            base.WidenWith(element);
        }

        public bool IsSubsetOrEqual(AbstractEvalStack<C> element)
        {
            return base.IsSubsetOrEqual(element);
        }

    }
}
