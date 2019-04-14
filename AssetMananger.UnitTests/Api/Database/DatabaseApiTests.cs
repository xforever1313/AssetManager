//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using AssetManager.Api.Database;
using Moq;
using NUnit.Framework;

namespace AssetMananger.UnitTests.Api.Database
{
    [TestFixture]
    public class DatabaseApiTests
    {
        // ---------------- Tests ----------------

        /// <summary>
        /// Ensures that if we pass in database configurations with duplicate names,
        /// it gets appended with the GUID.
        /// </summary>
        [Test]
        public void UniqueNameTest()
        {
            // db1 and db3 will have the same name, we expect the final database names to be
            // the database name the the GUID.

            Guid id1 = new Guid( "{08205A9C-D879-4EE4-B81C-64D4AB7CA596}" );
            Guid id2 = new Guid( "{3458AAD3-2358-4E02-B28C-36EDEA7C5080}" );
            Guid id3 = new Guid( "{7CFB2864-F606-455A-AC73-9AFA2F57B154}" );

            Mock<IDatabaseConfig> db1 = new Mock<IDatabaseConfig>( MockBehavior.Strict );
            db1.Setup( d => d.Name ).Returns( "My Database" );
            db1.Setup( d => d.DatabaseId ).Returns( id1 );

            Mock<IDatabaseConfig> db2 = new Mock<IDatabaseConfig>( MockBehavior.Strict );
            db2.Setup( d => d.Name ).Returns( "My Other Database" );
            db2.Setup( d => d.DatabaseId ).Returns( id2 );

            Mock<IDatabaseConfig> db3 = new Mock<IDatabaseConfig>( MockBehavior.Strict );
            db3.Setup( d => d.Name ).Returns( "My Database" );
            db3.Setup( d => d.DatabaseId ).Returns( id3 );

            List<IDatabaseConfig> configs = new List<IDatabaseConfig>
            {
                db1.Object,
                db2.Object,
                db3.Object
            };

            DatabaseApi api = new DatabaseApi( configs );

            Assert.AreEqual( "My Database " + id1, api.DatabaseNames[id1] );
            Assert.AreEqual( "My Other Database", api.DatabaseNames[id2] );
            Assert.AreEqual( "My Database " + id3, api.DatabaseNames[id3] );
        }

        /// <summary>
        /// Ensures if we pass in database configs with the same ID, we get an exception.
        /// </summary>
        [Test]
        public void DuplicateIdTest()
        {
            // db1 and db3 will have the same name, we expect the final database names to be
            // the database name the the GUID.

            Guid id1 = new Guid( "{08205A9C-D879-4EE4-B81C-64D4AB7CA596}" );
            Guid id2 = new Guid( "{3458AAD3-2358-4E02-B28C-36EDEA7C5080}" );
            Guid id3 = new Guid( "{08205A9C-D879-4EE4-B81C-64D4AB7CA596}" );

            Mock<IDatabaseConfig> db1 = new Mock<IDatabaseConfig>( MockBehavior.Strict );
            db1.Setup( d => d.Name ).Returns( "My Database" );
            db1.Setup( d => d.DatabaseId ).Returns( id1 );

            Mock<IDatabaseConfig> db2 = new Mock<IDatabaseConfig>( MockBehavior.Strict );
            db2.Setup( d => d.Name ).Returns( "My Other Database" );
            db2.Setup( d => d.DatabaseId ).Returns( id2 );

            Mock<IDatabaseConfig> db3 = new Mock<IDatabaseConfig>( MockBehavior.Strict );
            db3.Setup( d => d.Name ).Returns( "My Database" );
            db3.Setup( d => d.DatabaseId ).Returns( id3 );

            List<IDatabaseConfig> configs = new List<IDatabaseConfig>
            {
                db1.Object,
                db2.Object,
                db3.Object
            };

            Assert.Throws<ArgumentException>( () => new DatabaseApi( configs ) );
        }
    }
}
