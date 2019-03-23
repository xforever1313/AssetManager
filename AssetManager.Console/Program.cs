//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.IO;
using AssetManager.Api;
using AssetManager.Api.Attributes;
using AssetManager.Api.Attributes.Types;

namespace AssetManager.Cli
{
    class Program
    {
        static int Main( string[] args )
        {
            try
            {
                if( File.Exists( @"C:\Users\xfore\Downloads\assetmanager.db" ) )
                {
                    File.Delete( @"C:\Users\xfore\Downloads\assetmanager.db" );
                }

                IAssetManagerApi api = AssetManagerApiFactory.CreateApiFromDefaultConfigFile();

                {
                    AssetTypeBuilder builder = new AssetTypeBuilder( "Pokemon Card" );

                    IntegerAttributeType hpAttribute = new IntegerAttributeType
                    {
                        Key = "HP",
                        MinValue = 10,
                        Required = false,
                        DefaultValue = 50
                    };

                    builder.AttributeTypes.Add( hpAttribute );

                    IntegerAttributeType retreatCostAttribute = new IntegerAttributeType
                    {
                        Key = "Retreat Cost",
                        MinValue = 0,
                        MaxValue = 4,
                        Required = false
                    };
                    builder.AttributeTypes.Add( retreatCostAttribute );

                    api.DataBase.AddAssetType( builder );
                }

                {
                    AssetTypeBuilder builder = new AssetTypeBuilder( "Yugioh! Card" );

                    IntegerAttributeType attackAttribute = new IntegerAttributeType
                    {
                        Key = "Attack",
                        MinValue = 0,
                        Required = true
                    };
                    builder.AttributeTypes.Add( attackAttribute );

                    IntegerAttributeType defenseAttribute = new IntegerAttributeType
                    {
                        Key = "Defense",
                        MinValue = 0,
                        Required = true
                    };
                    builder.AttributeTypes.Add( defenseAttribute );

                    api.DataBase.AddAssetType( builder );
                }

                {
                    Asset asset = api.DataBase.GenerateEmptyAsset( "Pokemon Card" );
                    asset.Name = "Politoed";
                    asset.SetAttribute( "HP", new IntegerAttribute() { Value = 100 } );
                    asset.SetAttribute( "Retreat Cost", new IntegerAttribute() { Value = 3 } );

                    api.DataBase.AddAsset( asset );
                }

                {
                    Asset asset = api.DataBase.GenerateEmptyAsset( "Yugioh! Card" );
                    asset.Name = "The 13th Grave";

                    IntegerAttribute attackAttr = asset.CloneAttributeAsType<IntegerAttribute>( "Attack" );
                    attackAttr.Value = 1200;
                    asset.SetAttribute( "Attack", attackAttr );

                    IntegerAttribute defenseAttr = asset.CloneAttributeAsType<IntegerAttribute>( "Defense" );
                    defenseAttr.Value = 900;
                    asset.SetAttribute( "Defense", attackAttr );

                    api.DataBase.AddAsset( asset );
                }

                {
                    Asset asset = api.DataBase.GenerateEmptyAsset( "Yugioh! Card" );
                    asset.Name = "Overdrive";
                    asset.SetAttribute( "Attack", new IntegerAttribute( 1600 ) );
                    asset.SetAttribute( "Defense", new IntegerAttribute( 1500 ) );

                    api.DataBase.AddAsset( asset );
                }

                return 0;
            }
            catch( Exception e )
            {
                Console.WriteLine( "UNHANDLED EXCEPTION:" );
                Console.Write( e.ToString() );
                return -1;
            }
        }
    }
}
