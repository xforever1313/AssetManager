//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System.Collections.Generic;
using AssetManager.Api;
using Newtonsoft.Json;

namespace AssetManager.Web.Models
{
    /// <summary>
    /// This is the model of an asset that comes from the webpage.
    /// </summary>
    [JsonConverter( typeof( AssetBuilderConverter ) )]
    public class AssetBuilderModel
    {
        // ---------------- Constructor ----------------

        public AssetBuilderModel()
        {
            this.Attributes = new List<KeyValuePair<string, IAttribute>>();
            this.ErrorMessage = string.Empty;
            this.AssetName = "Unknown Asset";
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// The attributes to add to the Asset.
        /// </summary>
        /// <remarks>
        /// For some dumb reason, we can not make this a dictionary, since otherwise ASP.NET gets angry and reports
        /// Cannot deserialize the current JSON array (e.g. [1,2,3]) into type 'System.Collections.Generic.IDictionary'
        /// even though we use our own JSON converter *shrug*.
        /// </remarks>
        public IList<KeyValuePair<string, IAttribute>> Attributes { get; private set; }

        public string AssetName { get; set; }

        /// <summary>
        /// Did we get the information correctly?
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Were there any errors?  Empty if <see cref="Success"/> is set to true.
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
