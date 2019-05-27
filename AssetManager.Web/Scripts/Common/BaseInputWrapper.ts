//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

abstract class BaseInputWrapper implements IInputWrapper {

    // ---------------- Fields ----------------

    protected readonly input: HTMLInputElement;

    private errorDiv: HTMLDivElement;

    // ---------------- Constructor ----------------

    constructor(input: HTMLInputElement, errorDiv: HTMLDivElement) {
        this.input = input;
        this.errorDiv = errorDiv;
    }

    // ---------------- Functions ----------------

    public Enable(): void {
        this.input.readOnly = false;
    }

    public Disable(): void {
        this.input.readOnly = true;
    }

    public abstract SyncValue(): void;

    public DisplayErrors(messages: Array<string>): void {

        // First, remove all old error message children
        while (this.errorDiv.firstChild) {
            this.errorDiv.removeChild(this.errorDiv.firstChild);
        }

        if (Helpers.IsNullOrUndefined(messages) || (messages.length === 0)) {
            this.errorDiv.className = "";
            this.errorDiv.style.margin = "";
        }
        else {
            this.errorDiv.className = "alert alert-danger";
            this.errorDiv.style.margin = "1em";

            let errorList: HTMLUListElement = document.createElement("ul");
            for (let msg of messages) {
                let listElement: HTMLLIElement = document.createElement("li");
                listElement.innerText = msg;
                errorList.appendChild(listElement);
            }

            this.errorDiv.appendChild(errorList);
        }
    }
}