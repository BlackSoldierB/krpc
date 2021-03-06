namespace KRPC.Continuations
{
/*[[[cog
import cog
def wrap(s, x, e): return s + x + e if x != '' else ''
def prepend (s, x): return s + x if x != '' else ''

for n in range(int(nargs)+1):
    cog.outl("""
    ///<summary>
    ///A continuation wrapping a function that takes """ + str(n) + """ arguments and returns a result.
    ///</summary>
    public sealed class ParameterizedContinuation<""" + ','.join(['TReturn'] + ['TArg%d' % i for i in range(n)]) + """> : Continuation<TReturn> {
        public delegate TReturn Fn (""" + ', '.join(['TArg%d arg%d' % (i,i) for i in range(n)]) + """);
        Fn fn;""" + prepend(' ', ' '.join(['TArg%d arg%d;' % (i,i) for i in range(n)])) + """
        public ParameterizedContinuation (Fn fn""" + prepend(', ', ', '.join(['TArg%d arg%d' % (i,i) for i in range(n)])) + ') { ' + \
            'this.fn = fn;' + prepend(' ', ' '.join(['this.arg%d = arg%d;' % (i,i) for i in range(n)])) + """ }
        public override TReturn Run () { return fn (""" + ', '.join(['arg%d' % i for i in range(n)]) + """); }
    }""")

    cog.outl("""
    ///<summary>
    ///A continuation wrapping a function that takes """ + str(n) + """ arguments, but does not return a result.
    ///</summary>
    public sealed class ParameterizedContinuationVoid""" + wrap('<', ','.join(['TArg%d' % i for i in range(n)]), '>') + """ : Continuation {
        public delegate void Fn (""" + ', '.join(['TArg%d arg%d' % (i,i) for i in range(n)]) + """);
        Fn fn;""" + prepend(' ', ' '.join(['TArg%d arg%d;' % (i,i) for i in range(n)])) + """
        public ParameterizedContinuationVoid (Fn fn""" + prepend(', ', ', '.join(['TArg%d arg%d' % (i,i) for i in range(n)])) + ') { ' + \
            'this.fn = fn;' + prepend(' ', ' '.join(['this.arg%d = arg%d;' % (i,i) for i in range(n)])) + """ }
        public override void Run () { fn (""" + ', '.join(['arg%d' % i for i in range(n)]) + """); }
    }""")
]]]*/

///<summary>
///A continuation wrapping a function that takes 0 arguments and returns a result.
///</summary>
public sealed class ParameterizedContinuation<TReturn> : Continuation<TReturn> {
    public delegate TReturn Fn ();
    Fn fn;
    public ParameterizedContinuation (Fn fn) { this.fn = fn; }
    public override TReturn Run () { return fn (); }
}

///<summary>
///A continuation wrapping a function that takes 0 arguments, but does not return a result.
///</summary>
public sealed class ParameterizedContinuationVoid : Continuation {
    public delegate void Fn ();
    Fn fn;
    public ParameterizedContinuationVoid (Fn fn) { this.fn = fn; }
    public override void Run () { fn (); }
}

///<summary>
///A continuation wrapping a function that takes 1 arguments and returns a result.
///</summary>
public sealed class ParameterizedContinuation<TReturn,TArg0> : Continuation<TReturn> {
    public delegate TReturn Fn (TArg0 arg0);
    Fn fn; TArg0 arg0;
    public ParameterizedContinuation (Fn fn, TArg0 arg0) { this.fn = fn; this.arg0 = arg0; }
    public override TReturn Run () { return fn (arg0); }
}

///<summary>
///A continuation wrapping a function that takes 1 arguments, but does not return a result.
///</summary>
public sealed class ParameterizedContinuationVoid<TArg0> : Continuation {
    public delegate void Fn (TArg0 arg0);
    Fn fn; TArg0 arg0;
    public ParameterizedContinuationVoid (Fn fn, TArg0 arg0) { this.fn = fn; this.arg0 = arg0; }
    public override void Run () { fn (arg0); }
}

///<summary>
///A continuation wrapping a function that takes 2 arguments and returns a result.
///</summary>
public sealed class ParameterizedContinuation<TReturn,TArg0,TArg1> : Continuation<TReturn> {
    public delegate TReturn Fn (TArg0 arg0, TArg1 arg1);
    Fn fn; TArg0 arg0; TArg1 arg1;
    public ParameterizedContinuation (Fn fn, TArg0 arg0, TArg1 arg1) { this.fn = fn; this.arg0 = arg0; this.arg1 = arg1; }
    public override TReturn Run () { return fn (arg0, arg1); }
}

///<summary>
///A continuation wrapping a function that takes 2 arguments, but does not return a result.
///</summary>
public sealed class ParameterizedContinuationVoid<TArg0,TArg1> : Continuation {
    public delegate void Fn (TArg0 arg0, TArg1 arg1);
    Fn fn; TArg0 arg0; TArg1 arg1;
    public ParameterizedContinuationVoid (Fn fn, TArg0 arg0, TArg1 arg1) { this.fn = fn; this.arg0 = arg0; this.arg1 = arg1; }
    public override void Run () { fn (arg0, arg1); }
}

///<summary>
///A continuation wrapping a function that takes 3 arguments and returns a result.
///</summary>
public sealed class ParameterizedContinuation<TReturn,TArg0,TArg1,TArg2> : Continuation<TReturn> {
    public delegate TReturn Fn (TArg0 arg0, TArg1 arg1, TArg2 arg2);
    Fn fn; TArg0 arg0; TArg1 arg1; TArg2 arg2;
    public ParameterizedContinuation (Fn fn, TArg0 arg0, TArg1 arg1, TArg2 arg2) { this.fn = fn; this.arg0 = arg0; this.arg1 = arg1; this.arg2 = arg2; }
    public override TReturn Run () { return fn (arg0, arg1, arg2); }
}

///<summary>
///A continuation wrapping a function that takes 3 arguments, but does not return a result.
///</summary>
public sealed class ParameterizedContinuationVoid<TArg0,TArg1,TArg2> : Continuation {
    public delegate void Fn (TArg0 arg0, TArg1 arg1, TArg2 arg2);
    Fn fn; TArg0 arg0; TArg1 arg1; TArg2 arg2;
    public ParameterizedContinuationVoid (Fn fn, TArg0 arg0, TArg1 arg1, TArg2 arg2) { this.fn = fn; this.arg0 = arg0; this.arg1 = arg1; this.arg2 = arg2; }
    public override void Run () { fn (arg0, arg1, arg2); }
}

///<summary>
///A continuation wrapping a function that takes 4 arguments and returns a result.
///</summary>
public sealed class ParameterizedContinuation<TReturn,TArg0,TArg1,TArg2,TArg3> : Continuation<TReturn> {
    public delegate TReturn Fn (TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3);
    Fn fn; TArg0 arg0; TArg1 arg1; TArg2 arg2; TArg3 arg3;
    public ParameterizedContinuation (Fn fn, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3) { this.fn = fn; this.arg0 = arg0; this.arg1 = arg1; this.arg2 = arg2; this.arg3 = arg3; }
    public override TReturn Run () { return fn (arg0, arg1, arg2, arg3); }
}

///<summary>
///A continuation wrapping a function that takes 4 arguments, but does not return a result.
///</summary>
public sealed class ParameterizedContinuationVoid<TArg0,TArg1,TArg2,TArg3> : Continuation {
    public delegate void Fn (TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3);
    Fn fn; TArg0 arg0; TArg1 arg1; TArg2 arg2; TArg3 arg3;
    public ParameterizedContinuationVoid (Fn fn, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3) { this.fn = fn; this.arg0 = arg0; this.arg1 = arg1; this.arg2 = arg2; this.arg3 = arg3; }
    public override void Run () { fn (arg0, arg1, arg2, arg3); }
}

///<summary>
///A continuation wrapping a function that takes 5 arguments and returns a result.
///</summary>
public sealed class ParameterizedContinuation<TReturn,TArg0,TArg1,TArg2,TArg3,TArg4> : Continuation<TReturn> {
    public delegate TReturn Fn (TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4);
    Fn fn; TArg0 arg0; TArg1 arg1; TArg2 arg2; TArg3 arg3; TArg4 arg4;
    public ParameterizedContinuation (Fn fn, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4) { this.fn = fn; this.arg0 = arg0; this.arg1 = arg1; this.arg2 = arg2; this.arg3 = arg3; this.arg4 = arg4; }
    public override TReturn Run () { return fn (arg0, arg1, arg2, arg3, arg4); }
}

///<summary>
///A continuation wrapping a function that takes 5 arguments, but does not return a result.
///</summary>
public sealed class ParameterizedContinuationVoid<TArg0,TArg1,TArg2,TArg3,TArg4> : Continuation {
    public delegate void Fn (TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4);
    Fn fn; TArg0 arg0; TArg1 arg1; TArg2 arg2; TArg3 arg3; TArg4 arg4;
    public ParameterizedContinuationVoid (Fn fn, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4) { this.fn = fn; this.arg0 = arg0; this.arg1 = arg1; this.arg2 = arg2; this.arg3 = arg3; this.arg4 = arg4; }
    public override void Run () { fn (arg0, arg1, arg2, arg3, arg4); }
}

///<summary>
///A continuation wrapping a function that takes 6 arguments and returns a result.
///</summary>
public sealed class ParameterizedContinuation<TReturn,TArg0,TArg1,TArg2,TArg3,TArg4,TArg5> : Continuation<TReturn> {
    public delegate TReturn Fn (TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5);
    Fn fn; TArg0 arg0; TArg1 arg1; TArg2 arg2; TArg3 arg3; TArg4 arg4; TArg5 arg5;
    public ParameterizedContinuation (Fn fn, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5) { this.fn = fn; this.arg0 = arg0; this.arg1 = arg1; this.arg2 = arg2; this.arg3 = arg3; this.arg4 = arg4; this.arg5 = arg5; }
    public override TReturn Run () { return fn (arg0, arg1, arg2, arg3, arg4, arg5); }
}

///<summary>
///A continuation wrapping a function that takes 6 arguments, but does not return a result.
///</summary>
public sealed class ParameterizedContinuationVoid<TArg0,TArg1,TArg2,TArg3,TArg4,TArg5> : Continuation {
    public delegate void Fn (TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5);
    Fn fn; TArg0 arg0; TArg1 arg1; TArg2 arg2; TArg3 arg3; TArg4 arg4; TArg5 arg5;
    public ParameterizedContinuationVoid (Fn fn, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5) { this.fn = fn; this.arg0 = arg0; this.arg1 = arg1; this.arg2 = arg2; this.arg3 = arg3; this.arg4 = arg4; this.arg5 = arg5; }
    public override void Run () { fn (arg0, arg1, arg2, arg3, arg4, arg5); }
}

///<summary>
///A continuation wrapping a function that takes 7 arguments and returns a result.
///</summary>
public sealed class ParameterizedContinuation<TReturn,TArg0,TArg1,TArg2,TArg3,TArg4,TArg5,TArg6> : Continuation<TReturn> {
    public delegate TReturn Fn (TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6);
    Fn fn; TArg0 arg0; TArg1 arg1; TArg2 arg2; TArg3 arg3; TArg4 arg4; TArg5 arg5; TArg6 arg6;
    public ParameterizedContinuation (Fn fn, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6) { this.fn = fn; this.arg0 = arg0; this.arg1 = arg1; this.arg2 = arg2; this.arg3 = arg3; this.arg4 = arg4; this.arg5 = arg5; this.arg6 = arg6; }
    public override TReturn Run () { return fn (arg0, arg1, arg2, arg3, arg4, arg5, arg6); }
}

///<summary>
///A continuation wrapping a function that takes 7 arguments, but does not return a result.
///</summary>
public sealed class ParameterizedContinuationVoid<TArg0,TArg1,TArg2,TArg3,TArg4,TArg5,TArg6> : Continuation {
    public delegate void Fn (TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6);
    Fn fn; TArg0 arg0; TArg1 arg1; TArg2 arg2; TArg3 arg3; TArg4 arg4; TArg5 arg5; TArg6 arg6;
    public ParameterizedContinuationVoid (Fn fn, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6) { this.fn = fn; this.arg0 = arg0; this.arg1 = arg1; this.arg2 = arg2; this.arg3 = arg3; this.arg4 = arg4; this.arg5 = arg5; this.arg6 = arg6; }
    public override void Run () { fn (arg0, arg1, arg2, arg3, arg4, arg5, arg6); }
}

///<summary>
///A continuation wrapping a function that takes 8 arguments and returns a result.
///</summary>
public sealed class ParameterizedContinuation<TReturn,TArg0,TArg1,TArg2,TArg3,TArg4,TArg5,TArg6,TArg7> : Continuation<TReturn> {
    public delegate TReturn Fn (TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7);
    Fn fn; TArg0 arg0; TArg1 arg1; TArg2 arg2; TArg3 arg3; TArg4 arg4; TArg5 arg5; TArg6 arg6; TArg7 arg7;
    public ParameterizedContinuation (Fn fn, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7) { this.fn = fn; this.arg0 = arg0; this.arg1 = arg1; this.arg2 = arg2; this.arg3 = arg3; this.arg4 = arg4; this.arg5 = arg5; this.arg6 = arg6; this.arg7 = arg7; }
    public override TReturn Run () { return fn (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7); }
}

///<summary>
///A continuation wrapping a function that takes 8 arguments, but does not return a result.
///</summary>
public sealed class ParameterizedContinuationVoid<TArg0,TArg1,TArg2,TArg3,TArg4,TArg5,TArg6,TArg7> : Continuation {
    public delegate void Fn (TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7);
    Fn fn; TArg0 arg0; TArg1 arg1; TArg2 arg2; TArg3 arg3; TArg4 arg4; TArg5 arg5; TArg6 arg6; TArg7 arg7;
    public ParameterizedContinuationVoid (Fn fn, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7) { this.fn = fn; this.arg0 = arg0; this.arg1 = arg1; this.arg2 = arg2; this.arg3 = arg3; this.arg4 = arg4; this.arg5 = arg5; this.arg6 = arg6; this.arg7 = arg7; }
    public override void Run () { fn (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7); }
}

///<summary>
///A continuation wrapping a function that takes 9 arguments and returns a result.
///</summary>
public sealed class ParameterizedContinuation<TReturn,TArg0,TArg1,TArg2,TArg3,TArg4,TArg5,TArg6,TArg7,TArg8> : Continuation<TReturn> {
    public delegate TReturn Fn (TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8);
    Fn fn; TArg0 arg0; TArg1 arg1; TArg2 arg2; TArg3 arg3; TArg4 arg4; TArg5 arg5; TArg6 arg6; TArg7 arg7; TArg8 arg8;
    public ParameterizedContinuation (Fn fn, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8) { this.fn = fn; this.arg0 = arg0; this.arg1 = arg1; this.arg2 = arg2; this.arg3 = arg3; this.arg4 = arg4; this.arg5 = arg5; this.arg6 = arg6; this.arg7 = arg7; this.arg8 = arg8; }
    public override TReturn Run () { return fn (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8); }
}

///<summary>
///A continuation wrapping a function that takes 9 arguments, but does not return a result.
///</summary>
public sealed class ParameterizedContinuationVoid<TArg0,TArg1,TArg2,TArg3,TArg4,TArg5,TArg6,TArg7,TArg8> : Continuation {
    public delegate void Fn (TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8);
    Fn fn; TArg0 arg0; TArg1 arg1; TArg2 arg2; TArg3 arg3; TArg4 arg4; TArg5 arg5; TArg6 arg6; TArg7 arg7; TArg8 arg8;
    public ParameterizedContinuationVoid (Fn fn, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8) { this.fn = fn; this.arg0 = arg0; this.arg1 = arg1; this.arg2 = arg2; this.arg3 = arg3; this.arg4 = arg4; this.arg5 = arg5; this.arg6 = arg6; this.arg7 = arg7; this.arg8 = arg8; }
    public override void Run () { fn (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8); }
}

///<summary>
///A continuation wrapping a function that takes 10 arguments and returns a result.
///</summary>
public sealed class ParameterizedContinuation<TReturn,TArg0,TArg1,TArg2,TArg3,TArg4,TArg5,TArg6,TArg7,TArg8,TArg9> : Continuation<TReturn> {
    public delegate TReturn Fn (TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9);
    Fn fn; TArg0 arg0; TArg1 arg1; TArg2 arg2; TArg3 arg3; TArg4 arg4; TArg5 arg5; TArg6 arg6; TArg7 arg7; TArg8 arg8; TArg9 arg9;
    public ParameterizedContinuation (Fn fn, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9) { this.fn = fn; this.arg0 = arg0; this.arg1 = arg1; this.arg2 = arg2; this.arg3 = arg3; this.arg4 = arg4; this.arg5 = arg5; this.arg6 = arg6; this.arg7 = arg7; this.arg8 = arg8; this.arg9 = arg9; }
    public override TReturn Run () { return fn (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9); }
}

///<summary>
///A continuation wrapping a function that takes 10 arguments, but does not return a result.
///</summary>
public sealed class ParameterizedContinuationVoid<TArg0,TArg1,TArg2,TArg3,TArg4,TArg5,TArg6,TArg7,TArg8,TArg9> : Continuation {
    public delegate void Fn (TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9);
    Fn fn; TArg0 arg0; TArg1 arg1; TArg2 arg2; TArg3 arg3; TArg4 arg4; TArg5 arg5; TArg6 arg6; TArg7 arg7; TArg8 arg8; TArg9 arg9;
    public ParameterizedContinuationVoid (Fn fn, TArg0 arg0, TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7, TArg8 arg8, TArg9 arg9) { this.fn = fn; this.arg0 = arg0; this.arg1 = arg1; this.arg2 = arg2; this.arg3 = arg3; this.arg4 = arg4; this.arg5 = arg5; this.arg6 = arg6; this.arg7 = arg7; this.arg8 = arg8; this.arg9 = arg9; }
    public override void Run () { fn (arg0, arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9); }
}
//[[[end]]]
}