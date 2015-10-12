using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang.Parsing.Scoping
{
    internal class FunctionScope : BlockScope, IReturnScope
    {
        public LabelTarget ReturnTarget { get; private set; }

        private Dictionary<string, Expression> parameters;
        private int parameterIndex;

        private ParameterExpression args;

        public FunctionScope(Runtime runtime)
            : base(runtime)
        {
            ReturnTarget = Expression.Label(typeof(Any));

            parameters = new Dictionary<string, Expression>();
            parameterIndex = 0;

            args = Expression.Parameter(typeof(Args));
        }

        public override Expression this[string name]
        {
            get
            {
                Expression result;

                if (parameters.TryGetValue(name, out result))
                {
                    return result;
                }

                return base[name];
            }
        }

        public override Expression Pack(List<Expression> list)
        {
            var block = base.Pack(list);
            return Expression.Lambda(block, args);
        }
        
        public Expression NewParameter(string name)
        {
            if (this[name] != null)
            {
                throw new Exception();
            }

            var expr = Expression.MakeIndex(args, Args.Indexer, new[] { Expression.Constant(parameterIndex++) });

            return parameters[name] = expr; 
        }

    }
}
