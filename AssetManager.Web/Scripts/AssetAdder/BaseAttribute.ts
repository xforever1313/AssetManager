//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

abstract class BaseAttribute implements IAttribute {

    // ---------------- Fields ----------------

    // ---------------- Constructor ----------------

    constructor(attrType: AttributeType, key: string) {
        this.AttributeType = attrType;
        this.Key = key;
    }

    // ---------------- Properties ----------------

    readonly AttributeType: AttributeType;

    readonly Key: string;

    // ---------------- Functions ----------------

    public abstract Validate(): Array<string>;

    public abstract ToJson(): object;
}
