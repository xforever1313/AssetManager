//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using AssetManager.Api;
using AssetManager.Api.Attributes;
using NUnit.Framework;

namespace AssetMananger.UnitTests.Api.Attributes
{
    [TestFixture]
    public class ImageUrlAttributeTests
    {
        // ---------------- Tests ----------------

        [Test]
        public void EqualsTest()
        {
            ImageUrlAttribute uut1 = new ImageUrlAttribute( "/MyImage.jpg" );
            ImageUrlAttribute uut2 = new ImageUrlAttribute( uut1.Value );

            Assert.AreNotEqual( uut1, 1 );
            Assert.AreNotEqual( uut1, null );

            Assert.AreEqual( uut1, uut2 );
            Assert.AreEqual( uut1.GetHashCode(), uut2.GetHashCode() );

            // Start changing things.
            {
                uut1.Value = uut2.Value + "1";
                Assert.AreNotEqual( uut1, uut2 );
                Assert.AreNotEqual( uut1.GetHashCode(), uut2.GetHashCode() );
                uut1.Value = uut2.Value;
            }

            {
                uut1.Value = null;
                Assert.AreNotEqual( uut1, uut2 );
                Assert.AreNotEqual( uut1.GetHashCode(), uut2.GetHashCode() );
                uut1.Value = uut2.Value;
            }

            {
                uut1.Width = null;
                uut2.Width = 1;
                Assert.AreNotEqual( uut1, uut2 );
                Assert.AreNotEqual( uut1.GetHashCode(), uut2.GetHashCode() );
                uut1.Width = null;
                uut2.Width = null;
            }

            {
                uut1.Height = null;
                uut2.Height = 1;
                Assert.AreNotEqual( uut1, uut2 );
                Assert.AreNotEqual( uut1.GetHashCode(), uut2.GetHashCode() );
                uut1.Height = null;
                uut2.Height = null;
            }

            {
                uut1.Width = 1;
                uut2.Width = uut1.Width + 1;
                Assert.AreNotEqual( uut1, uut2 );
                Assert.AreNotEqual( uut1.GetHashCode(), uut2.GetHashCode() );
                uut1.Width = null;
                uut2.Width = null;
            }

            {
                uut1.Height = 1;
                uut2.Height = uut1.Height + 1;
                Assert.AreNotEqual( uut1, uut2 );
                Assert.AreNotEqual( uut1.GetHashCode(), uut2.GetHashCode() );
                uut1.Height = null;
                uut2.Height = null;
            }
        }

        [Test]
        public void SerializeTest1()
        {
            IAttribute uut1 = new ImageUrlAttribute( null );
            IAttribute uut2 = new ImageUrlAttribute( null );

            string serializedValue = uut1.Serialize();
            uut2.Deserialize( serializedValue );

            Assert.AreEqual( uut1, uut2 );
        }

        [Test]
        public void SerializeTest2()
        {
            IAttribute uut1 = new ImageUrlAttribute( "/MyImage.jpg" )
            {
                Height = 100,
                Width = 101
            };

            IAttribute uut2 = new ImageUrlAttribute( null );

            string serializedValue = uut1.Serialize();
            uut2.Deserialize( serializedValue );

            Assert.AreEqual( uut1, uut2 );
        }
    }
}
