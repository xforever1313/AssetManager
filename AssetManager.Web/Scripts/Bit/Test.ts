//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class Test{

    // ---------------- Fields ----------------

    private readonly action: Function;

    // ---------------- Constructor ----------------

    constructor(name: string, action: Function) {
        this.Name = name;
        this.action = action;
    }

    // ---------------- Properties ---------------

    public Name: string;

    // ---------------- Functions ----------------

    DoTest(): void{
        this.action();
    }
}