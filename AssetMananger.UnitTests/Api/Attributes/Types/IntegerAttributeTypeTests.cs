//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using AssetManager.Api.Attributes.Types;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using SethCS.Exceptions;

namespace AssetMananger.UnitTests.Api.Attributes.Types
{
    [TestFixture]
    public class IntegerAttributeTypeTests
    {
        [Test]
        public void ValidateTest()
        {
            IntegerAttributeType uut = new IntegerAttributeType
            {
                Key = "Some Int",
                DefaultValue = null,
                MaxValue = null,
                MinValue = null,
                Required = false
            };

            Assert.DoesNotThrow( () => uut.Validate() );

            // Having a Max, min, and default should be okay.
            uut.DefaultValue = 10;
            uut.MinValue = 0;
            uut.MaxValue = 100;
            Assert.DoesNotThrow( () => uut.Validate() );

            // Having a default value outside of the range should throw an exception.
            uut.DefaultValue = -1;
            Assert.Throws<ValidationException>( () => uut.Validate() );
            uut.DefaultValue = null;

            // Not having a default value with a min/max should work okay.
            Assert.DoesNotThrow( () => uut.Validate() );

            // Having a max < min should throw.
            uut.MinValue = 101;
            Assert.Throws<ValidationException>( () => uut.Validate() );

            // Just having a default value should be okay.
            uut.MinValue = null;
            uut.MaxValue = null;
            uut.DefaultValue = 10;
            Assert.DoesNotThrow( () => uut.Validate() );

            // Null key is not okay
            uut.Key = null;
            Assert.Throws<ValidationException>( () => uut.Validate() );
        }

        [Test]
        public void DeserializeTest1()
        {
            const string json =
@"
{
    ""Key"": ""Test Attribute"",
    ""AttributeType"": 3,
    ""Required"": false,
    ""PossibleValues"": {
        ""Version"": 1,
        ""MinValue"": 3,
        ""MaxValue"": null
    },
    ""DefaultValue"": null
}
";
            IntegerAttributeType uut = new IntegerAttributeType();
            uut.Deserialize( json );

            Assert.AreEqual( "Test Attribute", uut.Key );
            Assert.AreEqual( false, uut.Required );
            Assert.AreEqual( 3, uut.MinValue );
            Assert.IsNull( uut.MaxValue );
            Assert.IsNull( uut.DefaultValue );
        }

        [Test]
        public void DeserializeTest2()
        {
            const string json =
@"
{
    ""Key"": ""Test Attribute"",
    ""AttributeType"": 3,
    ""Required"": true,
    ""PossibleValues"": {
        ""Version"": 1,
        ""MinValue"": null,
        ""MaxValue"": 1
    },
    ""DefaultValue"": 2
}
";
            IntegerAttributeType uut = new IntegerAttributeType();
            uut.Deserialize( json );

            Assert.AreEqual( "Test Attribute", uut.Key );
            Assert.AreEqual( true, uut.Required );
            Assert.IsNull( uut.MinValue );
            Assert.AreEqual( 1, uut.MaxValue );
            Assert.AreEqual( 2, uut.DefaultValue );
        }

        /// <summary>
        /// Ensures if we serialize/deserialze an object, we get the same object back.
        /// </summary>
        [Test]
        public void SerializeDeserializeTest()
        {
            IntegerAttributeType originalObject = new IntegerAttributeType
            {
                Key = "My Object",
                MaxValue = 100,
                MinValue = 0,
                Required = true,
                DefaultValue = 50
            };

            string serialObjected = originalObject.Serialize();

            IntegerAttributeType uut = new IntegerAttributeType();
            uut.Deserialize( serialObjected );

            Assert.AreEqual( originalObject.Key, uut.Key );
            Assert.AreEqual( originalObject.MaxValue, uut.MaxValue );
            Assert.AreEqual( originalObject.MinValue, uut.MinValue );
            Assert.AreEqual( originalObject.Required, uut.Required );
            Assert.AreEqual( originalObject.DefaultValue, uut.DefaultValue );
        }
    }
}
