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
    public class String : Any
    {
        public string Value { get; set; }

        public String(string value)
        {
            Value = value;
        }

        internal override bool UnaryOperation(UnaryOperationBinder binder, out Any result)
        {
            switch (binder.Operation)
            {
                case ExpressionType.IsTrue:
                    result = Value.Length > 0 ? Bool.True : Bool.False;
                    return true;
            }

            return base.UnaryOperation(binder, out result);
        }

        internal override bool BinaryOperation(BinaryOperationBinder binder, Any arg, out Any result)
        {
            var str = arg as String;
            if (str != null)
            {
                switch (binder.Operation)
                {
                    case ExpressionType.Add:
                        result = new String(Value + str.Value);
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

        static String()
        {
            New = typeof(String).GetConstructor(new[] { typeof(string) });
        }

        public static ConstructorInfo New { get; private set; }
    }
}
