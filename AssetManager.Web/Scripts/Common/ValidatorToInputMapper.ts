//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class ValidatorToInputMapper<TValidator extends IValidator> {

    // ---------------- Fields ----------------

    private readonly validator: TValidator;

    private readonly inputs: Array<IInputWrapper>;

    private readonly errorDiv: HTMLDivElement;

    // ---------------- Constructor ----------------

    constructor(validator: TValidator, inputs: Array<IInputWrapper>, errorDiv: HTMLDivElement) {
        this.validator = validator;
        this.inputs = inputs;
        this.errorDiv = errorDiv;
    }

    // ---------------- Functions ----------------

    public GetValidator(): TValidator {
        return this.validator;
    }

    public Validate(): boolean {

        // First, sync all values:
        for (let input of this.inputs) {
            input.SyncValue();
        }

        // Next, display all errors.
        let errors: Array<string> = this.validator.Validate();
        if ((errors === null) || (errors.length === 0)) {
            this.RemoveOldErrors();
            return true;
        }

        this.DisplayErrors(errors);
        return false;
    }

    public EnableInputs(): void {
        for (let input of this.inputs) {
            input.Enable();
        }
    }

    public DisableInputs(): void {
        for (let input of this.inputs) {
            input.Disable();
        }
    }

    private RemoveOldErrors(): void {
        while (this.errorDiv.firstChild) {
            this.errorDiv.removeChild(this.errorDiv.firstChild);
        }
        this.errorDiv.className = "";
        this.errorDiv.style.margin = "";
    }

    private DisplayErrors(messages: Array<string>): void {
        // First, remove all old error message children
        this.RemoveOldErrors();

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