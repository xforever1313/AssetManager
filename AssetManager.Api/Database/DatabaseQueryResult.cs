//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AssetManager.Api.Database
{
    public class DatabaseQueryResult<TResult>
    {
        public DatabaseQueryResult( Guid databaseId, TResult result, Exception error )
        {
            this.DatabaseId = databaseId;
            this.Error = error;
            if ( this.Success )
            {
                this.Result = result;
            }
            else
            {
                this.Result = default;
            }
        }

        public Guid DatabaseId { get; private set; }

        /// <summary>
        /// The result of the query.  Set to the default if not successful.
        /// </summary>
        public TResult Result { get; private set; }

        /// <summary>
        /// Was the query successful?
        /// </summary>
        public bool Success
        {
            get
            {
                return this.Error == null;
            }
        }

        /// <summary>
        /// The exception that happened during the query... null if none happened.
        /// </summary>
        public Exception Error { get; private set; }
    }

    public class DatabaseQueryMultiResult<TResult>
    {
        public DatabaseQueryMultiResult( IReadOnlyDictionary<Guid, DatabaseQueryResult<TResult>> results )
        {
            this.Results = results;
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// Result from the multi-database query.
        /// </summary>
        public IReadOnlyDictionary<Guid, DatabaseQueryResult<TResult>> Results { get; private set; }

    }
}
