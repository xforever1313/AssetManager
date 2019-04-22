//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.IO;
using System.Linq;
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
                IAssetManagerApi api = AssetManagerApiFactory.CreateApiFromDefaultConfigFile();

                AddTradingCards( api );
                AddVideoGames( api );
                return 0;
            }
            catch( Exception e )
            {
                Console.WriteLine( "UNHANDLED EXCEPTION:" );
                Console.Write( e.ToString() );
                return -1;
            }
        }

        private static void AddVideoGames( IAssetManagerApi api )
        {
            if ( File.Exists( @"C:\Users\xfore\Downloads\VideoGames.db" ) )
            {
                File.Delete( @"C:\Users\xfore\Downloads\VideoGames.db" );
            }

            Guid databaseId = api.DataBase.DatabaseNames.First( n => n.Value == "VideoGames" ).Key;

            AssetTypeBuilder builder = new AssetTypeBuilder( "PC Games", databaseId );

            AssetNameAttributeType assetNameType = new AssetNameAttributeType
            {
                Key = "Title",
                Required = true
            };
            builder.AttributeTypes.Add( assetNameType );

            IntegerAttributeType releaseYear = new IntegerAttributeType
            {
                Key = "Release Year",
                Required = true
            };
            builder.AttributeTypes.Add( releaseYear );

            int pcGameId = api.DataBase.AddAssetType( builder );

            {
                Asset asset = api.DataBase.GenerateEmptyAsset( databaseId, pcGameId );
                asset.SetAttribute( "Title", new AssetNameAttribute( "Command and Conquer" ) );
                asset.SetAttribute( "Release Year", new IntegerAttribute { Value = 1995 } );
                api.DataBase.AddAsset( asset );
            }
        }

        private static void AddTradingCards( IAssetManagerApi api )
        {
            if ( File.Exists( @"C:\Users\xfore\Downloads\TradingCards.db" ) )
            {
                File.Delete( @"C:\Users\xfore\Downloads\TradingCards.db" );
            }

            Guid databaseId = api.DataBase.DatabaseNames.First( n => n.Value == "TradingCards" ).Key;

            int pokemonCardTypeId = -1;
            {
                AssetTypeBuilder builder = new AssetTypeBuilder( "Pokemon Card", databaseId );

                AssetNameAttributeType assetNameType = new AssetNameAttributeType
                {
                    Key = "Card Name",
                    Required = true
                };
                builder.AttributeTypes.Add( assetNameType );

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

                StringAttributeType flavorText = new StringAttributeType
                {
                    Key = "Flavor Text",
                    Required = true
                };
                builder.AttributeTypes.Add( flavorText );

                pokemonCardTypeId = api.DataBase.AddAssetType( builder );
            }

            int yugiohCardTypeId = -1;
            {
                AssetTypeBuilder builder = new AssetTypeBuilder( "Yugioh! Card", databaseId );

                AssetNameAttributeType assetNameType = new AssetNameAttributeType
                {
                    Key = "Card Name",
                    Required = true
                };
                builder.AttributeTypes.Add( assetNameType );

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

                yugiohCardTypeId = api.DataBase.AddAssetType( builder );
            }

            {
                Asset asset = api.DataBase.GenerateEmptyAsset( databaseId, pokemonCardTypeId );
                asset.SetAttribute( "Card Name", new AssetNameAttribute( "Politoed" ) );
                asset.SetAttribute( "HP", new IntegerAttribute() { Value = 100 } );
                asset.SetAttribute( "Retreat Cost", new IntegerAttribute() { Value = 3 } );
                asset.SetAttribute(
                    "Flavor Text",
                    new StringAttribute
                    {
                        Value =
@"Whenever 3 or more of these get together,
they sing in an lound voice that sounds like bellowing."
                    }
                );

                api.DataBase.AddAsset( asset );
            }

            {
                Asset asset = api.DataBase.GenerateEmptyAsset( databaseId, yugiohCardTypeId );
                asset.SetAttribute( "Card Name", new AssetNameAttribute( "The 13th Grave" ) );

                IntegerAttribute attackAttr = asset.CloneAttributeAsType<IntegerAttribute>( "Attack" );
                attackAttr.Value = 1200;
                asset.SetAttribute( "Attack", attackAttr );

                IntegerAttribute defenseAttr = asset.CloneAttributeAsType<IntegerAttribute>( "Defense" );
                defenseAttr.Value = 900;
                asset.SetAttribute( "Defense", attackAttr );

                api.DataBase.AddAsset( asset );
            }

            {
                Asset asset = api.DataBase.GenerateEmptyAsset( databaseId, yugiohCardTypeId );
                asset.SetAttribute( "Card Name", new AssetNameAttribute( "Overdrive" ) );
                asset.SetAttribute( "Attack", new IntegerAttribute( 1600 ) );
                asset.SetAttribute( "Defense", new IntegerAttribute( 1500 ) );

                api.DataBase.AddAsset( asset );
            }
        }
    }
}
