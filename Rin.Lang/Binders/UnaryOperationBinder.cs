using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dyn = System.Dynamic;

namespace Rin.Lang.Binders
{
    class UnaryOperationBinder : Dyn.UnaryOperationBinder
    {
        public UnaryOperationBinder(ExpressionType op)
            : base(op)
        {

        }

        public override T BindDelegate<T>(CallSite<T> site, object[] args)
        {
            Func<CallSite, Any, Any> func = (cs, a) =>
                {
                    Any b;
                    if (!a.UnaryOperation(this, out b))
                    {
                        throw new Exception();
                    }

                    return b;
                };
            return (T)(object)func;
        }

        public override Dyn.DynamicMetaObject FallbackUnaryOperation(Dyn.DynamicMetaObject target, Dyn.DynamicMetaObject errorSuggestion)
        {
            if (!target.HasValue)
            {
                return Defer(target);
            }

            throw new NotImplementedException();
        }
    }
}
