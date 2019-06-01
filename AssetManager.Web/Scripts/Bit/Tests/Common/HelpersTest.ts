//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

/// <reference path="../../../Common/Helpers.ts"/>
/// <reference path="../../ITestFixture.ts"/>
/// <reference path="../../Test.ts"/>
/// <reference path="../../Assert.ts"/>

class HelpersTest implements ITestFixture {

    // ---------------- Fields ----------------

    private readonly tests: Array<Test>;

    // ---------------- Constructor ----------------

    constructor() {
        let fixture = this;
        this.tests = new Array<Test>();

        this.tests.push(
            new Test("StringIsNullOrEmpty Test", this.DoStringIsNullOrEmptyTest.bind(fixture))
        );

        this.tests.push(
            new Test("IsNullOrUndefined Test", this.DoIsNullOrUndefinedTest.bind(fixture))
        );

        this.tests.push(
            new Test("IsNotNullOrUndefined Test", this.DoIsNotNullOrUndefinedTest.bind(fixture))
        );
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

    public DoIsNullOrUndefinedTest(): void {
        let str = "";
        Assert.IsTrue(Helpers.IsNullOrUndefined(null));
        Assert.IsTrue(Helpers.IsNullOrUndefined(undefined));
        Assert.IsFalse(Helpers.IsNullOrUndefined(str));
    }

    public DoIsNotNullOrUndefinedTest(): void {
        let str = "";
        Assert.IsFalse(Helpers.IsNotNullOrUndefined(null));
        Assert.IsFalse(Helpers.IsNotNullOrUndefined(undefined));
        Assert.IsTrue(Helpers.IsNotNullOrUndefined(str));
    }
}