//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class AssetNameAttribute extends BaseAttribute {

    // ---------------- Fields ----------------

    private value: string;

    // ---------------- Constructor ----------------

    constructor(info: AssetNameAttributeTypeInfo, key: string) {
        super(info.AttributeType, key)
    }

    // ---------------- Functions ----------------

    public Validate(): Array<string> {
        let errors = new Array<string>();

        if (Helpers.StringIsNullOrEmpty(this.value)) {
            errors.push("No value specified.");
        }

        return errors;
    }

    public ToJson(): object {
        let data = {
            "Key": this.Key,
            "AttributeType": this.AttributeType,
            "Value": this.GetValue()
        };

        return data;
    }

    // -------- Setters --------

    public SetValue(newValue: string): AssetNameAttribute {
        if (newValue === "") {
            this.value = null;
        }
        else {
            this.value = newValue;
        }
        return this;
    }

    // -------- Getters --------

    public GetValue(): string {
        return this.value;
    }
}