using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TaskrowSharp.IntegrationTests
{
    [TestFixture]
    public class JsonTests
    {
        [Test]
        public void Json_Serialize()
        {
            Test test = new Test() { Name = "Eduardo Coutinho", Phone = "+55 11 6666-6666", ValueInt = 1, ValueDecimal = 0.35M };

            string json = Utils.JsonHelper.Serialize(test);

            Test test2 = Utils.JsonHelper.Deserialize<Test>(json);

            Assert.AreEqual(test.Name, test2.Name);
            Assert.AreEqual(test.Phone, test2.Phone);
            Assert.AreEqual(test.ValueInt, test2.ValueInt);
            Assert.AreEqual(test.ValueIntNulable, test2.ValueIntNulable);
            Assert.AreEqual(test.ValueDecimal, test2.ValueDecimal);
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
            public Nullable<int> ValueIntNulable { get; set; }

            [DataMember(Name = "valueDecimal")]
            public Decimal ValueDecimal { get; set; }
        }
    }
}
