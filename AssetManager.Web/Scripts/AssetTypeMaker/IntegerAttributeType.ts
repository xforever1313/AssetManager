//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class IntegerAttributeType extends BaseAttributeType {

    // ---------------- Constructor ----------------

    constructor() {
        super("Integer Attribute", AttributeType.IntegerAttribute);

        let helpDiv = <HTMLDivElement>(document.createElement("div"));
        helpDiv.className = "form-group";
        this.AppendChild(helpDiv);

        helpDiv.innerText = "An integer attribute is a key-value pair, " +
            "where the key is the attribute name, and the value is an interger.  " +
            "The key is set here, but the value is set by the user.  An example of this could be " +
            "having the attribute name be 'Population', and the value would be '10000'.";
    }

    // ---------------- Functions ----------------

    public ToJson(): object {

        let minValue: Number = null;
        let maxValue: Number = null;
        let defaultValue: Number = null;

        //TODO: Fill in properties.
        let data = {
            "Key": this.GetKey(),
            "AttributeType": this.AttributeType,
            "Required": false,
            "PossibleValues": {
                "Version": 1,
                "MinValue": minValue,
                "MaxValue": maxValue
            },
            "DefaultValue": defaultValue
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