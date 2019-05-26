//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System.Linq;
using AssetManager.Api;
using AssetManager.Api.Attributes;
using AssetManager.Api.Attributes.Types;
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
            ListedValidationException e = Assert.Throws<ListedValidationException>( () => uut.Validate() );
            Assert.AreEqual( 1, e.Errors.Count() );
            uut.DefaultValue = null;

            // Not having a default value with a min/max should work okay.
            Assert.DoesNotThrow( () => uut.Validate() );

            // Having a max < min should throw.
            uut.MinValue = 101;
            e = Assert.Throws<ListedValidationException>( () => uut.Validate() );
            Assert.AreEqual( 1, e.Errors.Count() );

            // Just having a default value should be okay.
            uut.MinValue = null;
            uut.MaxValue = null;
            uut.DefaultValue = 10;
            Assert.DoesNotThrow( () => uut.Validate() );

            // Null key is not okay
            uut.Key = null;
            e = Assert.Throws<ListedValidationException>( () => uut.Validate() );
            Assert.AreEqual( 1, e.Errors.Count() );
        }

        [Test]
        public void ValidateAttributeTest()
        {
            IntegerAttribute attr = new IntegerAttribute
            {
                Value = 1
            };

            IntegerAttributeType uut = new IntegerAttributeType
            {
                Key = "Some Asset",
                MaxValue = null,
                MinValue = null,
                Required = false
            };

            // Having min and max not specified should not result in an exception.
            {
                Assert.DoesNotThrow( () => uut.ValidateAttribute( attr ) );
                Assert.DoesNotThrow( () => uut.ValidateAttribute( (IAttribute)attr ) );
            }

            // Still within range, should not blow up.
            {
                uut.MinValue = attr.Value;
                uut.MaxValue = attr.Value;

                Assert.DoesNotThrow( () => uut.ValidateAttribute( attr ) );
                Assert.DoesNotThrow( () => uut.ValidateAttribute( (IAttribute)attr ) );
            }

            // Below min should result in an exception.
            {
                uut.MinValue = attr.Value + 1;
                uut.MaxValue = null;

                ListedValidationException e;
                e = Assert.Throws<ListedValidationException>( () => uut.ValidateAttribute( attr ) );
                Assert.AreEqual( 1, e.Errors.Count() );

                e = Assert.Throws<ListedValidationException>( () => uut.ValidateAttribute( (IAttribute)attr ) );
                Assert.AreEqual( 1, e.Errors.Count() );
            }

            // Above max should result in an exception.
            {
                uut.MinValue = null;
                uut.MaxValue = attr.Value - 1;

                ListedValidationException e;
                e = Assert.Throws<ListedValidationException>( () => uut.ValidateAttribute( attr ) );
                Assert.AreEqual( 1, e.Errors.Count() );

                e = Assert.Throws<ListedValidationException>( () => uut.ValidateAttribute( (IAttribute)attr ) );
                Assert.AreEqual( 1, e.Errors.Count() );
            }
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
