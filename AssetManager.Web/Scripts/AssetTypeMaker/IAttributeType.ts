//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

enum AttributeType {
    AssetNameAttribute = 1,
    StringAttribute = 2,
    IntegerAttribute = 3,
}

interface IAttributeType {

    // ---------------- Events ----------------

    /**
     * Action that is called when the delete button on an attribute
     * is clicked on.
     */
    OnDelete: (attr: IAttributeType) => void;

    // ---------------- Properties -----------------

    AttributeType: AttributeType;

    // ---------------- Functions -----------------

    /**
     * Ensures the attribute is in a valid state.
     **/
    Validate(): boolean;

    GetHtmlDiv(): HTMLDivElement;

    ToJson(): object;
}
