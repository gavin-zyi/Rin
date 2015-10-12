using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rin.Lang.Parsing
{
    public enum TokenType
    {
        Eof,
        Var,
        Let,
        And,
        Or,
        As,
        Of,
        Func,
        Class,
        This,
        Return,
        Pass,
        If,
        For,
        While,
        Break,
        In,
        Elif,
        Else,
        None,
        True,
        False,
        Member,
        Number,
        String,
        Ws,
        Eol,
        Add,
        Sub,
        Mul,
        Div,
        LParen,
        RParen,
        LBrack,
        RBrack,
        Range,
        Dot,
        Comma,
        Colon,
        SemiColon,
        Equal,
        Less,
        Great,
        Indent,
        Dedent,
        Comment,
        Dummy,
    }
}
