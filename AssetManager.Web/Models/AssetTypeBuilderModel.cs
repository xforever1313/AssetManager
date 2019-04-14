//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
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

        public AssetTypeBuilderModel( string name, Guid databaseId ) :
            base( name, databaseId )
        {
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// Did we create the asset type correctly?
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Were there any errors.  Empty if <see cref="Success"/> is set to true.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
