using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang.Parsing.Scoping
{
    internal class BlockScope : Scope
    {
        protected Dictionary<string, ParameterExpression> Variables { get; private set; }
        
        public BlockScope(Runtime runtime)
            : base(runtime)
        {
            Variables = new Dictionary<string, ParameterExpression>();
        }

        public override Expression this[string name]
        {
            get
            {
                ParameterExpression result;

                if (!Variables.TryGetValue(name, out result))
                {
                    result = null;
                }

                return result;
            }
        }

        public override Expression Pack(List<Expression> list)
        {
            return Expression.Block(Variables.Values, list);
        }

        public override Expression NewVariable(string name)
        {
            if (this[name] != null)
            {
                throw new Exception();
            }

            return Variables[name] = Expression.Variable(typeof(Any), name);
        }
    }
}
