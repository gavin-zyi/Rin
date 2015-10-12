using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang.Parsing.Scoping
{
    internal class ModuleScope : DynamicScope
    {
        public ModuleScope(Runtime runtime)
            : base(runtime)
        {
        }

        public override Expression Pack(List<Expression> exprs)
        {
            exprs.Insert(0, Expression.Assign(Storage, Expression.New(Module.New)));

            var returnTarget = Expression.Label(typeof(Any));
            exprs.Add(Expression.Return(returnTarget, Storage));

            var returnLabel = Expression.Label(returnTarget, Expression.Constant(Any.None));
            exprs.Add(returnLabel);

            var block = Expression.Block(new[] { Storage }, exprs);
            return Expression.Lambda<Func<Any>>(block);
        }
    }
}
