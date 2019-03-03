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
                Asset asset = api.DataBase.GenerateEmptyAsset( "Pokemon Card" );
                asset.Name = "Politoed";
                asset["HP"] = 100.ToString();
                asset["Retreat Cost"] = 3.ToString();

                api.DataBase.AddAsset( asset );
            }

            {
                Asset asset = api.DataBase.GenerateEmptyAsset( "Yugioh! Card" );
                asset.Name = "The 13th Grave";
                asset["Attack"] = 1200;
                asset["Defense"] = 900;

                api.DataBase.AddAsset( asset );
            }

            {
                Asset asset = api.DataBase.GenerateEmptyAsset( "Yugioh! Card" );
                asset.Name = "Overdrive";
                asset["Attack"] = 1600;
                asset["Defense"] = 1500;

                api.DataBase.AddAsset( asset );
            }
        }
    }
}
