using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang
{
    public sealed class Number : Any
    {
        public double Value { get; set; }

        public Number(double value)
        {
            Value = value;
        }

        internal override bool UnaryOperation(UnaryOperationBinder binder, out Any result)
        {
            switch (binder.Operation)
            {
                case ExpressionType.IsTrue:
                    result = ((int)Value) != 0 ? Bool.True : Bool.False;
                    return true;
                case ExpressionType.Negate:
                    result = new Number(-Value);
                    return true;
            }

            return base.UnaryOperation(binder, out result);
        }

        internal override bool BinaryOperation(BinaryOperationBinder binder, Any arg, out Any result)
        {
            var number = arg as Number;
            if (number != null)
            {
                switch (binder.Operation)
                {
                    case ExpressionType.Add:
                        result = new Number(Value + number.Value);
                        break;
                    case ExpressionType.Subtract:
                        result = new Number(Value - number.Value);
                        break;
                    case ExpressionType.Multiply:
                        result = new Number(Value * number.Value);
                        break;
                    case ExpressionType.Divide:
                        result = new Number(Value / number.Value);
                        break;
                    case ExpressionType.LessThan:
                        result = Value < number.Value ? Bool.True : Bool.False;
                        break;
                    case ExpressionType.LessThanOrEqual:
                        result = Value <= number.Value ? Bool.True : Bool.False;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                return true;
            }

            return base.BinaryOperation(binder, arg, out result);
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        static Number()
        {
            New = typeof(Number).GetConstructor(new[] { typeof(double) });
        }

        public static ConstructorInfo New { get; private set; }
    }
}
