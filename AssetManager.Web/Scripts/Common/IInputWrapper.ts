//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

/**
 * This interface wraps inputs and provides helper functions for them.
 * */
interface IInputWrapper {

    // ---------------- Functions ----------------

    /**
     * Enables the input element so it is read/write
     **/
    Enable(): void;

    /**
     * Disables the input element so it is read-only
     **/
    Disable(): void;

    /**
     * Call this to take the value of the wrapped input and save it somewhere.
     **/
    SyncValue(): void;

    /**
     * Displays error messages near the input.
     * @param messages - Messages to display.  Pass in null, undefined, or an empty array to show none.
     */
    DisplayErrors(messages: Array<string>): void;
}