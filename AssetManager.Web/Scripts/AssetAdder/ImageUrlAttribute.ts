//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class ImageUrlAttribute extends BaseAttribute {

    // ---------------- Fields ----------------

    private readonly info: ImageUrlAttributeTypeInfo;

    private value: string;
    private width: Number;
    private height: Number;

    // ---------------- Constructor ----------------

    constructor(info: ImageUrlAttributeTypeInfo, key: string) {
        super(info.AttributeType, key);
        this.info = info;

        this.value = "";
    }

    // ---------------- Functions ----------------

    public Validate(): Array<string> {
        let errors = new Array<string>();

        if (this.info.GetRequired()) {
            if (Helpers.IsNullOrUndefined(this.value)) {
                errors.push("No value specified.");
            }
        }

        if (Helpers.IsNotNullOrUndefined(this.width)) {
            if (this.width < 0) {
                errors.push("Width can not be less than 0 when specified.");
            }
        }

        if (Helpers.IsNotNullOrUndefined(this.height)) {
            if (this.height < 0) {
                errors.push("Height can not be less than 0 when specified.");
            }
        }

        return errors;
    }

    public ToJson(): object {
        let data = {
            "Key": this.Key,
            "AttributeType": this.AttributeType,
            "Value": {
                "SchemaVersion": 1,
                "Width": this.GetWidth(),
                "Height": this.GetHeight(),
                "Value": this.GetValue()
            }
        };

        return data;
    }

    // -------- Setters --------

    public SetValue(newValue: string): ImageUrlAttribute {
        if (Helpers.StringIsNullOrEmpty(newValue)) {
            this.value = null;
        }
        else {
            this.value = newValue;
        }
        return this;
    }

    public SetWidth(newValue: number): ImageUrlAttribute {
        if (isNaN(newValue)) {
            this.width = null;
        }
        else {
            this.width = newValue;
        }
        return this;
    }

    public SetHeight(newValue: number): ImageUrlAttribute {
        if (isNaN(newValue)) {
            this.height = null;
        }
        else {
            this.height = newValue;
        }
        return this;
    }

    // -------- Getters --------

    public GetValue(): string {
        return this.value;
    }

    public GetWidth(): Number {
        return this.width;
    }

    public GetHeight(): Number {
        return this.height;
    }
}