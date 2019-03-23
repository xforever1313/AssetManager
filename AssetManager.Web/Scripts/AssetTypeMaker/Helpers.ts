//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class Helpers {
    public static StringIsNullOrEmpty(str: string): boolean {
        return (str === null) || (str === undefined) || (str === "");
    }

    public static IsNullOrUndefined(o: object): boolean {
        return (o === undefined) || (o === null);
    }

    public static IsNotNullOrUndefined(o: object): boolean {
        return (o !== undefined) && (o !== null);
    }
}
