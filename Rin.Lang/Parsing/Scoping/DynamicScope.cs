using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang.Parsing.Scoping
{
    internal class DynamicScope : Scope
    {
        protected ParameterExpression Storage { get; private set; }
        protected Dictionary<string, Expression> Variables { get; private set; }

        public DynamicScope(Runtime runtime)
            : base(runtime)
        {
            Storage = Expression.Variable(typeof(Any));
            Variables = new Dictionary<string, Expression>();
        }

        public override Expression this[string name]
        {
            get
            {
                Expression result;

                if (!Variables.TryGetValue(name, out result))
                {
                    result = null;
                }

                return result;
            }
        }

        public override Expression Pack(List<Expression> list)
        {
            throw new NotImplementedException();
        }

        public override Expression NewVariable(string name)
        {
            if (this[name] != null)
            {
                throw new Exception();
            }

            return Variables[name] = Expression.Dynamic(Runtime.GetMemberCallSite(name), typeof(Any), Storage);
        }
    }
}
