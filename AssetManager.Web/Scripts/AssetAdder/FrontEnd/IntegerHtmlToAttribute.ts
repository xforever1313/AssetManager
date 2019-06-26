//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

/// <reference path="../AttributeInputMapper.ts"/>
/// <reference path="../AssetAdder.ts"/>
/// <reference path="../IntegerAttribute.ts"/>
/// <reference path="../../Common/BaseInputWrapper.ts"/>
/// <reference path="../../Common/IInputWrapper.ts"/>
/// <reference path="../../Common/TextBoxWrapper.ts"/>

class IntegerHtmlToAttribute {

    // ---------------- Fields ----------------

    private readonly integerAttribute: IntegerAttribute;
    private readonly valueTextBox: HTMLInputElement;
    private readonly errorDiv: HTMLDivElement;
    private readonly mapper: AttributeInputMapper;

    // ---------------- Constructor ----------------

    constructor(
        valueTextBox: HTMLInputElement,
        errorDiv: HTMLDivElement,
        integerAttribute: IntegerAttribute
    ) {
        this.valueTextBox = valueTextBox;
        this.errorDiv = errorDiv;
        this.integerAttribute = integerAttribute;

        let inputs: Array<IInputWrapper> = new Array<IInputWrapper>();
        let myClass = this;

        {
            let valueTextBox: TextboxWrapper = new TextboxWrapper(this.valueTextBox);
            inputs.push(valueTextBox);
            valueTextBox.OnSyncValue = function (value) { myClass.integerAttribute.SetValue(parseInt(value)); };
        }

        this.mapper = new AttributeInputMapper(this.integerAttribute, inputs, this.errorDiv);
    }

    // ---------------- Functions ----------------

    public GetMapper(): AttributeInputMapper {
        return this.mapper;
    }
}