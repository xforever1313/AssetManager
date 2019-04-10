//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssetManager.Api;

namespace AssetManager.Web.Models
{
    /// <summary>
    /// Interface that ALL models must inherit from.
    /// </summary>
    public interface IModel
    {
        /// <summary>
        /// A reference to the API object.
        /// </summary>
        IAssetManagerApi Api { get; }
    }
}
