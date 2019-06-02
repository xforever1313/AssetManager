//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

/// <reference path="../../Common/Enums.ts"/>
/// <reference path="../../Common/IntegerAttributeTypeInfo.ts"/>
/// <reference path="../../../../SethCS/TypescriptBit/Bit.ts"/>

class IntegerAttributeTypeInfoTests implements ITestFixture {

    // ---------------- Fields ----------------

    private readonly tests: Array<Test>;

    private uut: IntegerAttributeTypeInfo;

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
            new Test("Get/Set Max Test", this.SetGetMaxTest.bind(fixture))
        );

        this.tests.push(
            new Test("Get/Set Min Test", this.SetGetMinTest.bind(fixture))
        );

        this.tests.push(
            new Test("Validate All Null Test", this.ValidateTestAllNull.bind(fixture))
        );

        this.tests.push(
            new Test("Min Max Null, Default Set Test", this.ValidateMinMaxNullDefaultSet.bind(fixture))
        );

        this.tests.push(
            new Test("Validate Min Greater Than Max Test", this.ValidateMinGreaterThanMax.bind(fixture))
        );

        this.tests.push(
            new Test("Validate Default Less than Min Test", this.ValidateDefaultLessThanMin.bind(fixture))
        );

        this.tests.push(
            new Test("Validate Default Greater Than Max Test", this.ValidateDefaultGreaterThanMax.bind(fixture))
        );

        this.tests.push(
            new Test("Validate Default Equal to Min Test", this.ValidateDefaultEqualToMin.bind(fixture))
        );

        this.tests.push(
            new Test("Validate Default Equal to Max Test", this.ValidateDefaultEqualToMax.bind(fixture))
        );

        this.tests.push(
            new Test("Validate Default in Range Test", this.ValidateDefaultInRange.bind(fixture))
        );

        this.tests.push(
            new Test("Attribute Type Test", this.AttributeTypeTest.bind(fixture))
        );

        this.FixtureName = "IntegerAttributeTypeInfo Tests";
    }

    // ---------------- Properties ----------------

    public FixtureName: string;

    // ---------------- Setup / Teardown ----------------

    public DoFixtureSetup(): void {

    }

    public DoFixtureTeardown(): void {

    }

    public DoTestSetup(): void {
        this.uut = new IntegerAttributeTypeInfo();
    }

    public DoTestTeardown(): void {

    }

    public GetAllTests(): Array<Test> {
        return this.tests;
    }

    // ---------------- Tests ----------------

    public SetGetDefaultTest(): void {
        const value: number = 1;

        this.uut.SetDefault(value);
        Assert.AreEqual(value, this.uut.GetDefault());

        // Null should be null.
        this.uut.SetDefault(null);
        Assert.AreEqual(null, this.uut.GetDefault());

        // Undefined should become null.
        this.uut.SetDefault(undefined);
        Assert.AreEqual(null, this.uut.GetDefault());
    }

    public SetGetRequiredTest(): void {
        const value: boolean = this.uut.GetRequired() === false;

        this.uut.SetRequired(value);
        Assert.AreEqual(value, this.uut.GetRequired());
    }

    public SetGetMaxTest(): void {
        const value: number = 1;

        this.uut.SetMax(value);
        Assert.AreEqual(value, this.uut.GetMax());

        // Null should be null.
        this.uut.SetMax(null);
        Assert.AreEqual(null, this.uut.GetMax());

        // Undefined should become null.
        this.uut.SetMax(undefined);
        Assert.AreEqual(null, this.uut.GetMax());
    }

    public SetGetMinTest(): void {
        const value: number = -11;

        this.uut.SetMin(value);
        Assert.AreEqual(value, this.uut.GetMin());

        // Null should be null.
        this.uut.SetMin(null);
        Assert.AreEqual(null, this.uut.GetMin());

        // Undefined should become null.
        this.uut.SetMin(undefined);
        Assert.AreEqual(null, this.uut.GetMin());
    }

    public ValidateTestAllNull(): void {
        // Everything null should be okay.
        this.uut.SetMin(null);
        this.uut.SetMax(null);
        this.uut.SetDefault(null);
        Assert.AreEqual(0, this.uut.Validate().length);
    }

    public ValidateMinMaxNullDefaultSet(): void {
        // Default set, but min and max not should be okay.
        this.uut.SetMin(null);
        this.uut.SetMax(null);
        this.uut.SetDefault(1);
        Assert.AreEqual(0, this.uut.Validate().length);
    }

    public ValidateMinGreaterThanMax(): void {
        // Min being greater than max should not be okay,
        this.uut.SetMin(1);
        this.uut.SetMax(0);
        this.uut.SetDefault(null);
        Assert.AreEqual(1, this.uut.Validate().length);
    }

    public ValidateDefaultLessThanMin(): void {
        // Default being less than min should not be okay.
        this.uut.SetMin(0);
        this.uut.SetMax(1);
        this.uut.SetDefault(-1);
        Assert.AreEqual(1, this.uut.Validate().length);
    }

    public ValidateDefaultGreaterThanMax(): void {
        // Default being greater than max should not be okay.
        this.uut.SetMin(0);
        this.uut.SetMax(1);
        this.uut.SetDefault(2);
        Assert.AreEqual(1, this.uut.Validate().length);
    }

    public ValidateDefaultEqualToMin(): void {
        // Default being equal to min should be okay.
        this.uut.SetMin(0);
        this.uut.SetMax(1);
        this.uut.SetDefault(0);
        Assert.AreEqual(0, this.uut.Validate().length);
    }

    public ValidateDefaultEqualToMax(): void {
        // Default being equal to max should be okay.
        this.uut.SetMin(0);
        this.uut.SetMax(1);
        this.uut.SetDefault(1);
        Assert.AreEqual(0, this.uut.Validate().length);
    }

    public ValidateDefaultInRange(): void {
        // Default being between min and max should be okay.
        this.uut.SetMin(0);
        this.uut.SetMax(2);
        this.uut.SetDefault(1);
        Assert.AreEqual(0, this.uut.Validate().length);
    }

    public AttributeTypeTest(): void {
        Assert.AreEqual(AttributeType.IntegerAttribute, this.uut.AttributeType);
    }
}