//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class TextboxWrapper extends BaseInputWrapper {

    // ---------------- Events ----------------

    public OnSyncValue: (attr: string) => void;

    // ---------------- Constructor ----------------

    constructor(input: HTMLInputElement, errorDiv: HTMLDivElement) {
        super(input, errorDiv)
    }

    // ---------------- Functions ----------------

    public SyncValue(): void {
        if (this.OnSyncValue) {
            this.OnSyncValue(this.input.value);
        }
    }
}
