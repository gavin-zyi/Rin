using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dyn = System.Dynamic;

namespace Rin.Lang.Binders
{
    internal class SetIndexBinder : Dyn.SetIndexBinder
    {
        public SetIndexBinder()
            : base(new Dyn.CallInfo(0))
        {
            
        }

        public override T BindDelegate<T>(CallSite<T> site, object[] args)
        {
            Func<CallSite, Any, Args, Any, Any> func = (c, any, idx, val) =>
            {
                if (!any.SetIndexOperation(this, idx, val))
                {
                    throw new Exception();
                }

                return val;
            };
            return (T)(object)func;
        }

        public override Dyn.DynamicMetaObject FallbackSetIndex(Dyn.DynamicMetaObject target, Dyn.DynamicMetaObject[] indexes, Dyn.DynamicMetaObject value, Dyn.DynamicMetaObject errorSuggestion)
        {
            throw new NotImplementedException();
        }
    }
}
