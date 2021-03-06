﻿//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

interface IAttribute extends IValidator {

    // ---------------- Properties -----------------

    readonly AttributeType: AttributeType;

    // ---------------- Functions -----------------

    /**
     * Ensures the attribute is in a valid state.
     * Return a list of errors, or null if there are none.
     **/
    Validate(): Array<string>;

    ToJson(): object;
}
