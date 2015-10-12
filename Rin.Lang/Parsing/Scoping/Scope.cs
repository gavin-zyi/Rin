using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang.Parsing.Scoping
{
    internal abstract class Scope
    {
        protected Runtime Runtime { get; private set; }

        protected Scope(Runtime runtime)
        {
            Runtime = runtime;
        }

        public abstract Expression this[string name] { get; }

        public virtual Expression Pack(params Expression[] list)
        {
            return Pack(list.ToList());
        }

        public abstract Expression Pack(List<Expression> list);

        public abstract Expression NewVariable(string name);
    }
}
