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
    class BinaryOperationBinder : Dyn.BinaryOperationBinder
    {
        public BinaryOperationBinder(ExpressionType op)
            : base(op)
        {

        }


        public override T BindDelegate<T>(CallSite<T> site, object[] args)
        {
            Func<CallSite, Any, Any, Any> func = (cs, a, b) =>
                {
                    Any c;
                    if (!a.BinaryOperation(this, b, out c))
                    {
                        throw new Exception();
                    }

                    return c;
                };
            return (T)(object)func;
        }

        public override Dyn.DynamicMetaObject FallbackBinaryOperation(Dyn.DynamicMetaObject target, Dyn.DynamicMetaObject arg, Dyn.DynamicMetaObject errorSuggestion)
        {
            if (!target.HasValue || !arg.HasValue)
            {
                return Defer(target, arg);
            }

            var restrictions = target.Restrictions.Merge(arg.Restrictions)
                .Merge(Dyn.BindingRestrictions.GetTypeRestriction(target.Expression, target.LimitType))
                .Merge(Dyn.BindingRestrictions.GetTypeRestriction(arg.Expression, arg.LimitType));

            throw new NotImplementedException();
        }
    }
}
