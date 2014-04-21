using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProtoBuf;

namespace protobuf_net_test
{
    [ProtoContract]
    class TestClassWithList
    {
        [ProtoMember(1)]
        public int Id { get; set; }
        [ProtoMember(2)]        
        public List<string> Names { get; set; }
        [ProtoMember(3, IsPacked = true)]
        public int[] Childrens { get; set; }
        [ProtoMember(4, IsPacked = true)]
        public List<int> ListInts { get; set; }
        [ProtoMember(5)]
        public List<TestClassWithList> SubLists { get; set; }
    }
}