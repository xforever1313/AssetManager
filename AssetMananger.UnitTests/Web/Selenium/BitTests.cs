//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace AssetMananger.UnitTests.Web.Selenium
{
    /// <summary>
    /// This tests runs the TypeScript BIT test to ensure
    /// all of our TypeScript classes work as expected.
    /// </summary>
    [Category( "selenium" )]
    [TestFixture]
    public class BitTests
    {
        // ---------------- Fields ----------------

        private WebLauncher launcher;

        private string url;

        // ---------------- Setup / Teardown ----------------

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            const ushort port = 44368;
            this.url = "http://localhost:" + port;
            this.launcher = new WebLauncher( port );
            this.launcher.StartProcess();
        }

        [OneTimeTearDown]
        public void FixtureTeardown()
        {
            this.launcher?.Dispose();
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
        public void DoChromeBitTest()
        {
            using ( IWebDriver driver = new ChromeDriver( WebLauncher.ChromeDriverDirectory ) )
            {
                this.DoBitTest( driver );
            }
        }

        [Test]
        public void DoFirefoxBitTest()
        {
            using ( IWebDriver driver = new FirefoxDriver( WebLauncher.FirefoxDriverDirectory ) )
            {
                this.DoBitTest( driver );
            }
        }

        // ---------------- Test Helpers ----------------

        private void DoBitTest( IWebDriver driver )
        {
            driver.Navigate().GoToUrl( url + "/Bit" );

            string result = null;
            for ( int i = 0; ( i < 10 ) && ( result == null ); ++i )
            {
                string text = null;
                try
                {
                    IWebElement query = driver.FindElement( By.Id( "overall" ) );
                    text = query.Text.Trim();
                }
                catch ( NoSuchElementException )
                {
                }

                if ( ( text == null ) || ( text == "Running..." ) )
                {
                    Thread.Sleep( 1000 );
                }
                else
                {
                    result = text;
                }
            }

            Assert.IsNotNull( result );

            Match match = Regex.Match(
                result,
                @"Passes:\s*(?<passes>\d+),\s*Failures:\s*(?<fails>\d+)"
            );

            Assert.IsTrue( match.Success );
            Assert.AreEqual( "0", match.Groups["fails"].Value );
        }
    }
}
