//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class StringAttributeType extends BaseAttributeType {

    // ---------------- Fields ----------------

    private static idCount: number = 0;

    private defaultTextBox: HTMLInputElement;
    private requiredBox: HTMLInputElement;
    private id: Number;

    // ---------------- Constructor ----------------

    constructor() {
        super("String Attribute", AttributeType.StringAttribute);

        this.id = StringAttributeType.idCount++;
        this.DefaultValue = null;
        this.Required = false;

        let attr = this;

        this.defaultTextBox = <HTMLInputElement>(document.createElement("input"));
        this.CreateTextBox("Default Value", this.defaultTextBox, this.SetDefault.bind(this));

        let helpDiv = <HTMLDivElement>(document.createElement("div"));
        helpDiv.className = "form-group";
        this.AppendChild(helpDiv);

        {
            let requiredDiv = <HTMLDivElement>(document.createElement("div"));
            requiredDiv.className = "form-group";

            let requiredLabel = <HTMLLabelElement>(document.createElement("label"));
            requiredLabel.innerText = "Required";
            requiredLabel.htmlFor = "integerRequired" + this.id;
            requiredLabel.style.marginLeft = "0.5em";

            this.requiredBox = <HTMLInputElement>(document.createElement("input"));
            this.requiredBox.type = "checkbox";
            this.requiredBox.id = requiredLabel.htmlFor;
            this.requiredBox.checked = this.Required;
            this.requiredBox.onclick = function () {
                attr.Required = attr.requiredBox.checked;
            }.bind(this);

            requiredDiv.appendChild(this.requiredBox);
            requiredDiv.appendChild(requiredLabel);
            this.AppendChild(requiredDiv);
        }

        helpDiv.innerText = "A string attribute is a key-value pair, " +
            "where the key is the attribute name, and the value is a string of " +
            "any size.  The key is set here, but the value is set by the user.  An example of this could be " +
            "having the attribute name be 'Location', and the value would be 'New York'.  " +
            "A default value can be specified, leave this box blank " +
            " to not have one.";
    }

    private CreateTextBox(context: string, bindedElement: HTMLInputElement, action: Function): void {
        let parentDiv = <HTMLDivElement>(document.createElement("div"));
        parentDiv.className = "form-group";

        let labelDiv = <HTMLLabelElement>(document.createElement("label"));
        labelDiv.innerText = context;
        parentDiv.appendChild(labelDiv);

        bindedElement.className = "form-control";
        bindedElement.type = "text";
        parentDiv.appendChild(bindedElement);

        bindedElement.onchange = () => {
            action(bindedElement.value);
        };

        this.AppendChild(parentDiv);
    }

    // ---------------- Properties ----------------

    public DefaultValue: string;

    public Required: boolean;

    // ---------------- Functions ----------------

    public ToJson(): object {
        let possibleValues: object = null;

        let data = {
            "Key": this.GetKey(),
            "AttributeType": this.AttributeType,
            "Required": this.Required,
            "PossibleValues": possibleValues,
            "DefaultValue": this.DefaultValue
        };

        return data;
    }

    public ValidateChild(): Array<string> {
        // Nothing to validate.
        return null;
    }

    protected EnableFormInternal(): void {
        this.defaultTextBox.disabled = false;
        this.defaultTextBox.readOnly = false;
        this.requiredBox.disabled = false;
        this.requiredBox.readOnly = false;
    }

    protected DisableFormInternal(): void {
        this.defaultTextBox.disabled = true;
        this.defaultTextBox.readOnly = true;
        this.requiredBox.disabled = true;
        this.requiredBox.readOnly = true;
    }

    private SetDefault(def: string): void {
        // Empty string will be null.
        if (def === "") {
            this.DefaultValue = null;
        }
        else {
            this.DefaultValue = def;
        }
    }
}