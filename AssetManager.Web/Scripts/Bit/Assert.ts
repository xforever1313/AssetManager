//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class TestFailureException {

    constructor( message: string ) {
        this.Message = message;
    }

    Message: string;

    public toString(): string{
        return this.Message;
    }
}

class Assert{

    public static IsTrue(assert: boolean): void {
        if(assert === false){
            throw new TestFailureException( "Passed in assertion was false, expected true");
        }
    }

    public static IsFalse(assert: boolean): void {
        if(assert === true){
            throw new TestFailureException( "Passed in assertion was true, expected false");
        }
    }

    public static AreEqual(obj1: any, obj2: any){
        if(obj1 !== obj2){
            throw new TestFailureException(
                "Expected: " + obj1 + ", Actual: " + obj2
            );
        }
    }
}