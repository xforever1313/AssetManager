//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class AssetNameAttribute extends BaseAttribute {

    // ---------------- Constructor ----------------

    constructor() {
        super("Asset Name", true);

        let helpDiv = <HTMLDivElement>(document.createElement("div"));
        helpDiv.className = "form-group";
        this.AppendChild(helpDiv);

        helpDiv.innerText = "The name of the instance of the asset that will be added.  This is required whenever adding a new asset instance.  " +
            "The name should imply that it is unique, such as 'Town Name' if dealing with towns or 'Hostname' if dealing with servers.";
    }

    // ---------------- Functions ----------------

    public ValidateChild(): Array<string> {
        // Nothing to validate.
        return null;
    }
}