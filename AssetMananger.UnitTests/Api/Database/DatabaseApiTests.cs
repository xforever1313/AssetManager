//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.IO;
using AssetManager.Api;
using AssetManager.Api.Attributes;
using AssetManager.Api.Attributes.Types;
using AssetManager.Api.Database;
using AssetManager.Sqlite;
using Moq;
using NUnit.Framework;

namespace AssetMananger.UnitTests.Api.Database
{
    [TestFixture]
    public class DatabaseApiTests
    {
        // ---------------- Fields ----------------

        private const string redKey = "R";
        private const string greenKey = "G";
        private const string blueKey = "B";
        private const string nameKey = "Name";

        private const string colorTypeName = "Colors";

        private string db1Location;

        // ---------------- Setup / Teardown ----------------

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            this.db1Location = Path.Combine(
                TestContext.CurrentContext.TestDirectory,
                "test1.db"
            );
        }

        [OneTimeTearDown]
        public void FixtureTeardown()
        {
        }

        [SetUp]
        public void TestSetup()
        {
            if ( File.Exists( db1Location ) )
            {
                File.Delete( db1Location );
            }
        }

        [TearDown]
        public void TestTeardown()
        {
            if ( File.Exists( db1Location ) )
            {
                File.Delete( db1Location );
            }
        }

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

        [Test]
        public void AddRemoveUpdateAssetTest()
        {
            // First, create a simple asset type.  We'll do colors!
            SqliteDatabaseConfig databaseConfig = new SqliteDatabaseConfig
            {
                DatabaseLocation = this.db1Location
            };
            Guid databaseId = databaseConfig.DatabaseId;

            DatabaseApi uut = new DatabaseApi( new List<IDatabaseConfig> { databaseConfig } );

            int typeId = this.AddColorAttributeType( uut, databaseId );
            Assert.Greater( typeId, 0 );

            int redId = this.AddRedAsset( uut, databaseId, typeId );
            this.VerifyRedAsset( uut.GetAsset( databaseId, redId ) );

            int greenId = this.AddGreenAsset( uut, databaseId, typeId );
            this.VerifyGreenAsset( uut.GetAsset( databaseId, greenId ) );
            // Ensure red wasn't modified
            this.VerifyRedAsset( uut.GetAsset( databaseId, redId ) );

            int blueId = this.AddBlueAsset( uut, databaseId, typeId );
            this.VerifyBlueAsset( uut.GetAsset( databaseId, blueId ) );
            // Ensure red and green weren't modified.
            this.VerifyRedAsset( uut.GetAsset( databaseId, redId ) );
            this.VerifyGreenAsset( uut.GetAsset( databaseId, greenId ) );

            // Add a duplicate color, blue 2.
            int blue2Id = this.AddBlueAsset( uut, databaseId, typeId );
            this.VerifyBlueAsset( uut.GetAsset( databaseId, blue2Id ) );
            // Ensure existing assets weren't modified.
            this.VerifyRedAsset( uut.GetAsset( databaseId, redId ) );
            this.VerifyGreenAsset( uut.GetAsset( databaseId, greenId ) );
            this.VerifyBlueAsset( uut.GetAsset( databaseId, blueId ) );

            // Remove duplicate blue ID
            uut.DeleteAsset( databaseId, typeId, blue2Id );
            Assert.Throws<ArgumentException>( () => uut.GetAsset( databaseId, blue2Id ) ); // <- Should not exist.
            // Ensure existing assets weren't modified.
            this.VerifyRedAsset( uut.GetAsset( databaseId, redId ) );
            this.VerifyGreenAsset( uut.GetAsset( databaseId, greenId ) );
            this.VerifyBlueAsset( uut.GetAsset( databaseId, blueId ) );

            // Edit red to make it slightly less red.
            {
                Asset redAsset = uut.GetAsset( databaseId, redId );
                AssetNameAttribute nameAttr = redAsset.CloneAttributeAsType<AssetNameAttribute>( nameKey );
                nameAttr.Value = "New Red!";
                redAsset.SetAttribute( nameKey, nameAttr );

                IntegerAttribute redAttr = redAsset.CloneAttributeAsType<IntegerAttribute>( redKey );
                redAttr.Value = 254;
                redAsset.SetAttribute( redKey, redAttr );

                IntegerAttribute greenAttr = redAsset.CloneAttributeAsType<IntegerAttribute>( greenKey );
                greenAttr.Value = 1;
                redAsset.SetAttribute( greenKey, greenAttr );

                IntegerAttribute blueAttr = redAsset.CloneAttributeAsType<IntegerAttribute>( blueKey );
                blueAttr.Value = 5;
                redAsset.SetAttribute( blueKey, blueAttr );

                uut.UpdateAsset( redId, redAsset );
                this.VerifyAsset(
                    uut.GetAsset( databaseId, redId ),
                    nameAttr.Value,
                    redAttr.Value,
                    greenAttr.Value,
                    blueAttr.Value
                );
            }

            AssetListInfo assetList = uut.GetAssetsOfType( databaseId, typeId );
            Assert.AreEqual( 3, assetList.AssetList.Count );
            Assert.AreEqual( typeId, assetList.AssetTypeId );
            Assert.AreEqual( colorTypeName, assetList.AssetTypeName );
            Assert.AreEqual( databaseId, assetList.DatabaseId );
        }

