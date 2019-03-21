//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using AssetManager.Api;
using Newtonsoft.Json;

namespace AssetManager.Web.Models
{
    [JsonConverter( typeof( AssetTypeBuilderConverter ) )]
    public class AssetTypeBuilderModel : AssetTypeBuilder
    {
        // ---------------- Constructor ----------------

        public AssetTypeBuilderModel() :
            base()
        {
        }

        public AssetTypeBuilderModel( string name ) :
            base( name )
        {
        }
    }
}
