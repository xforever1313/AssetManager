//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class StringAttributeType extends BaseAttributeType {

    // ---------------- Constructor ----------------

    constructor() {
        super("String Attribute", AttributeType.StringAttribute);

        let helpDiv = <HTMLDivElement>(document.createElement("div"));
        helpDiv.className = "form-group";
        this.AppendChild(helpDiv);

        helpDiv.innerText = "A string attribute is a key-value pair, " +
            "where the key is the attribute name, and the value is a string of " +
            "any size.  The key is set here, but the value is set by the user.  An example of this could be " +
            "having the attribute name be 'Location', and the value would be 'New York'.";
    }

    // ---------------- Properties ----------------

    public DefaultValue: string;

    // ---------------- Functions ----------------

    public ToJson(): object {
        let possibleValues: object = null;

        let data = {
            "Key": this.GetKey(),
            "AttributeType": this.AttributeType,
            "Required": false,
            "PossibleValues": possibleValues,
            "DefaultValue": this.DefaultValue
        };

        return data;
    }

    public ValidateChild(): Array<string> {
        // Nothing to validate.
        return null;
    }

    protected EnableFormInternal(): void {
        // Nothing to do.
    }

    protected DisableFormInternal(): void {
        // Nothing to do.
    }
}