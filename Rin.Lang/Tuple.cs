using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang
{
    public class Tuple : Iter
    {
        private readonly Any[] values;

        public Tuple(params Any[] values)
        {
            this.values = values;

            var i = 0;
            State = () => i < values.Length ? values[i++] : Any.None;
        }

        public Any this [int index]
        { 
            get
            {
                if (index >= values.Length)
                {
                    return Any.None;
                }

                return values[index];
            }
            set
            {
                if (index >= values.Length)
                {
                    throw new Exception();
                }

                values[index] = value;
            }
        }

        internal override bool GetIndexOperation(GetIndexBinder binder, Args indices, out Any result)
        {
            var idx = (int)((Number)indices[0]).Value;
            result = values[idx];
            return true;
        }

        static Tuple()
        {
            New = typeof(Tuple).GetConstructor(new Type[] { typeof(Any[]) });
        }

        public static ConstructorInfo New { get; private set; }
    }
}