        /// <summary>
        /// Adds the color attribute type.
        /// </summary>
        /// <returns>The type ID.</returns>
        private int AddColorAttributeType( IDatabaseApi uut, Guid databaseId )
        {
            AssetTypeBuilder typeBuilder = new AssetTypeBuilder( colorTypeName, databaseId );
            AssetNameAttributeType assetNameType = new AssetNameAttributeType
            {
                Key = nameKey,
                Required = true
            };
            typeBuilder.AttributeTypes.Add( assetNameType );

            IntegerAttributeType redValue = new IntegerAttributeType
            {
                Key = redKey,
                Required = true,
                MinValue = 0,
                MaxValue = 255,
                DefaultValue = 0
            };

            typeBuilder.AttributeTypes.Add( redValue );

            IntegerAttributeType greenValue = new IntegerAttributeType
            {
                Key = greenKey,
                Required = true,
                MinValue = 0,
                MaxValue = 255,
                DefaultValue = 0
            };

            typeBuilder.AttributeTypes.Add( greenValue );

            IntegerAttributeType blueValue = new IntegerAttributeType
            {
                Key = blueKey,
                Required = true,
                MinValue = 0,
                MaxValue = 255,
                DefaultValue = 0
            };

            typeBuilder.AttributeTypes.Add( blueValue );

            int colorAssetTypeID = uut.AddAssetType( typeBuilder );
            return colorAssetTypeID;
        }

        private int AddRedAsset( IDatabaseApi uut, Guid databaseId, int assetTypeId )
        {
            Asset redAsset = uut.GenerateEmptyAsset( databaseId, assetTypeId );
            redAsset.SetAttribute( nameKey, new AssetNameAttribute { Value = "Red" } );
            redAsset.SetAttribute( redKey, new IntegerAttribute { Value = 255 } );
            redAsset.SetAttribute( greenKey, new IntegerAttribute { Value = 0 } );
            redAsset.SetAttribute( blueKey, new IntegerAttribute { Value = 0 } );

            return uut.AddAsset( redAsset );
        }

        private int AddGreenAsset( IDatabaseApi uut, Guid databaseId, int assetTypeId )
        {
            Asset greenAsset = uut.GenerateEmptyAsset( databaseId, assetTypeId );
            greenAsset.SetAttribute( nameKey, new AssetNameAttribute { Value = "Green" } );
            greenAsset.SetAttribute( redKey, new IntegerAttribute { Value = 0 } );
            greenAsset.SetAttribute( greenKey, new IntegerAttribute { Value = 255 } );
            greenAsset.SetAttribute( blueKey, new IntegerAttribute { Value = 0 } );

            return uut.AddAsset( greenAsset );
        }

        private int AddBlueAsset( IDatabaseApi uut, Guid databaseId, int assetTypeId )
        {
            Asset blueAsset = uut.GenerateEmptyAsset( databaseId, assetTypeId );
            blueAsset.SetAttribute( nameKey, new AssetNameAttribute { Value = "Blue" } );
            blueAsset.SetAttribute( redKey, new IntegerAttribute { Value = 0 } );
            blueAsset.SetAttribute( greenKey, new IntegerAttribute { Value = 0 } );
            blueAsset.SetAttribute( blueKey, new IntegerAttribute { Value = 255 } );

            return uut.AddAsset( blueAsset );
        }

        private void VerifyRedAsset( Asset redAsset )
        {
            this.VerifyAsset( redAsset, "Red", 255, 0, 0 );
        }

        private void VerifyGreenAsset( Asset greenAsset )
        {
            this.VerifyAsset( greenAsset, "Green", 0, 255, 0 );
        }

        private void VerifyBlueAsset( Asset blueAsset )
        {
            this.VerifyAsset( blueAsset, "Blue", 0, 0, 255 );
        }

        private void VerifyAsset( Asset asset, string name, int red, int green, int blue )
        {
            Assert.IsTrue( asset.ContainsKey( nameKey ) );
            Assert.IsTrue( asset.ContainsKey( redKey ) );
            Assert.IsTrue( asset.ContainsKey( greenKey ) );
            Assert.IsTrue( asset.ContainsKey( blueKey ) );

            Assert.AreEqual( name, asset.CloneAttributeAsType<AssetNameAttribute>( nameKey ).Value );
            Assert.AreEqual( red, asset.CloneAttributeAsType<IntegerAttribute>( redKey ).Value );
            Assert.AreEqual( green, asset.CloneAttributeAsType<IntegerAttribute>( greenKey ).Value );
            Assert.AreEqual( blue, asset.CloneAttributeAsType<IntegerAttribute>( blueKey ).Value );
        }
    }
}
