//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

/// <reference path="../../Common/Enums.ts"/>
/// <reference path="../../Common/StringAttributeTypeInfo.ts"/>
/// <reference path="../../../../SethCS/TypescriptBit/Bit.ts"/>

class StringAttributeTypeInfoTests implements ITestFixture {

    // ---------------- Fields ----------------

    private readonly tests: Array<Test>;

    private uut: StringAttributeTypeInfo;

    // ---------------- Constructor ----------------

    constructor() {
        let fixture = this;
        this.tests = new Array<Test>();

        this.tests.push(
            new Test("Set/Get Default Test", this.SetGetDefaultTest.bind(fixture))
        );

        this.tests.push(
            new Test("Get/Set Required Test", this.SetGetRequiredTest.bind(fixture))
        );

        this.tests.push(
            new Test("Validate Test", this.ValidateTest.bind(fixture))
        );

        this.tests.push(
            new Test("Attribute Type Test", this.AttributeTypeTest.bind(fixture))
        );

        this.FixtureName = "StringAttributeTypeInfo Tests";
    }

    // ---------------- Properties ----------------

    public FixtureName: string;

    // ---------------- Setup / Teardown ----------------

    public DoFixtureSetup(): void {

    }

    public DoFixtureTeardown(): void {

    }

    public DoTestSetup(): void {
        this.uut = new StringAttributeTypeInfo();
    }

    public DoTestTeardown(): void {

    }

    public GetAllTests(): Array<Test> {
        return this.tests;
    }

    // ---------------- Tests ----------------

    public SetGetDefaultTest(): void {
        const value: string = "Hello World";

        this.uut.SetDefault(value);
        Assert.AreEqual(value, this.uut.GetDefaultValue());

        // Empty string should be null
        this.uut.SetDefault("");
        Assert.AreEqual(null, this.uut.GetDefaultValue());

        // Null should be null.
        this.uut.SetDefault(null);
        Assert.AreEqual(null, this.uut.GetDefaultValue());

        // Undefined should become null.
        this.uut.SetDefault(undefined);
        Assert.AreEqual(null, this.uut.GetDefaultValue());
    }

    public SetGetRequiredTest(): void {
        const value: boolean = this.uut.GetRequired() === false;

        this.uut.SetRequired(value);
        Assert.AreEqual(value, this.uut.GetRequired());
    }

    public ValidateTest(): void {
        const errors: Array<string> = this.uut.Validate();
        Assert.AreEqual(null, errors);
    }

    public AttributeTypeTest(): void {
        Assert.AreEqual(AttributeType.StringAttribute, this.uut.AttributeType);
    }
}