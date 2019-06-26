//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

/// <reference path="../AttributeInputMapper.ts"/>
/// <reference path="../AssetAdder.ts"/>
/// <reference path="../StringAttribute.ts"/>
/// <reference path="../../Common/BaseInputWrapper.ts"/>
/// <reference path="../../Common/IInputWrapper.ts"/>
/// <reference path="../../Common/TextBoxWrapper.ts"/>

class StringHtmlToAttribute {

    // ---------------- Fields ----------------

    private readonly stringAttribute: StringAttribute;
    private readonly valueTextBox: HTMLInputElement;
    private readonly errorDiv: HTMLDivElement;
    private readonly mapper: AttributeInputMapper;

    // ---------------- Constructor ----------------

    constructor(
        valueTextBox: HTMLInputElement,
        errorDiv: HTMLDivElement,
        stringAttribute: StringAttribute
    ) {
        this.valueTextBox = valueTextBox;
        this.errorDiv = errorDiv;
        this.stringAttribute = stringAttribute;

        let inputs: Array<IInputWrapper> = new Array<IInputWrapper>();
        let myClass = this;

        {
            let valueTextBox: TextboxWrapper = new TextboxWrapper(this.valueTextBox);
            inputs.push(valueTextBox);
            valueTextBox.OnSyncValue = function (value) { myClass.stringAttribute.SetValue(value); };
        }

        this.mapper = new AttributeInputMapper(this.stringAttribute, inputs, this.errorDiv);
    }

    // ---------------- Functions ----------------

    public GetMapper(): AttributeInputMapper {
        return this.mapper;
    }
}