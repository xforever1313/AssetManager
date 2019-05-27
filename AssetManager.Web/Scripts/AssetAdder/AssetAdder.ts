//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class AssetAdder {

    // ---------------- Fields ----------------

    readonly attributes: Array<IAttribute>;
    readonly inputs: Array<IInputWrapper>;

    readonly databaseId: string;
    readonly assetTypeId: number;

    // ---------------- Constructor ----------------

    constructor(databaseId: string, assetTypeId: number) {
        this.attributes = new Array<IAttribute>();
        this.inputs = new Array<IInputWrapper>();
        this.databaseId = databaseId;
        this.assetTypeId = assetTypeId;
    }

    // ---------------- Properties ----------------

    // ---------------- Functions ----------------

    public AddAttribute(attribute: IAttribute, input: IInputWrapper) {
        this.attributes.push(attribute);
        this.inputs.push(input);
    }

    public Validate(): boolean {
        let success: boolean = true;
        for (let i = 0; i < this.attributes.length; ++i) {

            // First, sync all values
            this.inputs[i].SyncValue();

            // Then validate, and display any errors.
            let errors: Array<string> = this.attributes[i].Validate();

            this.inputs[i].DisplayErrors(errors);
            if (errors.length !== 0) {
                success = false;
            }
        }

        return success;
    }

    public Submit(): void {
        if (this.Validate() === false) {
            this.EnableForm();
        }
        else {
            this.DisableForm();

            const dataType = "application/json; charset=utf-8";
            let data = {
                AttributeList: new Array<object>()
            };

            for (let i = 0; i < this.attributes.length; ++i) {
                data.AttributeList.push(this.attributes[i].ToJson());
            }

            let jsonString: string = JSON.stringify(data);
            console.log(jsonString);

            let adder = this;
            let xhr = new XMLHttpRequest();
            let url = "/Assets/Add/" + adder.databaseId + "/" + adder.assetTypeId;
            xhr.open("POST", url);
            xhr.onreadystatechange = function () {
                if (xhr.readyState == XMLHttpRequest.DONE) {
                    // If we are successful, return to the asset screen.
                    if (xhr.status === 200) {
                        window.location.href = "/Assets/List/" + adder.databaseId + "/" + adder.assetTypeId;
                    }
                    else {
                        alert(xhr.responseText);
                        adder.EnableForm();
                    }
                }
            };

            xhr.setRequestHeader("Content-Type", dataType);
            xhr.send(jsonString);
        }
    }

    private EnableForm(): void {
        for (let input of this.inputs) {
            input.Enable();
        }
    }

    private DisableForm(): void {
        for (let input of this.inputs) {
            input.Disable();
        }
    }
}
