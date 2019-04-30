//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

interface IAttribute {

    // ---------------- Functions -----------------

    EnableForm(): void;

    DisableForm(): void;

    /**
     * Ensures the attribute is in a valid state.
     **/
    Validate(): boolean;

    ToJson(): object;
}
