//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using AssetManager.Api;

namespace AssetManager.Web.Models
{
    public class AttributeTypeModel
    {
        // ---------------- Constructor ----------------

        public AttributeTypeModel()
        {
        }

        // ---------------- Properties ----------------

        public string Key { get; set; }

        public AttributeTypes AttributeType { get; set; }
    }
}
