//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class IntegerAttributeTypeInfo {

    // ---------------- Fields ----------------

    private minValue: Number;

    private maxValue: Number;

    private defaultValue: Number;

    private required: boolean;

    // ---------------- Constructor ----------------

    constructor() {
        this.SetMin(null);
        this.SetMax(null);
        this.SetDefault(null);
        this.required = false;
    }

    // ---------------- Functions ----------------

    public Validate(): Array<string> {
        let errors: Array<string> = new Array<string>();

        if (Helpers.IsNotNullOrUndefined(this.minValue) && Helpers.IsNotNullOrUndefined(this.maxValue)) {
            if (this.minValue > this.maxValue) {
                errors.push("Min Value can not be greater than the maximum value.");
            }
        }

        if (Helpers.IsNotNullOrUndefined(this.defaultValue)) {
            if (Helpers.IsNotNullOrUndefined(this.minValue)) {
                if (this.minValue > this.defaultValue) {
                    errors.push("The default value can not be less than the minimum value.")
                }
            }

            if (Helpers.IsNotNullOrUndefined(this.maxValue)) {
                if (this.maxValue < this.defaultValue) {
                    errors.push("The default value can not be greater than the maximum value.")
                }
            }
        }

        return errors;
    }

    // -------- Setters --------

    public SetMax(max: number): IntegerAttributeTypeInfo {
        if (isNaN(max)) {
            this.maxValue = null;
        }
        else {
            this.maxValue = max;
        }

        return this;
    }

    public SetMin(min: number): IntegerAttributeTypeInfo {
        if (isNaN(min)) {
            this.minValue = null;
        }
        else {
            this.minValue = min;
        }

        return this;
    }

    public SetDefault(def: number): IntegerAttributeTypeInfo {
        if (isNaN(def)) {
            this.defaultValue = null;
        }
        else {
            this.defaultValue = def;
        }

        return this;
    }

    public SetRequired(newValue: boolean): IntegerAttributeTypeInfo {
        this.required = newValue;
        return this;
    }

    // -------- Getters --------

    public GetMax(): Number {
        return this.maxValue;
    }

    public GetMin(): Number {
        return this.minValue;
    }

    public GetDefault(): Number {
        return this.defaultValue;
    }

    public GetRequired(): boolean {
        return this.required;
    }
}
