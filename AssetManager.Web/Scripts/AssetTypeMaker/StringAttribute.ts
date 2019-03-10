//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class StringAttribute extends BaseAttribute {

    // ---------------- Fields ----------------

    private value: string;

    private valueHtml: HTMLTextAreaElement;

    // ---------------- Constructor ----------------

    constructor() {
        super();
        this.value = "";

        let valueFormDiv = <HTMLDivElement>(document.createElement("div"));
        valueFormDiv.className = "form-group";
        this.AppendChild(valueFormDiv);

        let valueLabel = <HTMLLabelElement>(document.createElement("label"));
        valueLabel.innerText = "Value:";
        valueFormDiv.appendChild(valueLabel);

        this.valueHtml = <HTMLTextAreaElement>(document.createElement("textarea"));
        this.valueHtml.className = "form-control";
        valueFormDiv.appendChild(this.valueHtml);

        this.valueHtml.oninput = (() => this.SetValue(this.valueHtml.value));
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

    public ValidateChild(): Array<string> {
        // Nothing to validate, string value can be anything.
        return null;
    }
}