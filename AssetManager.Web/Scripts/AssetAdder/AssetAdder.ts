//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class AssetAdder {

    // ---------------- Fields ----------------

    readonly attributes: Array<IAttribute>;

    readonly form: HTMLFormElement;

    // ---------------- Constructor ----------------

    constructor() {
        this.attributes = new Array<IAttribute>();
        this.form = document.getElementById("addForm") as HTMLFormElement;
    }

    // ---------------- Properties ----------------

    // ---------------- Functions ----------------

    public AddAttribute(attribute: IAttribute) {
        this.attributes.push(attribute);
    }

    public Validate(): boolean {
        let success: boolean = true;
        for (let attr of this.attributes) {
            if (attr.Validate().length !== 0) {
                success = false;
            }
        }

        return success;
    }

    public Submit(): void {
        if (this.Validate() === false) {

        }
        else {
            this.DisableForm();

            this.form.submit();
        }
    }

    private EnableForm(): void {
        for (let attr of this.attributes) {
            attr.EnableForm();
        }
    }

    private DisableForm(): void {
        for (let attr of this.attributes) {
            attr.DisableForm();
        }
    }
}
