﻿//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Linq;
using System.Reflection;
using AssetManager.Api;
using AssetManager.Api.Attributes;
using AssetManager.Api.Attributes.Types;
using NUnit.Framework;
using SethCS.Exceptions;

namespace AssetMananger.UnitTests.Api.Attributes.Types
{
    [TestFixture]
    public class StringAttributeTypeTests
    {
        [Test]
        public void ValidateTest()
        {
            StringAttributeType uut = new StringAttributeType
            {
                Key = "Some String",
                DefaultValue = null,
                Required = false
            };

            Assert.DoesNotThrow( () => uut.Validate() );

            // Having a default value should be okay.
            uut.DefaultValue = "Hello";
            Assert.DoesNotThrow( () => uut.Validate() );

            // Not having a default value with a min/max should work okay.
            Assert.DoesNotThrow( () => uut.Validate() );

            // Null key is not okay
            uut.Key = null;
            ListedValidationException e = Assert.Throws<ListedValidationException>( () => uut.Validate() );
            Assert.AreEqual( 1, e.Errors.Count() );
        }

        [Test]
        public void ValidateAttributeTest()
        {
            StringAttribute attr = new StringAttribute
            {
                Value = null
            };

            StringAttributeType uut = new StringAttributeType
            {
                Key = "Some Asset",
                Required = true
            };

            // Null should throw an exception, if required is set to true.
            {
                uut.Required = true;
                ListedValidationException e;

                e = Assert.Throws<ListedValidationException>( () => uut.ValidateAttribute( attr ) );
                Assert.AreEqual( 1, e.Errors.Count() );

                e = Assert.Throws<ListedValidationException>( () => uut.ValidateAttribute( (IAttribute)attr ) );
                Assert.AreEqual( 1, e.Errors.Count() );
            }

            // Null should not throw an exception if required is set to false.
            {
                uut.Required = false;
                Assert.DoesNotThrow( () => uut.ValidateAttribute( attr ) );
                Assert.DoesNotThrow( () => uut.ValidateAttribute( (IAttribute)attr ) );
            }

            // No exceptions should be thrown if required is false and a value is set.
            {
                uut.Required = false;
                attr.Value = "Some value";
                Assert.DoesNotThrow( () => uut.ValidateAttribute( attr ) );
                Assert.DoesNotThrow( () => uut.ValidateAttribute( (IAttribute)attr ) );
            }

            // No exceptions should be thrown if required is true and a value is set.
            {
                uut.Required = true;
                attr.Value = "Some value";
                Assert.DoesNotThrow( () => uut.ValidateAttribute( attr ) );
                Assert.DoesNotThrow( () => uut.ValidateAttribute( (IAttribute)attr ) );
            }
        }

        [Test]
        public void DeserializeTest1()
        {
            const string json =
@"
{
    ""Key"": ""Test Attribute"",
    ""AttributeType"": 2,
    ""Required"": false,
    ""PossibleValues"": null,
    ""DefaultValue"": null
}
";
            StringAttributeType uut = new StringAttributeType();
            uut.Deserialize( json );

            Assert.AreEqual( "Test Attribute", uut.Key );
            Assert.AreEqual( false, uut.Required );
            Assert.IsNull( uut.DefaultValue );
        }

        [Test]
        public void DeserializeTest2()
        {
            const string json =
@"
{
    ""Key"": ""Test Attribute"",
    ""AttributeType"": 2,
    ""Required"": true,
    ""PossibleValues"": null,
    ""DefaultValue"": ""Hello""
}
";
            StringAttributeType uut = new StringAttributeType();
            uut.Deserialize( json );

            Assert.AreEqual( "Test Attribute", uut.Key );
            Assert.AreEqual( true, uut.Required );
            Assert.AreEqual( "Hello", uut.DefaultValue );
        }

        /// <summary>
        /// Ensures if we serialize/deserialze an object, we get the same object back.
        /// </summary>
        [Test]
        public void SerializeDeserializeTest()
        {
            StringAttributeType originalObject = new StringAttributeType
            {
                Key = "My Attribute",
                DefaultValue = "World",
                Required = true
            };

            string serialObjected = originalObject.Serialize();

            StringAttributeType uut = new StringAttributeType();
            uut.Deserialize( serialObjected );

            Assert.AreEqual( originalObject.Key, uut.Key );
            Assert.AreEqual( originalObject.Required, uut.Required );
            Assert.AreEqual( originalObject.DefaultValue, uut.DefaultValue );
        }
    }
}
