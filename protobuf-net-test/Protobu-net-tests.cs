using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProtoBuf;
using System.Collections.Generic;
using ProtoBuf.Meta;


namespace protobuf_net_test
{
    [TestClass]
    public class Protobuftests
    {

        [TestMethod] 
        public void EmptyCollectionTest()
        {
            TestClassWithList test = new TestClassWithList {Id = 5, Names = new List<string>(), Childrens = new int[0], ListInts = new List<int>() };
            MemoryStream ms = new MemoryStream();
            Serializer.Serialize(ms, test);
            ms.Seek(0, SeekOrigin.Begin);
            TestClassWithList deserialized = Serializer.Deserialize<TestClassWithList>(ms);
            Assert.IsNotNull(deserialized.Names);
            Assert.AreEqual(deserialized.Names.Count, 0);
            Assert.IsNotNull(deserialized.Childrens);
            Assert.AreEqual(deserialized.Childrens.Count(), 0);
            Assert.IsNotNull(deserialized.ListInts);
            Assert.AreEqual(deserialized.ListInts.Count(), 0);

            Assert.AreEqual(test.Id, deserialized.Id);
        }

        [TestMethod]
        public void NullCollectionTest()
        {
            TestClassWithList test = new TestClassWithList { Id = 5};
            MemoryStream ms = new MemoryStream();
            Serializer.Serialize(ms, test);
            ms.Seek(0, SeekOrigin.Begin);
            TestClassWithList deserialized = Serializer.Deserialize<TestClassWithList>(ms);
            Assert.IsNull(deserialized.Names); 
            Assert.IsNull(deserialized.ListInts);
            Assert.IsNull(deserialized.Childrens);
            Assert.AreEqual(test.Id, deserialized.Id);
        }
    }
}
