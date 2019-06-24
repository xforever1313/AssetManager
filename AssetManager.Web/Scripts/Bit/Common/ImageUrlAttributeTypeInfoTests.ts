//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

/// <reference path="../../Common/Enums.ts"/>
/// <reference path="../../Common/ImageUrlAttributeTypeInfo.ts"/>
/// <reference path="../../../../SethCS/TypescriptBit/Bit.ts"/>

class ImageUrlAttributeTypeInfoTests implements ITestFixture {

    // ---------------- Fields ----------------

    private readonly tests: Array<Test>;

    private uut: ImageUrlAttributeTypeInfo;

    // ---------------- Constructor ----------------

    constructor() {
        let fixture = this;
        this.tests = new Array<Test>();

        this.tests.push(
            new Test("Get/Set Required Test", this.SetGetRequiredTest.bind(fixture))
        );

        this.tests.push(
            new Test("Validate Test", this.ValidateTest.bind(fixture))
        );

        this.tests.push(
            new Test("Attribute Type Test", this.AttributeTypeTest.bind(fixture))
        );

        this.FixtureName = "ImageUrlAttributeTypeInfo Tests";
    }

    // ---------------- Properties ----------------

    public FixtureName: string;

    // ---------------- Setup / Teardown ----------------

    public DoFixtureSetup(): void {

    }

    public DoFixtureTeardown(): void {

    }

    public DoTestSetup(): void {
        this.uut = new ImageUrlAttributeTypeInfo();
    }

    public DoTestTeardown(): void {

    }

    public GetAllTests(): Array<Test> {
        return this.tests;
    }

    // ---------------- Tests ----------------

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
        Assert.AreEqual(AttributeType.ImageUrl, this.uut.AttributeType);
    }
}