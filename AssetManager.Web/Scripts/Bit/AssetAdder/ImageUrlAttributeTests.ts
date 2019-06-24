//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

/// <reference path="../../Common/Enums.ts"/>
/// <reference path="../../Common/ImageUrlAttributeTypeInfo.ts"/>
/// <reference path="../../AssetAdder/ImageUrlAttribute.ts"/>
/// <reference path="../../../../SethCS/TypescriptBit/Bit.ts"/>

class ImageUrlAttributeTests implements ITestFixture {

    // ---------------- Fields ----------------

    private readonly tests: Array<Test>;

    private info: ImageUrlAttributeTypeInfo;
    private uut: ImageUrlAttribute

    private readonly key: string = "MyKey";

    // ---------------- Constructor ----------------

    constructor() {
        let fixture = this;
        this.tests = new Array<Test>();

        this.tests.push(
            new Test("Set/Get Value Test", this.GetSetValueTest.bind(fixture))
        );

        this.tests.push(
            new Test("Set/Get Height Test", this.GetSetHeightTest.bind(fixture))
        );

        this.tests.push(
            new Test("Set/Get Width Test", this.GetSetWidthTest.bind(fixture))
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

        this.tests.push(
            new Test("Validate negative height", this.ValidateNegativeHeight.bind(fixture))
        );

        this.tests.push(
            new Test("Validate okay height", this.ValidateOkayHeight.bind(fixture))
        );

        this.tests.push(
            new Test("Validate negative width", this.ValidateNegativeWidth.bind(fixture))
        );

        this.tests.push(
            new Test("Validate okay width", this.ValidateOkayWidth.bind(fixture))
        );

        this.FixtureName = "ImageUrlAttribute Tests";
    }

    // ---------------- Properties ----------------

    public FixtureName: string;

    // ---------------- Setup / Teardown ----------------

    public DoFixtureSetup(): void {

    }

    public DoFixtureTeardown(): void {

    }

    public DoTestSetup(): void {
        this.info = new ImageUrlAttributeTypeInfo();
        this.uut = new ImageUrlAttribute(this.info, this.key);
    }

    public DoTestTeardown(): void {

    }

    public GetAllTests(): Array<Test> {
        return this.tests;
    }

    // ---------------- Tests ----------------

    public GetSetValueTest(): void {
        const value: string = "https://shendrick.net/myimage.png";

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

    public GetSetHeightTest(): void {
        const value: number = 100;

        this.uut.SetHeight(value);
        Assert.AreEqual(value, this.uut.GetHeight());

        // null/undefined should result in null.
        this.uut.SetHeight(null);
        Assert.AreEqual(null, this.uut.GetHeight());

        this.uut.SetHeight(undefined);
        Assert.AreEqual(null, this.uut.GetHeight());
    }

    public GetSetWidthTest(): void {
        const value: number = 100;

        this.uut.SetWidth(value);
        Assert.AreEqual(value, this.uut.GetWidth());

        // null/undefined should result in null.
        this.uut.SetWidth(null);
        Assert.AreEqual(null, this.uut.GetWidth());

        this.uut.SetWidth(undefined);
        Assert.AreEqual(null, this.uut.GetWidth());
    }

    public KeySetTest(): void {
        // Ensure our key was set correctly in the constructor.
        Assert.AreEqual(this.key, this.uut.Key);
    }

    public AttributeTypeTest(): void {
        Assert.AreEqual(AttributeType.ImageUrl, this.uut.AttributeType);
    }

    public ValidateRequiredNotSetNoValue(): void {
        this.info.SetRequired(false);

        this.uut.SetValue(null);
        Assert.AreEqual(0, this.uut.Validate().length);
    }

    public ValidateRequiredNotSetWithValue(): void {
        this.info.SetRequired(false);

        this.uut.SetValue("/ShouldBeOkay.png");
        Assert.AreEqual(0, this.uut.Validate().length);
    }

    public ValidateRequiredSetNoValue(): void {
        this.info.SetRequired(true);

        this.uut.SetValue(null);
        Assert.AreEqual(1, this.uut.Validate().length);
    }

    public ValidateRequiredSetWithValue(): void {
        this.info.SetRequired(true);

        this.uut.SetValue("http://shendrick.net/ShouldBeOkay.jpg");
        Assert.AreEqual(0, this.uut.Validate().length);
    }

    public ValidateNegativeHeight(): void {
        this.info.SetRequired(false);
        this.uut.SetValue("http://shendrick.net/ShouldBeOkay.jpg");

        this.uut.SetHeight(-1);
        Assert.AreEqual(1, this.uut.Validate().length);
    }

    public ValidateOkayHeight(): void {
        this.info.SetRequired(false);
        this.uut.SetValue("http://shendrick.net/ShouldBeOkay.jpg");

        this.uut.SetHeight(0);
        Assert.AreEqual(0, this.uut.Validate().length);

        this.uut.SetHeight(100);
        Assert.AreEqual(0, this.uut.Validate().length);
    }

    public ValidateNegativeWidth(): void {
        this.info.SetRequired(false);
        this.uut.SetValue("http://shendrick.net/ShouldBeOkay.jpg");

        this.uut.SetWidth(-1);
        Assert.AreEqual(1, this.uut.Validate().length);
    }

    public ValidateOkayWidth(): void {
        this.info.SetRequired(false);
        this.uut.SetValue("http://shendrick.net/ShouldBeOkay.jpg");

        this.uut.SetWidth(0);
        Assert.AreEqual(0, this.uut.Validate().length);

        this.uut.SetWidth(100);
        Assert.AreEqual(0, this.uut.Validate().length);
    }
}