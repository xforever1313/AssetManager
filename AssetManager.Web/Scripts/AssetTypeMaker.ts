//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class AssetTypeMaker {

    // ---------------- Fields ----------------

    private readonly appDiv: HTMLDivElement;

    private htmlString: string

    // ---------------- Constructor ----------------

    constructor() {
        this.appDiv = document.getElementById("app") as HTMLDivElement;
        this.htmlString = "";
    }

    // ---------------- Functions ----------------

    public AddAttribute(attr: string): void {
        this.htmlString += "<li>" + attr + "</li>";
        this.appDiv.innerHTML = "<ul>" + this.htmlString + "</ul>"
    }
}
