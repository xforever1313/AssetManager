//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;

namespace AssetMananger.UnitTests.Web.Selenium
{
    public class WebLauncher : IDisposable
    {
        // ---------------- Fields ----------------

        private readonly Process process;
        private readonly ushort port;

        private static readonly string webProject;

        // ---------------- Constructor ----------------

        public WebLauncher( ushort port )
        {
            this.process = new Process();
            this.port = port;

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "run -- --urls=http://localhost:" + this.port,
                UseShellExecute = true,
                WorkingDirectory = webProject
            };

            this.process.StartInfo = startInfo;
        }

        static WebLauncher()
        {
            webProject = Path.Combine(
                TestContext.CurrentContext.WorkDirectory, // netcoreapp2.2
                "..", // Debug Folder
                "..", // Bin
                "..", // Unit Tests
                "..", // Project Root
                "AssetManager.Web"
            );

            FirefoxDriverDirectory = TestContext.CurrentContext.WorkDirectory;
            ChromeDriverDirectory = TestContext.CurrentContext.WorkDirectory;
        }

        public static string FirefoxDriverDirectory { get; private set; }

        public static string ChromeDriverDirectory { get; private set; }

        // ---------------- Functions ----------------

        public void StartProcess()
        {
            this.process.Start();
        }

        public void Dispose()
        {
            this.process?.Kill();
            this.process?.WaitForExit();
            this.process?.Dispose();
        }
    }
}
