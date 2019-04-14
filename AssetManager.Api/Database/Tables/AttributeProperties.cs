using System;
using System.Collections.Generic;
using System.Text;

namespace AssetManager.Api.Database.Tables
{
    /// <summary>
    /// These are properties an attribute can have,
    /// such as if it is required or something else.
    /// </summary>
    public class AttributeProperties
    {
        // ---------------- Constructor ----------------

        public AttributeProperties()
        {
            this.Required = false;
        }

        // ---------------- Properties ----------------

        public int Id { get; set; }

        /// <summary>
        /// Is the attribute a required attribute, or can it be
        /// left blank?
        /// 
        /// Defaulted to false.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// This is a JSON blob of possible values an attribute
        /// can be set to.  It is up to the attribute type
        /// to figure out what this means.
        /// 
        /// This can be null; depending on the attribute type.
        /// </summary>
        public string PossibleValues { get; set; }

        /// <summary>
        /// The default value of the attribute.
        /// This can be null; depending on the attribute.
        /// </summary>
        public string DefaultValue { get; set; }
    }
}
