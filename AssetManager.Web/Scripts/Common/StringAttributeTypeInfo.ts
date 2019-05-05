//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class StringAttributeTypeInfo {

    // ---------------- Fields ----------------

    private defaultValue: string;

    private required: boolean;

    // ---------------- Constructor ----------------

    constructor(){
        this.defaultValue = null;
        this.required = false;

        this.AttributeType = AttributeType.StringAttribute;
    }

    // ---------------- Properties ----------------

    public readonly AttributeType: AttributeType;

    // ---------------- Functions ----------------

    public Validate(): Array<string> {
        // Nothing to validate.
        return null;
    }

    // -------- Setters --------

    public SetDefault(def: string): StringAttributeTypeInfo {
        // Empty string will be null.
        if (def === "") {
            this.defaultValue = null;
        }
        else {
            this.defaultValue = def;
        }

        return this;
    }

    public SetRequired(newValue: boolean): StringAttributeTypeInfo {
        this.required = newValue;
        return this;
    }

    // -------- Getters--------

    public GetDefaultValue(): string {
        return this.defaultValue;
    }

    public GetRequired(): boolean {
        return this.required;
    }
}