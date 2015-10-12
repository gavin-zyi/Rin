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
    public abstract class Any
    {
        internal virtual bool UnaryOperation(UnaryOperationBinder binder, out Any result)
        {
            result = null;
            return false;
        }

        internal virtual bool BinaryOperation(BinaryOperationBinder binder, Any arg, out Any result)
        {
            result = null;
            return false;
        }

        internal virtual bool GetIndexOperation(GetIndexBinder binder, Args indices, out Any result)
        {
            result = null;
            return false;
        }

        internal virtual bool SetIndexOperation(SetIndexBinder binder, Args indices, Any value)
        {   
            return false;
        }

        internal virtual bool GetMemberOperation(GetMemberBinder binder, out Any result)
        {
            result = null;
            return false;
        }

        internal virtual bool SetMemberOperation(SetMemberBinder binder, Any value)
        {
            return false;
        }

        internal virtual bool InvokeOperation(InvokeBinder binder, Args args, out Any result)
        {
            result = null;
            return false;
        }
                
        public static None None { get; private set; }

        static Any()
        {
            None = new None();
        }
    }
}
