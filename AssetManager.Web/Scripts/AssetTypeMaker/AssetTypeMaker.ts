//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class AssetTypeMaker {

    // ---------------- Fields ----------------

    private readonly appDiv: HTMLDivElement;

    private readonly assetTypeNameEditor: AssetTypeNameEditor;

    private readonly attrList: Array<IAttributeType>;

    // ---------------- Constructor ----------------

    constructor() {
        this.appDiv = document.getElementById("app") as HTMLDivElement;
        this.assetTypeNameEditor = new AssetTypeNameEditor(
            document.getElementById("AssetTypeNameTextbox") as HTMLInputElement,
            document.getElementById("AssetTypeNameTextBoxError") as HTMLDivElement,
            document.getElementById("addAssetButton") as HTMLButtonElement,
            document.getElementById("addAttributeButton") as HTMLButtonElement
        );
        this.attrList = new Array<IAttributeType>();
    }

    // ---------------- Functions ----------------

    public AddAttribute(attrType: AttributeType): void {

        var attr: IAttributeType = undefined;

        switch (attrType) {
            case AttributeType.IntegerAttribute:
                attr = new IntegerAttributeType();
                break;
            case AttributeType.StringAttribute:
                attr = new StringAttributeType();
                break;
            case AttributeType.AssetNameAttribute:
                attr = new AssetNameAttributeType();
                break;
        }

        if (attr !== undefined) {
            this.attrList.push(attr);
            this.appDiv.appendChild(attr.GetHtmlDiv());

            if (attrType !== AttributeType.AssetNameAttribute) {
                let maker = this;
                attr.OnDelete = function (theAttr: IAttributeType) {
                    maker.appDiv.removeChild(theAttr.GetHtmlDiv());

                    // Holy crap, typescript doesn't have a REMOVE function for an array!?
                    const index = maker.attrList.indexOf(theAttr);
                    if (index > -1) {
                        maker.attrList.splice(index, 1);
                    }
                };
            }
        }
    }

    public Validate(): boolean {
        var success: boolean = true;
        for (let attr of this.attrList) {
            if (attr.Validate() === false) {
                success = false;
            }
        }

        if (this.assetTypeNameEditor.Validate() === false) {
            success = false;
        }

        return success;
    }

    public Submit(): void {
        if (this.Validate() === false) {
            // Handle Validation error.
        }
        else {
            this.DisableForm();
            const dataType = "application/json; charset=utf-8";
            let data = {
                AssetTypeName: this.assetTypeNameEditor.GetName(),
                AttributeList: new Array<object>()
            };

            for (var i = 0; i < this.attrList.length; ++i) {
                data.AttributeList.push(this.attrList[i].ToJson());
            }

            console.log(JSON.stringify(data));

            let maker = this;
            let xhr = new XMLHttpRequest();
            xhr.open("POST", "/AddAssetType/AddAssetType/");
            xhr.onreadystatechange = function () {
                if (xhr.readyState == XMLHttpRequest.DONE) {
                    // If we are successful, return to the home screen.
                    if (xhr.status === 200) {
                        window.location.href = "/";
                    }
                    else {
                        alert(xhr.responseText);
                        maker.EnableForm();
                    }
                }
            };

            xhr.setRequestHeader("Content-Type", dataType);
            xhr.send(JSON.stringify(data));
        }
    }

    private EnableForm(): void {
        for (let attr of this.attrList) {
            attr.EnableForm();
        }
        this.assetTypeNameEditor.EnableForm();
    }

    private DisableForm(): void {
        for (let attr of this.attrList) {
            attr.DisableForm();
        }
        this.assetTypeNameEditor.DisableForm();
    }
}
