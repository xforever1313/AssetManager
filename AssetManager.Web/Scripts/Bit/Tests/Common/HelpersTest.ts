//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

/// <reference path="../../../Common/Helpers.ts"/>
/// <reference path="../../ITestFixture.ts"/>
/// <reference path="../../Test.ts"/>
/// <reference path="../../TestRunner.ts"/>
/// <reference path="../../Assert.ts"/>

class HelpersTest implements ITestFixture {

    // ---------------- Fields ----------------

    private readonly tests: Array<Test>;

    // ---------------- Constructor ----------------

    constructor() {
        let fixture = this;
        this.tests = new Array<Test>();

        this.tests.push(
            new Test("DoStringIsNullOrEmptyTest", this.DoStringIsNullOrEmptyTest.bind(fixture))
        );

        TestRunner.Instance().AddTestFixture(this);
    }

    // ---------------- Properties ----------------

    public FixtureName: string;

    // ---------------- Setup / Teardown ----------------

    public DoFixtureSetup(): void {

    }

    public DoFixtureTeardown(): void {

    }

    public DoTestSetup(): void {

    }

    public DoTestTeardown(): void {

    }

    public GetAllTests(): Array<Test> {
        return this.tests;
    }

    // ---------------- Tests ----------------

    public DoStringIsNullOrEmptyTest(): void {
        Assert.IsTrue(Helpers.StringIsNullOrEmpty(null));
        Assert.IsTrue(Helpers.StringIsNullOrEmpty(undefined));
        Assert.IsTrue(Helpers.StringIsNullOrEmpty(""));
        Assert.IsFalse(Helpers.StringIsNullOrEmpty("Hello"));
    }
}