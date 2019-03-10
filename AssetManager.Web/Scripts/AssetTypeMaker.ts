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

abstract class BaseAttribute implements IAttribute {

    // ---------------- Fields ----------------

    private key: string;

    private readonly div: HTMLDivElement;
    private readonly keyHtml: HTMLInputElement;

    private readonly parentDiv: HTMLDivElement;

    // ---------------- Constructor ----------------

    constructor() {
        this.key = "";

        this.div = <HTMLDivElement>(document.createElement("div"));
        this.div.className = "panel panel-info";

        let panelHeadingDiv = <HTMLDivElement>(document.createElement("div"));
        panelHeadingDiv.className = "panel-heading";
        panelHeadingDiv.innerText = "String Attribute";
        this.div.appendChild(panelHeadingDiv);

        this.parentDiv = <HTMLDivElement>(document.createElement("div"));
        this.parentDiv.className = "panel-body";
        this.div.appendChild(this.parentDiv);

        {
            let keyFormDiv = <HTMLDivElement>(document.createElement("div"));
            keyFormDiv.className = "form-group";
            this.parentDiv.appendChild(keyFormDiv);

            let keyLabel = <HTMLLabelElement>(document.createElement("label"));
            keyLabel.innerText = "Name:";
            keyFormDiv.appendChild(keyLabel);

            this.keyHtml = <HTMLInputElement>(document.createElement("input"));
            this.keyHtml.type = "text";
            this.keyHtml.className = "form-control";
            keyFormDiv.appendChild(this.keyHtml);
        }
    }

    // ---------------- Getter/s Setters ----------------

    public SetKey(newKey: string): void {

        if (Helpers.StringIsNullOrEmpty(newKey)) {
            newKey = "";
        }

        this.key = newKey;
    }

    public GetKey(): string {
        return this.key;
    }

    public GetHtmlDiv(): HTMLDivElement {
        return this.div;
    }

    protected GetParentDiv(): HTMLDivElement {
        return this.parentDiv;
    }

    public Validate(): boolean {
        var success: boolean = true;
        var errorString: string = "";

        if (Helpers.StringIsNullOrEmpty(this.GetKey())) {
            success = false;
            errorString += "Key can not be null or empty.";
        }

        return success;
    }

    public abstract ValidateChild(): boolean;
}

class StringAttribute extends BaseAttribute {

    // ---------------- Fields ----------------

    private value: string;

    private valueHtml: HTMLTextAreaElement;

    // ---------------- Constructor ----------------

    constructor() {
        super();
        this.value = "";

        let parentDiv = this.GetParentDiv();

        let valueFormDiv = <HTMLDivElement>(document.createElement("div"));
        valueFormDiv.className = "form-group";
        parentDiv.appendChild(valueFormDiv);

        let valueLabel = <HTMLLabelElement>(document.createElement("label"));
        valueLabel.innerText = "Value:";
        valueFormDiv.appendChild(valueLabel);

        this.valueHtml = <HTMLTextAreaElement>(document.createElement("textarea"));
        this.valueHtml.className = "form-control";
        valueFormDiv.appendChild(this.valueHtml);

        this.valueHtml.onkeypress = (() => this.SetValue(this.valueHtml.innerText));
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

    // ---------------- Functions ----------------
    
    public ValidateChild(): boolean {
        var success: boolean = true;
        var errorString: string = "";

        if (Helpers.StringIsNullOrEmpty(this.GetValue())) {
            success = false;
            errorString += "Value can not be null or empty.";
        }

        return success;
    }
}

class Helpers {
    public static StringIsNullOrEmpty(str: string): boolean {
        return (str === null) || (str === undefined) || (str === "");
    }
}