//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class AssetAdder {

    // ---------------- Fields ----------------

    readonly attributes: Array<AttributeInputMapper>;

    readonly databaseId: string;
    readonly assetTypeId: number;

    // ---------------- Constructor ----------------

    constructor(databaseId: string, assetTypeId: number) {
        this.attributes = new Array<AttributeInputMapper>();
        this.databaseId = databaseId;
        this.assetTypeId = assetTypeId;
    }

    // ---------------- Properties ----------------

    // ---------------- Functions ----------------

    public AddAttribute(attribute: AttributeInputMapper) {
        this.attributes.push(attribute);
    }

    public Validate(): boolean {
        let success: boolean = true;
        for (let i = 0; i < this.attributes.length; ++i) {
            success = this.attributes[i].Validate() && success;
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
                data.AttributeList.push(this.attributes[i].GetValidator().ToJson());
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
        for (let attr of this.attributes) {
            attr.EnableInputs();
        }
    }

    private DisableForm(): void {
        for (let attr of this.attributes) {
            attr.DisableInputs();
        }
    }
}
