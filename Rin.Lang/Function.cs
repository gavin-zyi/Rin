using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang
{
    public class Function : Any
    {
        private readonly Func<Args, Any> signature;

        public Function(Action<Args> signature)
            : this(args => { signature(args); return Any.None; })
        {

        }

        public Function(Func<Args, Any> signature)
        {
            this.signature = signature;
        }

        public Any Call(Args args)
        {
            return signature(args);
        }

        internal override bool InvokeOperation(InvokeBinder binder, Args args, out Any result)
        {
            result = signature(args);
            return true;
        }

        static Function()
        {
            New = typeof(Function).GetConstructor(new[] { typeof(Func<Args, Any>) });
        }

        public static ConstructorInfo New { get; private set; }
    }
}
