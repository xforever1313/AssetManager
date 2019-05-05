//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class StringAttribute extends BaseAttribute {

    // ---------------- Fields ----------------

    private readonly info: StringAttributeTypeInfo;

    private value: string;

    // ---------------- Constructor ----------------

    constructor(info: StringAttributeTypeInfo) {
        super(info.AttributeType)
        this.info = info;

        this.value = this.info.GetDefaultValue();
    }

    // ---------------- Functions ----------------

    public Validate(): Array<string> {
        let errors = new Array<string>();

        if (this.info.GetRequired()) {
            if (Helpers.StringIsNullOrEmpty(this.value)) {
                errors.push("No value specified.");
            }
        }

        return errors;
    }

    public ToJson(): object {
        return null; // For now.
    }

    // -------- Setters --------

    public SetValue(newValue: string): StringAttribute {
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