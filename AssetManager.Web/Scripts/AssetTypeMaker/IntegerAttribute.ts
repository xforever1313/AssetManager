//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class IntegerAttribute extends BaseAttribute {

    // ---------------- Constructor ----------------

    constructor() {
        super("Integer Attribute");

        let helpDiv = <HTMLDivElement>(document.createElement("div"));
        helpDiv.className = "form-group";
        this.AppendChild(helpDiv);

        helpDiv.innerText = "An integer attribute is a key-value pair, " +
            "where the key is the attribute name, and the value is an interger.  " +
            "The key is set here, but the value is set by the user.  An example of this could be " +
            "having the attribute name be 'Population', and the value would be '10000'.";
    }

    // ---------------- Functions ----------------

    public ValidateChild(): Array<string> {
        // Nothing to validate.
        return null;
    }
}