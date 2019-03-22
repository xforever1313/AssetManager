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
    private readonly submitButton: HTMLButtonElement;
    private readonly addAssetButton: HTMLButtonElement;

    private assetTypeName: string;

    // ---------------- Constructor ----------------

    constructor(
        textBox: HTMLInputElement,
        errorDiv: HTMLDivElement,
        submitButton: HTMLButtonElement,
        addAssetButton: HTMLButtonElement
    ) {
        this.textBox = textBox;
        this.errorDiv = errorDiv;
        this.submitButton = submitButton;
        this.addAssetButton = addAssetButton;

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

    public EnableForm(): void {
        this.textBox.readOnly = false;
        this.submitButton.disabled = false;
        this.addAssetButton.disabled = false;
    }

    public DisableForm(): void {
        this.textBox.readOnly = true;
        this.submitButton.disabled = true;
        this.addAssetButton.disabled = true;
    }
}