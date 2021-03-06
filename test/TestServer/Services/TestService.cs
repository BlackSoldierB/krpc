using System;
using System.Linq;
using System.Collections.Generic;
using KRPC.Service.Attributes;
using KRPC.Continuations;

namespace TestServer.Services
{
    [KRPCService]
    public static class TestService
    {
        [KRPCProcedure]
        public static string FloatToString (float value)
        {
            return value.ToString ();
        }

        [KRPCProcedure]
        public static string DoubleToString (double value)
        {
            return value.ToString ();
        }

        [KRPCProcedure]
        public static string Int32ToString (int value)
        {
            return value.ToString ();
        }

        [KRPCProcedure]
        public static string Int64ToString (long value)
        {
            return value.ToString ();
        }

        [KRPCProcedure]
        public static string BoolToString (bool value)
        {
            return value.ToString ();
        }

        [KRPCProcedure]
        public static int StringToInt32 (string value)
        {
            return Convert.ToInt32 (value);
        }

        [KRPCProcedure]
        public static string BytesToHexString (byte[] value)
        {

            return BitConverter.ToString (value).Replace ("-", "").ToLower ();
        }

        [KRPCProcedure]
        public static string AddMultipleValues (float x, int y, long z)
        {
            return (x + y + z).ToString ();
        }

        [KRPCProperty]
        public static string StringProperty { get; set; }

        [KRPCProperty]
        public static string StringPropertyPrivateGet { private get; set; }

        static string stringPropertyPrivateSet = "foo";

        [KRPCProperty]
        public static string StringPropertyPrivateSet {
            get { return stringPropertyPrivateSet; }
            private set { stringPropertyPrivateSet = value; }
        }

        [KRPCProcedure]
        public static TestClass CreateTestObject (string value)
        {
            return new TestClass (value);
        }

        [KRPCProcedure]
        public static TestClass EchoTestObject (TestClass value)
        {
            return value;
        }

        [KRPCProperty]
        public static TestClass ObjectProperty { get; set; }

        [KRPCClass]
        public class TestClass : KRPC.Utils.Equatable<TestClass>
        {
            string value;

            public TestClass (string value)
            {
                this.value = value;
            }

            [KRPCMethod]
            public string GetValue ()
            {
                return "value=" + value;
            }

            [KRPCMethod]
            public string FloatToString (float x)
            {
                return value + x.ToString ();
            }

            [KRPCMethod]
            public string ObjectToString (TestClass other)
            {
                return value + (other == null ? "null" : other.value);
            }

            [KRPCProperty]
            public int IntProperty { get; set; }

            [KRPCProperty]
            public TestClass ObjectProperty { get; set; }

            [KRPCMethod]
            public static string OptionalArguments (string x, string y = "foo", string z = "bar", string anotherParameter = "baz")
            {
                return x + y + z + anotherParameter;
            }

            public override bool Equals (TestClass other)
            {
                return value == other.value;
            }

            public override int GetHashCode ()
            {
                return value.GetHashCode ();
            }
        }

        [KRPCProcedure]
        public static string OptionalArguments (string x, string y = "foo", string z = "bar", string anotherParameter = "baz")
        {
            return x + y + z + anotherParameter;
        }

        [KRPCProcedure]
        public static KRPC.Schema.Test.TestEnum EnumReturn ()
        {
            return KRPC.Schema.Test.TestEnum.a;
        }

        [KRPCProcedure]
        public static KRPC.Schema.Test.TestEnum EnumEcho (KRPC.Schema.Test.TestEnum x)
        {
            return x;
        }

        [KRPCProcedure]
        public static KRPC.Schema.Test.TestEnum EnumDefaultArg (KRPC.Schema.Test.TestEnum x = KRPC.Schema.Test.TestEnum.c)
        {
            return x;
        }

        [KRPCEnum]
        public enum CSharpEnum
        {
            ValueA,
            ValueB,
            ValueC}
        ;

        [KRPCProcedure]
        public static CSharpEnum CSharpEnumReturn ()
        {
            return CSharpEnum.ValueB;
        }

        [KRPCProcedure]
        public static CSharpEnum CSharpEnumEcho (CSharpEnum x)
        {
            return x;
        }

        [KRPCProcedure]
        public static CSharpEnum CSharpEnumDefaultArg (CSharpEnum x = CSharpEnum.ValueC)
        {
            return x;
        }

        [KRPCProcedure]
        public static int BlockingProcedure (int n, int sum = 0)
        {
            if (n == 0)
                return sum;
            throw new YieldException (new ParameterizedContinuation<int,int,int> (BlockingProcedure, n - 1, sum + n));
        }

        [KRPCProcedure]
        public static IList<int> IncrementList (IList<int> l)
        {
            return l.Select (x => x + 1).ToList ();
        }

        [KRPCProcedure]
        public static IDictionary<string,int> IncrementDictionary (IDictionary<string,int> d)
        {
            var result = new Dictionary<string,int> ();
            foreach (var entry in d)
                result [entry.Key] = entry.Value + 1;
            return result;
        }

        [KRPCProcedure]
        public static HashSet<int> IncrementSet (HashSet<int> h)
        {
            var result = new HashSet<int> ();
            foreach (var item in h)
                result.Add (item + 1);
            return result;
        }

        [KRPCProcedure]
        public static KRPC.Utils.Tuple<int,long> IncrementTuple (KRPC.Utils.Tuple<int,long> t)
        {
            return KRPC.Utils.Tuple.Create<int,long> (t.Item1 + 1, t.Item2 + 1);
        }

        [KRPCProcedure]
        public static IDictionary<string,IList<int>> IncrementNestedCollection (IDictionary<string,IList<int>> d)
        {
            IDictionary<string,IList<int>> result = new Dictionary<string,IList<int>> ();
            foreach (var entry in d)
                result [entry.Key] = entry.Value.Select (x => x + 1).ToList ();
            return result;
        }

        [KRPCProcedure]
        public static IList<TestClass> AddToObjectList (IList<TestClass> l, string value)
        {
            l.Add (new TestClass (value));
            return l;
        }
    }
}
