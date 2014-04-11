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

        [TestMethod]
        public void CollectionTest()
        {
            TestClassWithList test = new TestClassWithList { Id = 5, Names = new List<string>{"name1","name2",""}, ListInts = new List<int>{1,2,3}, Childrens = new []{3,4,5}};            
            MemoryStream ms = new MemoryStream();
            Serializer.Serialize(ms, test);
            ms.Seek(0, SeekOrigin.Begin);
            TestClassWithList deserialized = Serializer.Deserialize<TestClassWithList>(ms);
            Assert.AreEqual(deserialized.Names.Count, 3);
            Assert.AreEqual(deserialized.Names[0], "name1");
            Assert.AreEqual(deserialized.Names[1], "name2");
            Assert.AreEqual(deserialized.Names[2], "");
            Assert.AreEqual(deserialized.ListInts.Count, 3);
            Assert.AreEqual(deserialized.ListInts[0], 1);
            Assert.AreEqual(deserialized.ListInts[1], 2);
            Assert.AreEqual(deserialized.ListInts[2], 3);
            Assert.AreEqual(deserialized.Childrens.Length, 3);
            Assert.AreEqual(deserialized.Childrens[0], 3);
            Assert.AreEqual(deserialized.Childrens[1], 4);
            Assert.AreEqual(deserialized.Childrens[2], 5);
            Assert.AreEqual(test.Id, deserialized.Id);
        }
    }
}
