//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class TestResultDisplayer {

    // ---------------- Fields ----------------

    private readonly messageDiv: HTMLDivElement;
    private readonly overallDiv: HTMLDivElement;

    // ---------------- Constructor ----------------

    constructor(messageDiv: HTMLDivElement, overallDiv: HTMLDivElement) {
        this.messageDiv = messageDiv;
        this.overallDiv = overallDiv;
    }

    // ---------------- Functions ----------------

    DisplayResults(results: TestResults) {
        let list: HTMLUListElement = document.createElement("ul");
        for (let pass of results.Passes) {
            let listElement: HTMLLIElement = document.createElement("li");
            listElement.innerText = "Pass: " + pass;
            list.appendChild(listElement);
        }

        for (let fail of results.Fails) {
            let listElement: HTMLLIElement = document.createElement("li");
            listElement.innerText = "Fail: " + fail;
            list.appendChild(listElement);
        }

        this.messageDiv.appendChild(list);
        this.overallDiv.innerText = "Passes: " + results.Passes.length + ", Failures: " + results.Fails.length;
    }
}