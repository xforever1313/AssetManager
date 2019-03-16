//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

abstract class BaseAttribute implements IAttribute {

    // ---------------- Events ----------------

    public OnDelete: (attr: IAttribute) => void;

    // ---------------- Fields ----------------

    private key: string;

    private isReadOnly: boolean;

    private readonly div: HTMLDivElement;
    private readonly errorMessage: HTMLDivElement;

    private readonly keyHtml: HTMLInputElement;
    private readonly keyDiv: HTMLDivElement;

    /**
     * The DIV that the child attribute will append
     * its childrent to.
     */
    private readonly parentDiv: HTMLDivElement;

    /**
     * DIV that has control over moving or deleting
     * attributes.
     */
    private readonly controlDiv: HTMLDivElement;

    // ---------------- Constructor ----------------

    constructor(name: string, readonly: boolean = false) {
        this.key = "";
        this.isReadOnly = readonly;

        this.div = <HTMLDivElement>(document.createElement("div"));
        if (readonly) {
            this.div.className = "panel panel-default";
        }
        else {
            this.div.className = "panel panel-info";
        }

        let panelHeadingDiv = <HTMLDivElement>(document.createElement("div"));
        panelHeadingDiv.className = "panel-heading";
        panelHeadingDiv.innerText = name;
        this.div.appendChild(panelHeadingDiv);

        this.parentDiv = <HTMLDivElement>(document.createElement("div"));
        this.parentDiv.className = "panel-body";
        this.div.appendChild(this.parentDiv);

        this.errorMessage = <HTMLDivElement>(document.createElement("div"));
        this.errorMessage.className = "alert alert-danger";

        {
            this.keyDiv = <HTMLDivElement>(document.createElement("div"));
            this.keyDiv.className = "form-group";
            this.parentDiv.appendChild(this.keyDiv);

            let keyLabel = <HTMLLabelElement>(document.createElement("label"));
            keyLabel.innerText = "Name:";
            this.keyDiv.appendChild(keyLabel);

            this.keyHtml = <HTMLInputElement>(document.createElement("input"));
            this.keyHtml.type = "text";
            this.keyHtml.className = "form-control";
            this.keyDiv.appendChild(this.keyHtml);

            this.keyHtml.oninput = (() => this.SetKey(this.keyHtml.value));
        }

        // Add control DIV
        if(readonly === false) {
            this.controlDiv = <HTMLDivElement>(document.createElement("div"));
            this.controlDiv.className = "form-group";

            let deleteButton: HTMLButtonElement = <HTMLButtonElement>(document.createElement("button"));
            deleteButton.className = "btn btn-danger";
            deleteButton.type = "button";
            deleteButton.innerText = "Delete";
            deleteButton.onclick = () => {
                if ((this.OnDelete !== undefined) && (this.OnDelete !== null)) {
                    this.OnDelete(this);
                }
            };
            this.controlDiv.appendChild(deleteButton);

            this.parentDiv.appendChild(this.controlDiv);
        }
    }

    // ---------------- Getters / Setters ----------------

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

    protected AppendChild(childNode: HTMLDivElement): void {
        this.parentDiv.insertBefore(childNode, this.controlDiv);
    }

    // ---------------- Functions ----------------

    public Validate(): boolean {
        var success: boolean = true;

        let keyGood = (Helpers.StringIsNullOrEmpty(this.GetKey()) === false);
        success = keyGood;

        let childProblems: Array<string> = this.ValidateChild();
        if ((childProblems !== null) && (childProblems.length > 0)) {
            success = false;
        }

        if (success === false) {
            this.div.className = "panel panel-danger";

            let errorList: HTMLUListElement = document.createElement("ul");
            if (keyGood === false) {
                if (childProblems === null) {
                    childProblems = new Array<string>();
                }
                childProblems.push("Attribute name can not be empty.");
            }

            for (let msg of childProblems) {
                let listElement: HTMLLIElement = document.createElement("li");
                listElement.innerText = msg;
                errorList.appendChild(listElement);
            }

            // Remove the old error message children.
            while (this.errorMessage.firstChild) {
                this.errorMessage.removeChild(this.errorMessage.firstChild);
            }

            // Replace.
            this.errorMessage.appendChild(errorList);
            this.parentDiv.insertBefore(this.errorMessage, this.keyDiv);
        }
        else {
            var remove: boolean = false;
            this.parentDiv.childNodes.forEach(
                c => {
                    if (c.isSameNode(this.errorMessage)) {
                        remove = true;
                    }
                }
            );

            if (remove) {
                this.parentDiv.removeChild(this.errorMessage);
            }
            if (this.isReadOnly) {
                this.div.className = "panel panel-default";
            }
            else {
                this.div.className = "panel panel-info";
            }
        }

        return success;
    }

    /**
     * The child does validation to ensure it is in an okay state.
     * If not, return an array that contains error messages.
     * If there is nothing wrong, return null.
     **/
    public abstract ValidateChild(): Array<string>;
}