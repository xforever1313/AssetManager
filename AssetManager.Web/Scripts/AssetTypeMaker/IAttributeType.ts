//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

interface IAttributeType {

    // ---------------- Events ----------------

    /**
     * Action that is called when the delete button on an attribute
     * is clicked on.
     */
    OnDelete: (attr: IAttributeType) => void;

    /**
     * Ensures the attribute is in a valid state.
     **/
    Validate(): boolean;

    GetHtmlDiv(): HTMLDivElement;
}
