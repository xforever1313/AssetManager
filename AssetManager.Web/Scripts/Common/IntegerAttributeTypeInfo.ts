//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class IntegerAttributeTypeInfo {

    // ---------------- Fields ----------------

    // ---------------- Constructor ----------------

    constructor() {
        this.SetMin(null);
        this.SetMax(null);
        this.SetDefault(null);
        this.Required = false;
    }

    // ---------------- Properties ----------------

    public MinValue: Number;

    public MaxValue: Number;

    public DefaultValue: Number;

    public Required: boolean;

    // ---------------- Functions ----------------

    public Validate(): Array<string> {
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

    public SetMax(max: number): void {
        if (isNaN(max)) {
            this.MaxValue = null;
        }
        else {
            this.MaxValue = max;
        }
    }

    public SetMin(min: number): void {
        if (isNaN(min)) {
            this.MinValue = null;
        }
        else {
            this.MinValue = min;
        }
    }

    public SetDefault(def: number): void {
        if (isNaN(def)) {
            this.DefaultValue = null;
        }
        else {
            this.DefaultValue = def;
        }
    }
}
