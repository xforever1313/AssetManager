//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class IntegerAttribute extends BaseAttribute {

    // ---------------- Fields ----------------

    private readonly info: IntegerAttributeTypeInfo;

    private value: Number;

    // ---------------- Constructor ----------------

    constructor(info: IntegerAttributeTypeInfo, key: string) {
        super(info.AttributeType, key)
        this.info = info;

        this.value = info.GetDefault();
    }

    // ---------------- Functions ----------------

    public Validate(): Array<string> {
        let errors = new Array<string>();

        if (this.info.GetRequired()) {
            if (Helpers.IsNullOrUndefined(this.value)) {
                errors.push("No value specified.");
            }
        }

        const min = this.info.GetMin();
        const max = this.info.GetMax();

        if (Helpers.IsNotNullOrUndefined(min)){
            if (this.value < min) {
                errors.push("Value is less than the minimum value of " + min);
            }
        }

        if (Helpers.IsNotNullOrUndefined(max)) {
            if (this.value > max) {
                errors.push("Value is greater than the maximum value of " + max);
            }
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

    public SetValue(newValue: number): IntegerAttribute {
        if (isNaN(newValue)) {
            this.value = null;
        }
        else {
            this.value = newValue;
        }
        return this;
    }

    // -------- Getters --------

    public GetValue(): Number {
        return this.value;
    }
}