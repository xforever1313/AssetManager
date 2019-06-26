//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

/// <reference path="../Common/ValidatorToInputMapper.ts"/>

class AttributeInputMapper extends ValidatorToInputMapper<IAttribute> {

    // ---------------- Constructor ----------------

    constructor(validator: IAttribute, inputs: Array<IInputWrapper>, errorDiv: HTMLDivElement) {
        super(validator, inputs, errorDiv);
    }
}