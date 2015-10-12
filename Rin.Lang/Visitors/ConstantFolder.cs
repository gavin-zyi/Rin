using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang.Visitors
{
    public class ConstantFolder : ExpressionVisitor
    {
        private readonly Runtime runtime;

        public ConstantFolder(Runtime runtime)
        {
            this.runtime = runtime;
        }

        public Expression<T> Modify<T>(Expression<T> expression)
        {
            return (Expression<T>) Visit(expression);
        }

        private bool GetConstant(Expression node, out double number)
        {
            number = 0;

            var expr = node as DynamicExpression;
            if (expr == null || !(expr.Binder is ConvertBinder))
            {
                return false;
            }

            var arg = (ConstantExpression) expr.Arguments[0];

            if (!arg.Type.IsValueType)
            {
                return false;
            }

            number = (double)arg.Value;

            return true;
        }

        protected override Expression VisitDynamic(DynamicExpression node)
        {
            var binOp = node.Binder as BinaryOperationBinder;
            if (binOp != null)
            {
                var type = binOp.Operation;
                var args = node.Arguments;

                double a, b;
                if (GetConstant(args[0], out a) && GetConstant(args[1], out b))
                {
                    return Expression.Dynamic(runtime.ConvertCallSite, typeof(Any), Expression.Constant(a + b, typeof(double)));
                }                      
            }

            return base.VisitDynamic(node);
        }
    }
}
