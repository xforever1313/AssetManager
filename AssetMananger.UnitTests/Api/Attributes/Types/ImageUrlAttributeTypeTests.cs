//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Linq;
using AssetManager.Api;
using AssetManager.Api.Attributes;
using AssetManager.Api.Attributes.Types;
using NUnit.Framework;
using SethCS.Exceptions;

namespace AssetMananger.UnitTests.Api.Attributes.Types
{
    public class ImageUrlAttributeTypeTests
    {
        [Test]
        public void ValidateTest()
        {
            ImageUrlAttributeType uut = new ImageUrlAttributeType
            {
                Key = "Some Image",
                Required = false
            };

            Assert.DoesNotThrow( () => uut.Validate() );

            // Null key is not okay
            uut.Key = null;
            ListedValidationException e = Assert.Throws<ListedValidationException>( () => uut.Validate() );
            Assert.AreEqual( 1, e.Errors.Count() );
        }

        [Test]
        public void ValidateAttributeTest()
        {
            ImageUrlAttribute attr = new ImageUrlAttribute
            {
                Value = null
            };

            ImageUrlAttributeType uut = new ImageUrlAttributeType
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
                attr.Value = "https://someUrl.org/derp.jpg";
                Assert.DoesNotThrow( () => uut.ValidateAttribute( attr ) );
                Assert.DoesNotThrow( () => uut.ValidateAttribute( (IAttribute)attr ) );
            }

            // http:// is okay.
            {
                uut.Required = false;
                attr.Value = "http://someUrl.com/derp.jpg";
                Assert.DoesNotThrow( () => uut.ValidateAttribute( attr ) );
                Assert.DoesNotThrow( () => uut.ValidateAttribute( (IAttribute)attr ) );
            }

            // No exceptions should be thrown if required is true and a value is set.
            {
                uut.Required = true;
                attr.Value = "/Somevalue/pic.jpg";
                Assert.DoesNotThrow( () => uut.ValidateAttribute( attr ) );
                Assert.DoesNotThrow( () => uut.ValidateAttribute( (IAttribute)attr ) );
            }

            // No exceptions should be thrown if height and width are set, but are both are 0.
            {
                uut.Required = true;
                attr.Value = "/Somevalue/pic2.jpg";
                attr.Height = 0;
                attr.Width = 0;
                Assert.DoesNotThrow( () => uut.ValidateAttribute( attr ) );
                Assert.DoesNotThrow( () => uut.ValidateAttribute( (IAttribute)attr ) );
            }

            // No exceptions should be thrown if height and width are set, but are both are greater than 0.
            {
                uut.Required = true;
                attr.Value = "/Somevalue/pic2.jpg";
                attr.Height = 1;
                attr.Width = 1;
                Assert.DoesNotThrow( () => uut.ValidateAttribute( attr ) );
                Assert.DoesNotThrow( () => uut.ValidateAttribute( (IAttribute)attr ) );
            }

            // Height can not be negative.
            {
                attr.Value = "https://shendrick.net/Somevalue/pic2.jpg";
                attr.Height = -1;
                attr.Width = null;
                ListedValidationException e;

                e = Assert.Throws<ListedValidationException>( () => uut.ValidateAttribute( attr ) );
                Assert.AreEqual( 1, e.Errors.Count() );

                e = Assert.Throws<ListedValidationException>( () => uut.ValidateAttribute( (IAttribute)attr ) );
                Assert.AreEqual( 1, e.Errors.Count() );
            }

            // Width can not be negative.
            {
                attr.Value = "https://shendrick.net/Somevalue/pic3.jpg";
                attr.Height = null;
                attr.Width = -1;
                ListedValidationException e;

                e = Assert.Throws<ListedValidationException>( () => uut.ValidateAttribute( attr ) );
                Assert.AreEqual( 1, e.Errors.Count() );

                e = Assert.Throws<ListedValidationException>( () => uut.ValidateAttribute( (IAttribute)attr ) );
                Assert.AreEqual( 1, e.Errors.Count() );
            }

            {
                uut.Required = false;

                // If its starts with file://///, this should fail.
                attr.Value = "file://///users/seth/somePic.jpg";
                attr.Height = null;
                attr.Width= null;
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
    ""AttributeType"": 4,
    ""Required"": false,
    ""PossibleValues"": null,
    ""DefaultValue"": null
}
";
            ImageUrlAttributeType uut = new ImageUrlAttributeType();
            uut.Deserialize( json );

            Assert.AreEqual( "Test Attribute", uut.Key );
            Assert.AreEqual( false, uut.Required );
        }

        [Test]
        public void DeserializeTest2()
        {
            const string json =
@"
{
    ""Key"": ""Test Attribute"",
    ""AttributeType"": 4,
    ""Required"": true,
    ""PossibleValues"": null,
    ""DefaultValue"": null
}
";
            ImageUrlAttributeType uut = new ImageUrlAttributeType();
            uut.Deserialize( json );

            Assert.AreEqual( "Test Attribute", uut.Key );
            Assert.AreEqual( true, uut.Required );
        }

        /// <summary>
        /// Ensures if we serialize/deserialze an object, we get the same object back.
        /// </summary>
        [Test]
        public void SerializeDeserializeTest()
        {
            ImageUrlAttributeType originalObject = new ImageUrlAttributeType
            {
                Key = "My Attribute",
                Required = true
            };

            string serialObjected = originalObject.Serialize();

            ImageUrlAttributeType uut = new ImageUrlAttributeType();
            uut.Deserialize( serialObjected );

            Assert.AreEqual( originalObject.Key, uut.Key );
            Assert.AreEqual( originalObject.Required, uut.Required );
        }
    }
}
