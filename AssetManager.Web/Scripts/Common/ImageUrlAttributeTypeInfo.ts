//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class ImageUrlAttributeTypeInfo {

    // ---------------- Fields ----------------

    private required: boolean;

    // ---------------- Constructor ----------------

    constructor() {
        this.AttributeType = AttributeType.ImageUrl;
        this.required = false;
    }

    // ---------------- Properties ----------------

    public readonly AttributeType: AttributeType;

    // ---------------- Functions ----------------

    public Validate(): Array<string> {
        // Nothing to validate.
        return null;
    }

    // -------- Setters --------

    public SetRequired(newValue: boolean): ImageUrlAttributeTypeInfo {
        this.required = newValue;
        return this;
    }

    // -------- Getters --------

    public GetRequired(): boolean {
        return this.required;
    }
}