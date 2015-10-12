using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang
{
    public class Args : Any, IEnumerable<Any>
    {
        private readonly Any[] args;

        public Args(params Any[] args)
        {
            this.args = args;
        }

        public Any this [int index]
        { 
            get
            {
                if (index >= args.Length)
                {
                    return Any.None;
                }

                return args[index];
            }
            set
            {
                if (index >= args.Length)
                {
                    throw new Exception();
                }

                args[index] = value;
            }
        }

        public int Length { get { return args.Length; } }

        static Args()
        {
            Empty = new Args();

            New = typeof(Args).GetConstructor(new Type[] { typeof(Any[]) });

            foreach (PropertyInfo pi in typeof(Args).GetProperties())
            {
                if (pi.GetIndexParameters().Length > 0)
                {
                    Indexer = pi;
                    break;
                }
            }
        }

        public IEnumerator<Any> GetEnumerator()
        {
            return args.Cast<Any>().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return args.GetEnumerator();
        }

        public static Args Empty { get; private set; }

        public static ConstructorInfo New { get; private set; }
        public static PropertyInfo Indexer { get; private set; }

    }
}
