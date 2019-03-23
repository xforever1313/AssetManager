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

    // ---------------- Properties ----------------

    public MinValue: Number;

    public MaxValue: Number;

    public DefaultValue: Number;

    // ---------------- Functions ----------------

    public ToJson(): object {
        let data = {
            "Key": this.GetKey(),
            "AttributeType": this.AttributeType,
            "Required": false,
            "PossibleValues": {
                "Version": 1,
                "MinValue": this.MinValue,
                "MaxValue": this.MaxValue
            },
            "DefaultValue": this.DefaultValue
        };

        return data;
    }

    public ValidateChild(): Array<string> {
        let errors: Array<string> = new Array<string>();

        if (Helpers.IsNotNullOrUndefined(this.MinValue) && Helpers.IsNotNullOrUndefined(this.MaxValue)) {
            if (this.MinValue > this.MaxValue) {
                errors.push("Min Value can not be greater than the maximum value.");
            }
        }

        if (Helpers.IsNotNullOrUndefined(this.DefaultValue)) {
            if (Helpers.IsNotNullOrUndefined(this.MinValue)) {
                if (this.MinValue > this.DefaultValue) {
                    errors.push("The default value can not be less than the minimum value.")
                }
            }

            if (Helpers.IsNotNullOrUndefined(this.MaxValue)) {
                if (this.MaxValue < this.DefaultValue) {
                    errors.push("The default value can not be greater than the maximum value.")
                }
            }
        }

        return errors;
    }

    protected EnableFormInternal(): void {
        // Nothing to do.
    }

    protected DisableFormInternal(): void {
        // Nothing to do.
    }
}