//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class AssetNameAttributeType extends BaseAttributeType {

    // ---------------- Constructor ----------------

    constructor() {
        super("Asset Name", AttributeType.AssetNameAttribute, true);

        let helpDiv = <HTMLDivElement>(document.createElement("div"));
        helpDiv.className = "form-group";
        this.AppendChild(helpDiv);

        helpDiv.innerText = "The name of the instance of the asset that will be added.  This is required whenever adding a new asset instance.  " +
            "The name should imply that it is unique, such as 'Town Name' if dealing with towns or 'Hostname' if dealing with servers.";
    }

    // ---------------- Functions ----------------

    public ToJson(): object {
        let defaultValue: object = null;
        let possibleValues: object = null;

        let data = {
            "Key": this.GetKey(),
            "AttributeType": this.AttributeType,
            "Required": true, // Always required
            "DefaultValue": defaultValue,
            "PossibleValues": possibleValues
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