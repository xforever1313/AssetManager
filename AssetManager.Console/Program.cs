using System;
using System.IO;
using AssetManager.Api;

namespace AssetManager.Cli
{
    class Program
    {
        static void Main( string[] args )
        {
            File.Delete( @"C:\Users\xfore\Downloads\assetmanager.db" );

            AssetManagerApi api = new AssetManagerApi();

            {
                AssetTypeBuilder builder = new AssetTypeBuilder( "Pokemon Card" );
                builder.KeyValueAttributeKeys.Add( "HP" );
                builder.KeyValueAttributeKeys.Add( "Retreat Cost" );
                api.DataBase.AddAssetType( builder );
            }

            {
                AssetTypeBuilder builder = new AssetTypeBuilder( "Yugioh! Card" );
                builder.KeyValueAttributeKeys.Add( "Attack" );
                builder.KeyValueAttributeKeys.Add( "Defense" );
                api.DataBase.AddAssetType( builder );
            }

            {
                Asset asset = new Asset
                {
                    AssetType = "Pokemon Card",
                    Name = "Politoed"
                };
                asset["HP"] = 100.ToString();
                asset["Retreat Cost"] = 3.ToString();

                api.DataBase.AddAsset( asset );
            }

            {
                Asset asset = new Asset
                {
                    AssetType = "Yugioh! Card",
                    Name = "The 13th Grave"
                };
                asset["Attack"] = 1200.ToString();
                asset["Defense"] = 900.ToString();

                api.DataBase.AddAsset( asset );
            }

            {
                Asset asset = new Asset
                {
                    AssetType = "Yugioh! Card",
                    Name = "Overdrive"
                };
                asset["Attack"] = 1600.ToString();
                asset["Defense"] = 1500.ToString();

                api.DataBase.AddAsset( asset );
            }
        }
    }
}
