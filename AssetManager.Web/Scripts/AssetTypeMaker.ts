//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

// ---------------- Main Class ----------------

enum AttributeType {
    StringAttribute,
    IntegerAttribute
}

class AssetTypeMaker {

    // ---------------- Fields ----------------

    private readonly appDiv: HTMLDivElement;

    private readonly attrList: Array<IAttribute>;

    // ---------------- Constructor ----------------

    constructor() {
        this.appDiv = document.getElementById("app") as HTMLDivElement;
        this.attrList = new Array<IAttribute>();
    }

    // ---------------- Functions ----------------

    public AddAttribute(attrType: AttributeType): void {

        var attr: IAttribute = undefined;

        switch (attrType) {
            case AttributeType.IntegerAttribute:
                break;
            case AttributeType.StringAttribute:
                attr = new StringAttribute();
                break;
        }

        if (attr !== undefined) {
            this.attrList.push(attr);
            this.appDiv.appendChild(attr.GetHtmlDiv());
        }
    }

    public Validate(): boolean {
        var success: boolean = true;
        for (let attr of this.attrList) {
            if (attr.Validate() === false) {
                success = false;
            }
        }

        return success;
    }
}

// ---------------- Attributes ----------------

interface IAttribute {

    /**
     * Ensures the attribute is in a valid state.
     **/
    Validate(): boolean;

    GetHtmlDiv(): HTMLDivElement;
}

class StringAttribute implements IAttribute {

    // ---------------- Fields ----------------

    private key: string;

    private value: string;

    private div: HTMLDivElement;
    private keyHtml: HTMLInputElement;
    private valueHtml: HTMLTextAreaElement;

    // ---------------- Constructor ----------------

    constructor() {
        this.key = "Hello";
        this.value = "World";

        this.div = <HTMLDivElement>(document.createElement("div"));
        this.div.className = "panel panel-info";

        let panelHeadingDiv = <HTMLDivElement>(document.createElement("div"));
        panelHeadingDiv.className = "panel-heading";
        panelHeadingDiv.innerText = "String Attribute";
        this.div.appendChild(panelHeadingDiv);

        let panelBodyDiv = <HTMLDivElement>(document.createElement("div"));
        panelBodyDiv.className = "panel-body";
        this.div.appendChild(panelBodyDiv);

        {
            let keyFormDiv = <HTMLDivElement>(document.createElement("div"));
            keyFormDiv.className = "form-group";
            panelBodyDiv.appendChild(keyFormDiv);

            let keyLabel = <HTMLLabelElement>(document.createElement("label"));
            keyLabel.innerText = "Name:";
            keyFormDiv.appendChild(keyLabel);

            this.keyHtml = <HTMLInputElement>(document.createElement("input"));
            this.keyHtml.type = "text";
            this.keyHtml.className = "form-control";
            keyFormDiv.appendChild(this.keyHtml);
        }

        {
            let valueFormDiv = <HTMLDivElement>(document.createElement("div"));
            valueFormDiv.className = "form-group";
            panelBodyDiv.appendChild(valueFormDiv);

            let valueLabel = <HTMLLabelElement>(document.createElement("label"));
            valueLabel.innerText = "Value:";
            valueFormDiv.appendChild(valueLabel);

            this.valueHtml = <HTMLTextAreaElement>(document.createElement("textarea"));
            this.valueHtml.className = "form-control";
            valueFormDiv.appendChild(this.valueHtml);
        }

        this.keyHtml.onkeypress = (() => this.SetKey(this.keyHtml.innerText));
        this.valueHtml.onkeypress = (() => this.SetValue(this.valueHtml.innerText));
    }

    // ---------------- Getter/s Setters ----------------

    public SetKey(newKey: string): void{

        if (Helpers.StringIsNullOrEmpty(newKey)) {
            newKey = "";
        }

        this.key = newKey;
    }

    public GetKey(): string {
        return this.key;
    }

    public SetValue(newValue: string): void {

        if (Helpers.StringIsNullOrEmpty(newValue)) {
            newValue = "";
        }

        this.value = newValue;
    }

    public GetValue(): string {
        return this.value;
    }

    public GetHtmlDiv(): HTMLDivElement {
        return this.div;
    }

    // ---------------- Functions ----------------
    
    public Validate(): boolean {
        var success: boolean = true;
        var errorString: string = "";

        if (Helpers.StringIsNullOrEmpty(this.GetKey())) {
            success = false;
            errorString += "Key can not be null or empty.";
        }

        if (Helpers.StringIsNullOrEmpty(this.GetKey())) {
            success = false;
            errorString += "Key can not be null or empty.";
        }

        return success;
    }
}

class Helpers {
    public static StringIsNullOrEmpty(str: string): boolean {
        return (str === null) || (str === undefined) || (str === "");
    }
}