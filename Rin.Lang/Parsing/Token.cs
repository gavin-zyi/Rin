using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang.Parsing
{
    public class Token
    {
        public TokenType Type;
        public int Col;
        public int Line;
        
        public string Value;
        public int Length;
        public int Size;
    }
}
