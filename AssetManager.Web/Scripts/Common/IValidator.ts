//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

interface IValidator {

    // ---------------- Functions ----------------

    /**
     * Validates the implementor.  Any error messages will return
     * in the array.
     * If there are no validation errors, this will return an empty array or null.
     **/
    Validate(): Array<string>;
}