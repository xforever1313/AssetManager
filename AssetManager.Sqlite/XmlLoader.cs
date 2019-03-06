//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System.Xml;

namespace AssetManager.Sqlite
{
    public static class XmlLoader
    {
        // ---------------- Fields ----------------

        internal const string SettingsXmlElementName = "AssetManagerSqliteConfig";

        // ---------------- Functions ----------------

        /// <summary>
        /// Loads the given XML file and overwrites the given settings
        /// config with the settings specified in the XML file.
        /// </summary>
        public static void LoadFromXml( this SqliteDatabaseConfig config, string xmlFilePath )
        {
            XmlDocument doc = new XmlDocument();
            doc.Load( xmlFilePath );

            LoadFromXml( config, doc );
        }

        /// <summary>
        /// Overwrites the given settings
        /// config with the settings specified in the passed in XML document.
        /// </summary>
        public static void LoadFromXml( this SqliteDatabaseConfig config, XmlDocument doc )
        {
            XmlNode rootNode = doc.DocumentElement;
            if( rootNode.Name != SettingsXmlElementName )
            {
                throw new XmlException(
                    "Root XML node should be named \"" + SettingsXmlElementName + "\".  Got: " + rootNode.Name
                );
            }

            foreach( XmlNode childNode in rootNode.ChildNodes )
            {
                switch( childNode.Name.ToLower() )
                {
                    case "dblocation":
                        config.DatabaseLocation = childNode.InnerText;
                        break;
                }
            }
        }
    }
}
