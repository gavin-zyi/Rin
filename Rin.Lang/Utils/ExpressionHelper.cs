using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang.Utils
{
    internal static class ExpressionHelper
    {
        public static Expression AsObjectType(this Expression parent)
        {
            if (!parent.Type.IsValueType)
            {
                return parent;
            }

            if (parent.Type == typeof(void))
            {
                return Expression.Block(parent, Expression.Default(typeof(object)));
            }

            return Expression.Convert(parent, typeof(object));
        }
    }
}
