//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using AssetManager.Web.Models;
using Newtonsoft.Json.Linq;

namespace AssetManager.Web
{
    public class AssetTypeBuilderConverter : JsonCreationConverter<AssetTypeBuilderModel>
    {
        // ---------------- Constructor ----------------

        public AssetTypeBuilderConverter() :
            base()
        {
        }

        // ---------------- Functions ----------------

        protected override AssetTypeBuilderModel Create( Type objectType, JObject rootNode )
        {
            if( rootNode == null )
            {
                throw new ArgumentNullException( nameof( rootNode ) );
            }

            AssetTypeBuilderModel builder = new AssetTypeBuilderModel();
            try
            {
                builder.Deserialize( rootNode );
                builder.Success = true;
                builder.ErrorMessage = string.Empty;
            }
            catch( Exception e )
            {
                builder.Success = false;
                builder.ErrorMessage = e.Message;
            }
            return builder;
        }
    }
}
