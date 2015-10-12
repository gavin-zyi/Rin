using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dyn = System.Dynamic;

namespace Rin.Lang.Binders
{
    class InvokeBinder : Dyn.InvokeBinder
    {
        public InvokeBinder()
            : base(new Dyn.CallInfo(0))
        {

        }

        public override T BindDelegate<T>(CallSite<T> site, object[] args)
        {
            Func<CallSite, Any, Args, Any> func = (c, a, i) =>
                {
                    Any o;

                    if (!a.InvokeOperation(this, i, out o))
                    {
                        throw new Exception();
                    }

                    return o;
                };
            return (T)(object)func;
        }

        public override Dyn.DynamicMetaObject FallbackInvoke(Dyn.DynamicMetaObject target, Dyn.DynamicMetaObject[] args, Dyn.DynamicMetaObject errorSuggestion)
        {
            throw new NotImplementedException();
        }
    }
}
