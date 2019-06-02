//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

/// <reference path="../../Common/Enums.ts"/>
/// <reference path="../../Common/AssetNameAttributeTypeInfo.ts"/>
/// <reference path="../../AssetAdder/AssetNameAttribute.ts"/>
/// <reference path="../../../../SethCS/TypescriptBit/Bit.ts"/>

class AssetNameAttributeTests implements ITestFixture {

    // ---------------- Fields ----------------

    private readonly tests: Array<Test>;

    private info: AssetNameAttributeTypeInfo;
    private uut: AssetNameAttribute

    private readonly key: string = "MyKey";

    // ---------------- Constructor ----------------

    constructor() {
        let fixture = this;
        this.tests = new Array<Test>();

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
            new Test("Validate with value null", this.ValidateValueNullTest.bind(fixture))
        );

        this.tests.push(
            new Test("Validate with value not null", this.ValidateValueNotNullTest.bind(fixture))
        );

        this.FixtureName = "AssetNameAttribute Tests";
    }

    // ---------------- Properties ----------------

    public FixtureName: string;

    // ---------------- Setup / Teardown ----------------

    public DoFixtureSetup(): void {

    }

    public DoFixtureTeardown(): void {

    }

    public DoTestSetup(): void {
        this.info = new AssetNameAttributeTypeInfo();
        this.uut = new AssetNameAttribute(this.info, this.key);
    }

    public DoTestTeardown(): void {

    }

    public GetAllTests(): Array<Test> {
        return this.tests;
    }

    // ---------------- Tests ----------------

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
        Assert.AreEqual(AttributeType.AssetNameAttribute, this.uut.AttributeType);
    }

    public ValidateValueNullTest(): void {
        // There must be a value.
        this.uut.SetValue(null);
        Assert.AreEqual(1, this.uut.Validate().length);
    }

    public ValidateValueNotNullTest(): void {
        // There must be a value.
        this.uut.SetValue("Not Null!");
        Assert.AreEqual(0, this.uut.Validate().length);
    }
}