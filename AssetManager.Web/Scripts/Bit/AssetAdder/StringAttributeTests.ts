//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

/// <reference path="../../Common/Enums.ts"/>
/// <reference path="../../Common/StringAttributeTypeInfo.ts"/>
/// <reference path="../../AssetAdder/StringAttribute.ts"/>
/// <reference path="../../../../SethCS/TypescriptBit/Bit.ts"/>

class StringAttributeTests implements ITestFixture {

    // ---------------- Fields ----------------

    private readonly tests: Array<Test>;

    private info: StringAttributeTypeInfo;
    private uut: StringAttribute

    private readonly key: string = "MyKey";
    private readonly defaultValue: string = "Default";

    // ---------------- Constructor ----------------

    constructor() {
        let fixture = this;
        this.tests = new Array<Test>();

        this.tests.push(
            new Test("Default Value Test", this.DefaultValueTest.bind(fixture))
        );

        this.tests.push(
            new Test("Set/Get Value Test", this.GetSetValueTest.bind(fixture))
        );

        this.tests.push(
            new Test("Key Set Test", this.KeySetTest.bind(fixture))
        );

        this.tests.push(
            new Test("Attribute Type Test", this.AttributeTypeTest.bind(fixture))
        );

        this.tests.push(
            new Test("Validate required not set with no value", this.ValidateRequiredNotSetNoValue.bind(fixture))
        );

        this.tests.push(
            new Test("Validate required not set with value", this.ValidateRequiredNotSetWithValue.bind(fixture))
        );

        this.tests.push(
            new Test("Validate required set with no value", this.ValidateRequiredSetNoValue.bind(fixture))
        );

        this.tests.push(
            new Test("Validate required set with value", this.ValidateRequiredSetWithValue.bind(fixture))
        );

        this.FixtureName = "StringAttribute Tests";
    }

    // ---------------- Properties ----------------

    public FixtureName: string;

    // ---------------- Setup / Teardown ----------------

    public DoFixtureSetup(): void {

    }

    public DoFixtureTeardown(): void {

    }

    public DoTestSetup(): void {
        this.info = new StringAttributeTypeInfo();
        this.info.SetDefault(this.defaultValue);
        this.uut = new StringAttribute(this.info, this.key);
    }

    public DoTestTeardown(): void {

    }

    public GetAllTests(): Array<Test> {
        return this.tests;
    }

    // ---------------- Tests ----------------

    public DefaultValueTest(): void {
        Assert.AreEqual(this.defaultValue, this.uut.GetValue());
    }

    public GetSetValueTest(): void {
        const value: string = "New Value";

        this.uut.SetValue(value);
        Assert.AreEqual(value, this.uut.GetValue());

        // Empty string or null should result in null.
        this.uut.SetValue("");
        Assert.AreEqual(null, this.uut.GetValue());

        this.uut.SetValue(null);
        Assert.AreEqual(null, this.uut.GetValue());

        this.uut.SetValue(undefined);
        Assert.AreEqual(null, this.uut.GetValue());
    }

    public KeySetTest(): void {
        // Ensure our key was set correctly in the constructor.
        Assert.AreEqual(this.key, this.uut.Key);
    }

    public AttributeTypeTest(): void {
        Assert.AreEqual(AttributeType.StringAttribute, this.uut.AttributeType);
    }

    public ValidateRequiredNotSetNoValue(): void {
        this.info.SetRequired(false);

        this.uut.SetValue(null);
        Assert.AreEqual(0, this.uut.Validate().length);
    }

    public ValidateRequiredNotSetWithValue(): void {
        this.info.SetRequired(false);

        this.uut.SetValue("Should be okay");
        Assert.AreEqual(0, this.uut.Validate().length);
    }

    public ValidateRequiredSetNoValue(): void {
        this.info.SetRequired(true);

        this.uut.SetValue(null);
        Assert.AreEqual(1, this.uut.Validate().length);
    }

    public ValidateRequiredSetWithValue(): void {
        this.info.SetRequired(true);

        this.uut.SetValue("Should be okay");
        Assert.AreEqual(0, this.uut.Validate().length);
    }
}