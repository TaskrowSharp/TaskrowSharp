using System;
using System.Runtime.Serialization;
using Xunit;

namespace TaskrowSharp.Tests
{
    public class JsonTests
    {
        [Fact]
        public void Json_Serialize()
        {
            var test = new Test() { Name = "Eduardo Coutinho", Phone = "+55 11 6666-6666", ValueInt = 1, ValueDecimal = 0.35M };

            var json = Utils.JsonHelper.Serialize(test);

            Test test2 = Utils.JsonHelper.Deserialize<Test>(json);

            Assert.Equal(test.Name, test2.Name);
            Assert.Equal(test.Phone, test2.Phone);
            Assert.Equal(test.ValueInt, test2.ValueInt);
            Assert.Equal(test.ValueIntNulable, test2.ValueIntNulable);
            Assert.Equal(test.ValueDecimal, test2.ValueDecimal);
        }

        [DataContract]
        private class Test
        {
            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "phone")]
            public string Phone { get; set; }

            [DataMember(Name = "valueInt")]
            public int ValueInt { get; set; }

            [DataMember(Name = "valueIntNullable")]
            public int? ValueIntNulable { get; set; }

            [DataMember(Name = "valueDecimal")]
            public decimal ValueDecimal { get; set; }
        }
    }
}
