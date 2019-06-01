//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

class TestRunner {

    // ---------------- Fields ----------------

    private instance: TestRunner;

    private readonly testFixtures: Array<ITestFixture>;

    // ---------------- Constructor ----------------

    private constructor() {
        this.testFixtures = new Array<ITestFixture>();
    }

    Instance(): TestRunner{
        if(this.instance === undefined){
            this.instance = new TestRunner();
        }

        return this.instance;
    }

    AddTestFixture(fixture: ITestFixture): void{
        this.testFixtures.push(fixture);
    }

    AddTestFixtures(fixtures: Array<ITestFixture>): void {
        for(let fix of fixtures){
            this.AddTestFixture(fix);
        }
    }

    Execute(): TestResults {
        let results: TestResults = new TestResults();

        for(let fix of this.testFixtures){
            try{
                fix.DoFixtureSetup();
                
                for(let test of fix.GetAllTests()){
                    try{
                        fix.DoTestSetup();
                        test.DoTest();
                        results.AddPass(test.Name);
                    }
                    catch(e){
                        results.AddFail(test.Name + e);
                    }
                    finally{
                        fix.DoTestTeardown();
                    }
                }
            }
            catch(e){
                results.AddFail(fix.FixtureName + ": " + e);
            }
            finally{
                try{
                    fix.DoFixtureTeardown();
                }
                catch{
                }
            }
        }

        return results;
    }
}

class TestResults{

    // ---------------- Fields ----------------

    passes: Array<string>;
    fails: Array<string>;

    // ---------------- Constructor ----------------

    public constructor() {
        this.passes = new Array<string>();
        this.fails = new Array<string>();
    }

    // ---------------- Functions ----------------

    AddPass(message: string): void {
        this.passes.push(message);
    }

    AddFail(message: string): void {
        this.fails.push(message);
    }
}