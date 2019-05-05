//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class IntegerAttributeType extends BaseAttributeType {

    // ---------------- Fields ----------------

    private static idCount: number = 0;

    private minTextBox: HTMLInputElement;
    private maxTextBox: HTMLInputElement;
    private defaultTextBox: HTMLInputElement;
    private requiredBox: HTMLInputElement;
    private id: Number;

    private readonly info: IntegerAttributeTypeInfo;

    // ---------------- Constructor ----------------

    constructor() {
        super("Integer Attribute", AttributeType.IntegerAttribute);

        this.id = IntegerAttributeType.idCount++;
        this.info = new IntegerAttributeTypeInfo();

        let attr = this;

        this.minTextBox = <HTMLInputElement>(document.createElement("input"));
        this.CreateTextBox("Minimum Value", this.minTextBox, this.info.SetMin.bind(this.info));

        this.maxTextBox = <HTMLInputElement>(document.createElement("input"));
        this.CreateTextBox("Maximum Value", this.maxTextBox, this.info.SetMax.bind(this.info));

        this.defaultTextBox = <HTMLInputElement>(document.createElement("input"));
        this.CreateTextBox("Default Value", this.defaultTextBox, this.info.SetDefault.bind(this.info));

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
            this.requiredBox.checked = this.info.GetRequired();
            this.requiredBox.onclick = function () {
                attr.info.SetRequired( attr.requiredBox.checked );
            }.bind(this);

            requiredDiv.appendChild(this.requiredBox);
            requiredDiv.appendChild(requiredLabel);
            this.AppendChild(requiredDiv);
        }

        {
            let helpDiv = <HTMLDivElement>(document.createElement("div"));
            helpDiv.className = "form-group";
            this.AppendChild(helpDiv);

            helpDiv.innerText = "An integer attribute is a key-value pair, " +
                "where the key is the attribute name, and the value is an interger.  " +
                "The key is set here, but the value is set by the user.  An example of this could be " +
                "having the attribute name be 'Population', and the value would be '10000'.  " +
                "An integer can have a range, by filling in numbers in the Min and Max textboxes.  Leave these blank " +
                "to not specify a min and or max.  A default value can be specfied as well, leave this box blank " +
                " to not have one.";
        }
    }

    private CreateTextBox(context: string, bindedElement: HTMLInputElement, action: Function): void {
        let parentDiv = <HTMLDivElement>(document.createElement("div"));
        parentDiv.className = "form-group";

        let labelDiv = <HTMLLabelElement>(document.createElement("label"));
        labelDiv.innerText = context;
        parentDiv.appendChild(labelDiv);

        bindedElement.className = "form-control";
        bindedElement.type = "number";
        parentDiv.appendChild(bindedElement);

        bindedElement.onchange = () => {
            action(bindedElement.valueAsNumber);
        };

        this.AppendChild(parentDiv);
    }

    // ---------------- Properties ----------------

    // ---------------- Functions ----------------

    public ToJson(): object {
        let data = {
            "Key": this.GetKey(),
            "AttributeType": this.AttributeType,
            "Required": this.info.GetRequired(),
            "PossibleValues": {
                "Version": 1,
                "MinValue": this.info.GetMin(),
                "MaxValue": this.info.GetMax()
            },
            "DefaultValue": this.info.GetDefault()
        };

        return data;
    }

    public ValidateChild(): Array<string> {
        return this.info.Validate();
    }

    protected EnableFormInternal(): void {
        this.minTextBox.readOnly = false;
        this.maxTextBox.readOnly = false;
        this.defaultTextBox.readOnly = false;
        this.requiredBox.readOnly = false;
        this.requiredBox.disabled = false;
    }

    protected DisableFormInternal(): void {
        this.minTextBox.readOnly = true;
        this.maxTextBox.readOnly = true;
        this.defaultTextBox.readOnly = true;
        this.requiredBox.readOnly = true;
        this.requiredBox.disabled = true;
    }
}