using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Dyn = System.Dynamic;

namespace Rin.Lang.Binders
{
    internal class GetIndexBinder : Dyn.GetIndexBinder
    {
        public GetIndexBinder()
            : base(new Dyn.CallInfo(0))
        {

        }
        public override T BindDelegate<T>(System.Runtime.CompilerServices.CallSite<T> site, object[] args)
        {
            Func<CallSite, Any, Args, Any> func = (c, any, idx) =>
            {
                Any val;
                if (!any.GetIndexOperation(this, idx, out val))
                {
                    throw new Exception();
                }

                return val;
            };
            return (T)(object)func;
        }

        public override Dyn.DynamicMetaObject FallbackGetIndex(Dyn.DynamicMetaObject target, Dyn.DynamicMetaObject[] indexes, Dyn.DynamicMetaObject errorSuggestion)
        {
            throw new NotImplementedException();
        }
    }
}
