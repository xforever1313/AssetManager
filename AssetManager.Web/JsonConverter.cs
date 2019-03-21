//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AssetManager.Web
{
    /// <summary>
    /// This class helps create JSON we can use to convert
    /// information from the webpage to our C# objects here.
    /// </summary>
    /// <remarks>
    /// Some code borrowed from here: https://www.tutorialdocs.com/article/webapi-data-binding.html
    /// </remarks>
    public abstract class JsonCreationConverter<TOuput> : JsonConverter
    {
        // ---------------- Constructor ----------------

        protected JsonCreationConverter()
        {
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// Read-only, returns false.
        /// </summary>
        public override bool CanWrite { get { return false; } }

        public override bool CanRead { get { return true; } }

        // ---------------- Functions ----------------

        public override bool CanConvert( Type objectType )
        {
            return typeof( TOuput ).IsAssignableFrom( objectType );
        }

        public override object ReadJson( JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer )
        {
            if( reader == null )
            {
                throw new ArgumentNullException( nameof( reader ) );
            }

            if( serializer == null )
            {
                throw new ArgumentNullException( nameof( serializer ) );
            }

            if( reader.TokenType == JsonToken.Null )
            {
                return null;
            }

            JObject jObject = JObject.Load( reader );
            TOuput target = Create( objectType, jObject );
            serializer.Populate( jObject.CreateReader(), target );
            return target;
        }

        public override void WriteJson( JsonWriter writer, object value, JsonSerializer serializer )
        {
            throw new InvalidOperationException( "Can not write this JSON, only read from it" );
        }

        protected abstract TOuput Create( Type objectType, JObject jObject );
    }
}
