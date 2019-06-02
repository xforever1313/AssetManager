//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

/// <reference path="../../Common/Enums.ts"/>
/// <reference path="../../Common/IntegerAttributeTypeInfo.ts"/>
/// <reference path="../../AssetAdder/IntegerAttribute.ts"/>
/// <reference path="../../../../SethCS/TypescriptBit/Bit.ts"/>

class IntegerAttributeTests implements ITestFixture {

    // ---------------- Fields ----------------

    private readonly tests: Array<Test>;

    private info: IntegerAttributeTypeInfo;
    private uut: IntegerAttribute

    private readonly key: string = "MyKey";
    private readonly defaultValue: number = 100;

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
            new Test("Validate with info having everything null, no value set", this.ValidateInfoEverythingNullWithNoValue.bind(fixture))
        );

        this.tests.push(
            new Test("Validate with info having everything null, value set", this.ValidateInfoEverythingNullWithValue.bind(fixture))
        );

        this.tests.push(
            new Test("Validate with info having min set, value in range", this.ValidateInfoMinSetWithGoodValue.bind(fixture))
        );

        this.tests.push(
            new Test("Validate with info having min set, value out-of-range", this.ValidateInfoMinSetWithBadValue.bind(fixture))
        );

        this.tests.push(
            new Test("Validate with info having max set, value in range", this.ValidateInfoMaxSetWithGoodValue.bind(fixture))
        );

        this.tests.push(
            new Test("Validate with info having max set, value out-of-range", this.ValidateInfoMaxSetWithBadValue.bind(fixture))
        );

        this.tests.push(
            new Test("Validate with info having min/max set, value in range", this.ValidateInfoMinMaxSetValueInRange.bind(fixture))
        );

        this.tests.push(
            new Test("Validate with info having required set, value not set", this.ValidateInfoRequiredSetValueNot.bind(fixture))
        );

        this.tests.push(
            new Test("Validate with info having required set, value is set", this.ValidateInfoRequiredSetValueIs.bind(fixture))
        );

        this.tests.push(
            new Test("Attribute Type Test", this.AttributeTypeTest.bind(fixture))
        );

        this.FixtureName = "IntegerAttribute Tests";
    }

    // ---------------- Properties ----------------

    public FixtureName: string;

    // ---------------- Setup / Teardown ----------------

    public DoFixtureSetup(): void {

    }

    public DoFixtureTeardown(): void {

    }

    public DoTestSetup(): void {
        this.info = new IntegerAttributeTypeInfo();
        this.info.SetDefault(this.defaultValue);
        this.uut = new IntegerAttribute(this.info, this.key);
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
        const value: number = 10;
        
        this.uut.SetValue(value);
        Assert.AreEqual(value, this.uut.GetValue());

        // Not a number should result in a null
        this.uut.SetValue(null);
        Assert.AreEqual(null, this.uut.GetValue());

        this.uut.SetValue(undefined);
        Assert.AreEqual(null, this.uut.GetValue());
    }

    public KeySetTest(): void {
        // Ensure our key was set correctly in the constructor.
        Assert.AreEqual(this.key, this.uut.Key);
    }

    public ValidateInfoEverythingNullWithNoValue(): void {
        this.info.SetMax(null);
        this.info.SetMin(null);
        this.info.SetRequired(false);

        this.uut.SetValue(null);
        Assert.AreEqual(0, this.uut.Validate().length);
    }

    public ValidateInfoEverythingNullWithValue(): void {
        this.info.SetMax(null);
        this.info.SetMin(null);
        this.info.SetRequired(false);

        this.uut.SetValue(10);
        Assert.AreEqual(0, this.uut.Validate().length);
    }

    public ValidateInfoMinSetWithGoodValue(): void {
        this.info.SetMax(null);
        this.info.SetMin(0);
        this.info.SetRequired(false);

        this.uut.SetValue(1);
        Assert.AreEqual(0, this.uut.Validate().length);

        this.uut.SetValue(0);
        Assert.AreEqual(0, this.uut.Validate().length);
    }

    public ValidateInfoMinSetWithBadValue(): void {
        this.info.SetMax(null);
        this.info.SetMin(0);
        this.info.SetRequired(false);

        this.uut.SetValue(-1);
        Assert.AreEqual(1, this.uut.Validate().length);
    }

    public ValidateInfoMaxSetWithGoodValue(): void {
        this.info.SetMax(1);
        this.info.SetMin(null);
        this.info.SetRequired(false);

        this.uut.SetValue(1);
        Assert.AreEqual(0, this.uut.Validate().length);

        this.uut.SetValue(0);
        Assert.AreEqual(0, this.uut.Validate().length);
    }

    public ValidateInfoMaxSetWithBadValue(): void {
        this.info.SetMax(1);
        this.info.SetMin(null);
        this.info.SetRequired(false);

        this.uut.SetValue(2);
        Assert.AreEqual(1, this.uut.Validate().length);
    }

    public ValidateInfoMinMaxSetValueInRange(): void {
        this.info.SetMax(1);
        this.info.SetMin(-1);
        this.info.SetRequired(false);

        this.uut.SetValue(-1);
        Assert.AreEqual(0, this.uut.Validate().length);

        this.uut.SetValue(0);
        Assert.AreEqual(0, this.uut.Validate().length);

        this.uut.SetValue(1);
        Assert.AreEqual(0, this.uut.Validate().length);
    }

    public ValidateInfoRequiredSetValueNot(): void {
        this.info.SetMax(null);
        this.info.SetMin(null);
        this.info.SetRequired(true);

        this.uut.SetValue(null);
        Assert.AreEqual(1, this.uut.Validate().length);
    }

    public ValidateInfoRequiredSetValueIs(): void {
        this.info.SetMax(null);
        this.info.SetMin(null);
        this.info.SetRequired(true);

        this.uut.SetValue(10);
        Assert.AreEqual(0, this.uut.Validate().length);
    }

    public AttributeTypeTest(): void {
        Assert.AreEqual(AttributeType.IntegerAttribute, this.uut.AttributeType);
    }
}