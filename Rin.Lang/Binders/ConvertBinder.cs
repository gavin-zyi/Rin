using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Dyn = System.Dynamic;

namespace Rin.Lang.Binders
{
    internal class ConvertBinder : Dyn.ConvertBinder
    {
        private Dictionary<Type, Func<Expression, Expression>> converters;

        public ConvertBinder()
            : base(typeof(Any), false)
        {
            converters = new Dictionary<Type, Func<Expression, Expression>>
                    {
                        {typeof(bool), value => Expression.Call(Bool.Get, value)},
                        {typeof(double), value => Expression.New(Number.New, value)},
                        {typeof(Func<Args, Any>), value => Expression.New(Function.New, value)},
                        {typeof(Func<Args, Class>), value => Expression.New(Function.New, value)}
                    };

        }

        public override Dyn.DynamicMetaObject FallbackConvert(Dyn.DynamicMetaObject target, Dyn.DynamicMetaObject errorSuggestion)
        {
            if (!target.HasValue)
            {
                return Defer(target);
            }

            if (target.LimitType.IsSubclassOf(typeof(Any)))
            {
                return new Dyn.DynamicMetaObject(Expression.Convert(target.Expression, typeof(Any)), target.Restrictions.Merge(Dyn.BindingRestrictions.GetTypeRestriction(target.Expression, target.LimitType)));
            }

            if (target.Value == null)
            {
                return new Dyn.DynamicMetaObject(Expression.Constant(Any.None, typeof(Any)), Dyn.BindingRestrictions.GetInstanceRestriction(target.Expression, null));
            }

            var targetExpression = target.Expression;
            if (targetExpression.Type != target.LimitType)
            {
                targetExpression = Expression.Convert(targetExpression, target.LimitType);
            }

            var restrictions = target.Restrictions
                .Merge(Dyn.BindingRestrictions.GetTypeRestriction(target.Expression, target.LimitType));

            var converter = converters[target.LimitType];

            return new Dyn.DynamicMetaObject(converter(targetExpression), restrictions);
        }
    }
}
