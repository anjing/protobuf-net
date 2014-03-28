﻿using System;
using System.IO;
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
        static Protobuftests()
        {
            Type t = typeof(Protobuftests);
            MethodInfo factory = t.GetMethod("CreateObject");
            RuntimeTypeModel.Default.SetDefaultFactory(factory);
        }

        [TestMethod] 
        public void DefaultFactoryTest()
        {
            TestClassWithList test = new TestClassWithList {Id = 5, Names = new List<string>(), Childrens = new int[0] };
            MemoryStream ms = new MemoryStream();
            ProtoBuf.Serializer.Serialize(ms, test);
            TestClassWithList deserialized = ProtoBuf.Serializer.Deserialize<TestClassWithList>(ms);
            Assert.IsNotNull(deserialized.Names);
            Assert.IsNotNull(deserialized.Childrens);
            Assert.AreEqual(test.Id, deserialized.Id);
        }

        public static object CreateObject(Type t)
        {
            object res = Activator.CreateInstance(t);
            PropertyInfo[] properties = t.GetProperties();
            foreach(var prop in properties)
            {
                object value = prop.GetValue(res);
                if (value != null) continue;
                Type pType = prop.PropertyType;
                if ( pType.IsGenericType )
                {
                    value = Activator.CreateInstance(pType);
                }
                else if ( pType.IsArray)
                {
                    value = Array.CreateInstance(pType.GetElementType(), 0);
                }
                prop.SetValue(res, value);
            }
            return res;
        }
    }
}