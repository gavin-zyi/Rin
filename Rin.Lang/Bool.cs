using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang
{
    public sealed class Bool : Any
    {
        public bool Value { get; set; }

        private Bool(bool value)
        {
            Value = value;
        }

        internal override bool UnaryOperation(UnaryOperationBinder binder, out Any result)
        {
            switch (binder.Operation)
            {
                case ExpressionType.IsTrue:
                    result = this == Bool.True ? Bool.True : Bool.False;
                    return true;
                case ExpressionType.IsFalse:
                    result = this == Bool.False ? Bool.True : Bool.False;
                    return true;
            }

            return base.UnaryOperation(binder, out result);
        }

        private static Bool GetInstance(bool value)
        {
            return value ? True : False;
        }

        static Bool()
        {
            True = new Bool(true);
            False = new Bool(false);
            Get = typeof(Bool).GetMethod("GetInstance", BindingFlags.NonPublic | BindingFlags.Static);
        }
                
        public static Bool True { get; private set; }
        public static Bool False { get; private set; }

        public static MethodInfo Get { get; private set; }

    }
}
