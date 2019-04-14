//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Xml;

namespace AssetManager.Sqlite
{
    public static class XmlLoader
    {
        // ---------------- Fields ----------------

        internal const string DatabaseSettingsNodeName = "database";

        // ---------------- Functions ----------------


        /// <summary>
        /// Overwrites the given settings
        /// config with the settings specified in the passed in XML document.
        /// </summary>
        public static void LoadFromXml( this SqliteDatabaseConfig config, XmlNode parentNode )
        {
            if( parentNode.Name != DatabaseSettingsNodeName )
            {
                throw new XmlException(
                    "Root XML node should be named \"" + DatabaseSettingsNodeName + "\".  Got: " + parentNode.Name
                );
            }

            foreach( XmlNode childNode in parentNode.ChildNodes )
            {
                switch( childNode.Name.ToLower() )
                {
                    case "dblocation":
                        config.DatabaseLocation = childNode.InnerText;
                        break;

                    case "guid":
                        config.DatabaseId = Guid.Parse( childNode.InnerText );
                        break;
                }
            }
        }
    }
}
