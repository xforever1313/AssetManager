//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System.Net;
using AssetManager.Api;

namespace AssetManager.Web.Models
{
    /// <summary>
    /// Model for displaying errors.
    /// Not to be confused with ASP.net's default
    /// <see cref="ErrorViewModel"/>.
    /// </summary>
    public class ErrorModel : IModel
    {
        // ---------------- Constructor ----------------

        public ErrorModel( IAssetManagerApi api )
        {
            this.Message = "Unknown Error :(";
            this.HttpStatusCode = HttpStatusCode.InternalServerError;
            this.Api = api;
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// Message to display to the user.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The HTTP Status code to report.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; }

        /// <summary>
        /// Reference to the database API.
        /// </summary>
        public IAssetManagerApi Api { get; private set; }
    }
}
