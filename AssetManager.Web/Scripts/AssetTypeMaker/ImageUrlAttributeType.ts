//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

/// <reference path="./BaseAttributeType.ts"/>

class ImageUrlAttributeType extends BaseAttributeType {

    // ---------------- Fields ----------------

    private static idCount: number = 0;

    private requiredBox: HTMLInputElement;
    private id: Number;

    private readonly info: ImageUrlAttributeTypeInfo;

    // ---------------- Constructor ----------------

    constructor() {
        super("Image URL Attribute", AttributeType.ImageUrl);

        this.id = ImageUrlAttributeType.idCount++;
        this.info = new ImageUrlAttributeTypeInfo();

        let attr = this;

        let helpDiv = <HTMLDivElement>(document.createElement("div"));
        helpDiv.className = "form-group";
        this.AppendChild(helpDiv);

        {
            let requiredDiv = <HTMLDivElement>(document.createElement("div"));
            requiredDiv.className = "form-group";

            let requiredLabel = <HTMLLabelElement>(document.createElement("label"));
            requiredLabel.innerText = "Required";
            requiredLabel.htmlFor = "imagueUrlRequired" + this.id;
            requiredLabel.style.marginLeft = "0.5em";

            this.requiredBox = <HTMLInputElement>(document.createElement("input"));
            this.requiredBox.type = "checkbox";
            this.requiredBox.id = requiredLabel.htmlFor;
            this.requiredBox.checked = this.info.GetRequired();
            this.requiredBox.onclick = function () {
                attr.info.SetRequired(attr.requiredBox.checked);
            }.bind(this);

            requiredDiv.appendChild(this.requiredBox);
            requiredDiv.appendChild(requiredLabel);
            this.AppendChild(requiredDiv);
        }

        helpDiv.innerText = "A Image URL attribute is a key-value pair, " +
            "where the key is the attribute name, and the value is the URL to an image to display. " +
            "Upon creation, the user is able to optionally specify the width/height of the image. " +
            "URL must start with 'http:', 'https:', or '/'."
    }

    // ---------------- Functions ----------------

    public ToJson(): object {
        let possibleValues: object = null;
        let defaultValues: object = null;

        let data = {
            "Key": this.GetKey(),
            "AttributeType": this.AttributeType,
            "Required": this.info.GetRequired(),
            "PossibleValues": possibleValues,
            "DefaultValue": defaultValues
        };

        return data;
    }

    public ValidateChild(): Array<string> {
        return this.info.Validate();
    }

    protected EnableFormInternal(): void {
        this.requiredBox.disabled = false;
        this.requiredBox.readOnly = false;
    }

    protected DisableFormInternal(): void {
        this.requiredBox.disabled = true;
        this.requiredBox.readOnly = true;
    }
}