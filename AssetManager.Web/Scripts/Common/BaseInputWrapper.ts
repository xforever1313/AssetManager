//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

abstract class BaseInputWrapper implements IInputWrapper {

    // ---------------- Fields ----------------

    protected readonly input: HTMLInputElement;

    // ---------------- Constructor ----------------

    constructor(input: HTMLInputElement) {
        this.input = input;
    }

    // ---------------- Functions ----------------

    public Enable(): void {
        this.input.readOnly = false;
    }

    public Disable(): void {
        this.input.readOnly = true;
    }

    public abstract SyncValue(): void;
}