//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System.Diagnostics;
using System.IO;
using NUnit.Framework;

namespace AssetMananger.UnitTests.Web.Scripts
{
    [TestFixture]
    public class TypeScriptTests
    {
        // ---------------- Fields ----------------

        string testDirectory;
        string nodeModules;

        // ---------------- Setup / Teardown ----------------

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            this.testDirectory = Path.Combine(
                TestContext.CurrentContext.TestDirectory,
                "..",
                "..",
                "..",
                "Web",
                "Scripts"
            );
            this.nodeModules = Path.Combine( this.testDirectory, "node_modules" );

            if ( Directory.Exists( this.nodeModules ) == false )
            {
                this.RunSubProcess( "npm", "install", this.testDirectory );
            }
        }

        [OneTimeTearDown]
        public void FixtureTeardown()
        {
        }

        [SetUp]
        public void TestSetup()
        {
        }

        [TearDown]
        public void TestTeardown()
        {
        }

        // ---------------- Tests ----------------

        [Test]
        public void TestAll()
        {
            this.RunSubProcess( "npm", "test", this.testDirectory );
        }

        // ---------------- Test Helpers ----------------

        private void RunSubProcess( string command, string arguments, string workingDirectory )
        {
            ProcessStartInfo startInfo = new ProcessStartInfo( command, arguments )
            {
                UseShellExecute = true,
                WorkingDirectory = workingDirectory
            };

            using ( Process process = new Process() )
            {
                process.StartInfo = startInfo;
                process.Start();

                process.WaitForExit();

                Assert.AreEqual( 0, process.ExitCode );
            }
        }
    }
}
