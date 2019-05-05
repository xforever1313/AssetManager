//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class StringAttributeTypeInfo {

    // ---------------- Constructor ----------------

    constructor(){
        this.DefaultValue = null;
        this.Required = false;
    }

    // ---------------- Properties ----------------

    public DefaultValue: string;

    public Required: boolean;

    // ---------------- Functions ----------------

    public Validate(): Array<string> {
        // Nothing to validate.
        return null;
    }

    public SetDefault(def: string): void {
        // Empty string will be null.
        if (def === "") {
            this.DefaultValue = null;
        }
        else {
            this.DefaultValue = def;
        }
    }
}