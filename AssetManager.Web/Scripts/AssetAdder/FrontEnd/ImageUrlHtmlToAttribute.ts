//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

/// <reference path="../AttributeInputMapper.ts"/>
/// <reference path="../AssetAdder.ts"/>
/// <reference path="../ImageUrlAttribute.ts"/>
/// <reference path="../../Common/BaseInputWrapper.ts"/>
/// <reference path="../../Common/IInputWrapper.ts"/>
/// <reference path="../../Common/TextBoxWrapper.ts"/>

/**
 * Converts HTML elements to TS elements to minimize
 * native JS that needs to happen in the view.
 **/
class ImageUrlHtmlToAttribute {

    // ---------------- Fields ----------------

    private readonly valueTextBox: HTMLInputElement;
    private readonly widthTextBox: HTMLInputElement;
    private readonly heightTextBox: HTMLInputElement;
    private readonly errorDiv: HTMLDivElement;
    private readonly imageUrlAttribute: ImageUrlAttribute;

    private readonly mapper: AttributeInputMapper;

    // ---------------- Constructor ----------------

    constructor(
        valueTextBox: HTMLInputElement,
        widthTextBox: HTMLInputElement,
        heightTextBox: HTMLInputElement,
        errorDiv: HTMLDivElement,
        imageUrlAttribute: ImageUrlAttribute
    ) {
        this.valueTextBox = valueTextBox;
        this.widthTextBox = widthTextBox;
        this.heightTextBox = heightTextBox;
        this.errorDiv = errorDiv;
        this.imageUrlAttribute = imageUrlAttribute;

        let inputs: Array<IInputWrapper> = new Array<IInputWrapper>();
        let myClass = this;

        {
            let valueTextBox: TextboxWrapper = new TextboxWrapper(this.valueTextBox);
            inputs.push(valueTextBox);
            valueTextBox.OnSyncValue = function (value) { myClass.imageUrlAttribute.SetValue(value); };
        }

        {
            let heightTextBox: TextboxWrapper = new TextboxWrapper(this.heightTextBox);
            inputs.push(heightTextBox);
            heightTextBox.OnSyncValue = function (value) { myClass.imageUrlAttribute.SetHeight(parseInt(value)); };
        }

        {
            let widthTextBox: TextboxWrapper = new TextboxWrapper(this.widthTextBox);
            inputs.push(widthTextBox);
            widthTextBox.OnSyncValue = function (value) { myClass.imageUrlAttribute.SetWidth(parseInt(value)); };
        }

        this.mapper = new AttributeInputMapper(this.imageUrlAttribute, inputs, this.errorDiv);
    }

    // ---------------- Functions ----------------

    public GetMapper(): AttributeInputMapper {
        return this.mapper;
    }
}