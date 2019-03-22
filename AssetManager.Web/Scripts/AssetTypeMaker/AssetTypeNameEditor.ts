//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class AssetTypeNameEditor {

    // ---------------- Fields ----------------

    private readonly textBox: HTMLInputElement;

    private readonly errorDiv: HTMLDivElement;

    private assetTypeName: string;

    // ---------------- Constructor ----------------

    constructor(textBox: HTMLInputElement, errorDiv: HTMLDivElement) {
        this.textBox = textBox;
        this.errorDiv = errorDiv;

        this.assetTypeName = "";

        this.textBox.oninput = (() => {
            let contents: string = this.textBox.value;
            if (Helpers.StringIsNullOrEmpty(contents)) {
                this.assetTypeName = "";
            }
            else {
                this.assetTypeName = contents;
            }
        });
    }

    // ---------------- Functions ----------------

    public Validate(): boolean {
        if (this.assetTypeName.length === 0) {
            this.errorDiv.className = "alert alert-danger";
            this.errorDiv.style.marginTop = "1em";
            this.errorDiv.innerText = "Asset Type Name can not be empty.";
            return false;
        }
        else {
            this.errorDiv.innerText = "";
            this.errorDiv.className = "";
            this.errorDiv.style.marginTop = "";
            return true;
        }
    }

    public GetName(): string {
        return this.assetTypeName;
    }
}