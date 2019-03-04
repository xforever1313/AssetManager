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

namespace AssetManager.Cli
{
    class Program
    {
        static void Main( string[] args )
        {
            File.Delete( @"C:\Users\xfore\Downloads\assetmanager.db" );

            AssetManagerSettings settings = new AssetManagerSettings
            {
                DatabaseAssemblyPath = @"C:\Users\xfore\Documents\Source\AssetManager\AssetManager.Sqlite\bin\Debug\netcoreapp2.2\AssetManager.Sqlite.dll"
            };

            AssetManagerApi api = new AssetManagerApi( settings );

            {
                AssetTypeBuilder builder = new AssetTypeBuilder( "Pokemon Card" );

                AttributeBuilder hpAttribute = new AttributeBuilder
                {
                    Key = "HP",
                    Type = AttributeTypes.Integer
                };
                builder.KeyValueAttributeKeys.Add( hpAttribute );

                AttributeBuilder retreatCostBuilder = new AttributeBuilder
                {
                    Key = "Retreat Cost",
                    Type = AttributeTypes.Integer
                };
                builder.KeyValueAttributeKeys.Add( retreatCostBuilder );

                api.DataBase.AddAssetType( builder );
            }

            {
                AssetTypeBuilder builder = new AssetTypeBuilder( "Yugioh! Card" );

                AttributeBuilder attackBuilder = new AttributeBuilder
                {
                    Key = "Attack",
                    Type = AttributeTypes.Integer
                };
                builder.KeyValueAttributeKeys.Add( attackBuilder );

                AttributeBuilder defenseBuilder = new AttributeBuilder
                {
                    Key = "Defense",
                    Type = AttributeTypes.Integer
                };
                builder.KeyValueAttributeKeys.Add( defenseBuilder );

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
        }
    }
}
