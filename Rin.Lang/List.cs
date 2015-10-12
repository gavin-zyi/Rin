using System;
using System.Linq;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
namespace Rin.Lang
{
    public class List : Iter, IEnumerable<Any>
    {
        public List<Any> Value { get; private set; }

        public List()
        {
            Value = new List<Any>();

            var i = 0;
            State = () =>
                {
                    if (i < Value.Count)
                    {
                        return Value[i++];
                    }

                    i = 0;
                    return Any.None;
                };
        }



        public void Add(Any value)
        {
            Value.Add(value);
        }

        internal override bool GetIndexOperation(GetIndexBinder binder, Args indices, out Any result)
        {
            var idx = (int)((Number)indices[0]).Value;
            result = Value[idx];
            return true;
        }

        internal override bool SetIndexOperation(SetIndexBinder binder, Args indices, Any value)
        {
            var idx = (int)((Number)indices[0]).Value;
            Value[idx] = value;
            return true;
        }

        internal override bool GetMemberOperation(GetMemberBinder binder, out Any result)
        {
            Func<List, Any> member;

            if (!members.TryGetValue(binder.Name, out member))
            {
                return base.GetMemberOperation(binder, out result);
            }

            result = member(this);

            return true;
        }

        public IEnumerator<Any> GetEnumerator()
        {
            return Value.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Value.GetEnumerator();
        }

        static List()
        {
            members = new Dictionary<string, Func<List, Any>>
                {
                    {"length", obj => new Number(obj.Value.Count)},
                    {"add", obj => new Function(args => obj.Add(args[0]))},
                    {"sort", obj => new Function(args => 
                        {
                            var sorter = (Function)args[0];
                            //obj.Value.Sort((a, b) => (int) Math.Round((Number)sorter.Call(new Args(a, b))).Value));
                            obj.Value.Sort((a, b) => (int)(((Number)sorter.Call(new Args(a, b))).Value * 1000));
                        })}
                };

            New = typeof(List).GetConstructor(new Type[] { });
            Init = typeof(List).GetMethod("Add");
        }

        private static readonly Dictionary<string, Func<List, Any>> members;

        public static ConstructorInfo New { get; private set; }
        public static MethodInfo Init { get; private set; }

    }
}
