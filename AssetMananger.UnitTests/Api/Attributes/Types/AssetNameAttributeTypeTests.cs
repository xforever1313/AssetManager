//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System.Linq;
using AssetManager.Api.Attributes.Types;
using NUnit.Framework;
using SethCS.Exceptions;

namespace AssetMananger.UnitTests.Api.Attributes.Types
{
    [TestFixture]
    public class AssetNameAttributeTypeTests
    {
        [Test]
        public void ValidateTest()
        {
            AssetNameAttributeType uut = new AssetNameAttributeType
            {
                Key = "Some Asset"
            };

            Assert.DoesNotThrow( () => uut.Validate() );

            // Required set to false will cause a validation error,
            // this is ALWAYS required.

            uut.Required = false;
            ListedValidationException e = Assert.Throws<ListedValidationException>( () => uut.Validate() );
            Assert.AreEqual( 1, e.Errors.Count() );
            uut.Required = true;

            // Null key will throw exceptions.
            uut.Key = null;
            e = Assert.Throws<ListedValidationException>( () => uut.Validate() );
            Assert.AreEqual( 1, e.Errors.Count() );
        }

        [Test]
        public void DeserializeTest1()
        {
            const string json =
@"
{
    ""Key"": ""Asset Name"",
    ""AttributeType"": 1,
    ""Required"": true,
    ""DefaultValue"": null,
    ""PossibleValues"": null
}
";
            AssetNameAttributeType uut = new AssetNameAttributeType();
            uut.Deserialize( json );

            Assert.AreEqual( "Asset Name", uut.Key );
            Assert.AreEqual( true, uut.Required );
        }

        /// <summary>
        /// Ensures if we serialize/deserialze an object, we get the same object back.
        /// </summary>
        [Test]
        public void SerializeDeserializeTest()
        {
            AssetNameAttributeType originalObject = new AssetNameAttributeType
            {
                Key = "Asset Name"
            };

            string serialObjected = originalObject.Serialize();

            AssetNameAttributeType uut = new AssetNameAttributeType();
            uut.Deserialize( serialObjected );

            Assert.AreEqual( originalObject.Key, uut.Key );
            Assert.AreEqual( originalObject.Required, uut.Required );
        }
    }
}
