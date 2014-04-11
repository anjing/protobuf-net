using System;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace protobuf_net_test
{
    [TestClass]
    public class Example
    {
        public Example()
        {
        }

        private int test;
        public Example(int test) { this.test = test; }
        private delegate long SquareItInvoker(int input);
        private delegate TReturn OneParameter<out TReturn, in TParameter0>(TParameter0 p0);
        [TestMethod]
        public void EmitTestMethod1()
        {
            Type[] methodArgs = { typeof(int) };
            DynamicMethod squareIt = new DynamicMethod(
            "SquareIt",
            typeof(long),
            methodArgs,
            typeof(Example).Module);
            ILGenerator il = squareIt.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Conv_I8);
            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Mul);
            il.Emit(OpCodes.Ret);

            OneParameter<long, int> invokeSquareIt =
            (OneParameter<long, int>)
            squareIt.CreateDelegate(typeof(OneParameter<long, int>));

            Assert.AreEqual(invokeSquareIt(12), 12*12);            
        }

        [TestMethod]
        public void EmitTestMethod2()
        {
            Type[] methodArgs2 = { typeof(Example), typeof(int) };
            DynamicMethod multiplyHidden = new DynamicMethod(
            "",
            typeof(int),
            methodArgs2,
            typeof(Example));
            ILGenerator ilMH = multiplyHidden.GetILGenerator();
            ilMH.Emit(OpCodes.Ldarg_0);

            FieldInfo testInfo = typeof(Example).GetField("test",
                BindingFlags.NonPublic | BindingFlags.Instance);

            ilMH.Emit(OpCodes.Ldfld, testInfo);
            ilMH.Emit(OpCodes.Ldarg_1);
            ilMH.Emit(OpCodes.Mul);
            ilMH.Emit(OpCodes.Ret);

            OneParameter<int, int> invoke = (OneParameter<int, int>)
             multiplyHidden.CreateDelegate(
                 typeof(OneParameter<int, int>),
                 new Example(42)
             );

            Assert.AreEqual(invoke(3), 3*42);
        }

    }
}
