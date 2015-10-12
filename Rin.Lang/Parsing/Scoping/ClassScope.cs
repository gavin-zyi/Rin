using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang.Parsing.Scoping
{
    class ClassScope : DynamicScope
    {
        private ParameterExpression args;
        private int parameterIndex;

        public ClassScope(Runtime runtime)
            : base(runtime)
        {
            args = Expression.Parameter(typeof(Args));
        }

        public override Expression Pack(List<Expression> exprs)
        {
            exprs.Insert(0, Expression.Assign(Storage, Expression.New(Class.Instance.New)));

            var returnTarget = Expression.Label(typeof(Any));
            exprs.Add(Expression.Return(returnTarget, Storage));

            var returnLabel = Expression.Label(returnTarget, Expression.Constant(Any.None));
            exprs.Add(returnLabel);

            var block = Expression.Block(new[] { Storage }, exprs);
            return Expression.New(Class.New, Expression.Lambda<Func<Args, Any>>(block, args));
        }


        public Expression LoadParameter()
        {
            return Expression.MakeIndex(args, Args.Indexer, new[] { Expression.Constant(parameterIndex++) });
        }
    }
}
