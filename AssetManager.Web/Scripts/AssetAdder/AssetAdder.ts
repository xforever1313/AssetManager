//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class AssetAdder {

    // ---------------- Fields ----------------

    readonly attributes: Array<IAttribute>;

    // ---------------- Constructor ----------------

    constructor() {
        this.attributes = new Array<IAttribute>();
    }

    // ---------------- Properties ----------------

    // ---------------- Functions ----------------

    public AddAttribute(attribute: IAttribute) {
        this.attributes.push(attribute);
    }
}
