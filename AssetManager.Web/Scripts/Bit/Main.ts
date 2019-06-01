﻿//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

/// <reference path="./Tests/Common/HelpersTest.ts"/>

class Main {
    constructor() {

    }

    Run(messageDiv: HTMLDivElement, overallDiv: HTMLDivElement): void {
        let instance = TestRunner.Instance();
        new HelpersTest();

        let results: TestResults = instance.Execute();

        let displayer = new TestResultDisplayer(messageDiv, overallDiv);
        displayer.DisplayResults(results);
    }
}