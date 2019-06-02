//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using NUnit.Framework;

namespace AssetMananger.UnitTests.Web.Selenium
{
    public class WebLauncher : IDisposable
    {
        // ---------------- Fields ----------------

        private Process process;
        private readonly ushort port;

        private readonly Thread readerThread;
        private readonly ManualResetEvent applicationStartedEvent;

        private static readonly string webDll;
        private static readonly string workingDirectory;

        // ---------------- Constructor ----------------

        public WebLauncher( ushort port )
        {
            this.process = new Process();
            this.port = port;

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "exec " + webDll + " --urls=http://localhost:" + this.port,
                UseShellExecute = false,
                WorkingDirectory = workingDirectory,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                CreateNoWindow = false
            };

            this.process.StartInfo = startInfo;
            this.readerThread = new Thread( this.ReaderThreadEntry );
            this.applicationStartedEvent = new ManualResetEvent( false );
        }

        static WebLauncher()
        {
            string runTime = Path.GetFileName( TestContext.CurrentContext.WorkDirectory );
            string configuration = Path.GetFileName(
                Path.GetFullPath(
                    Path.Combine( TestContext.CurrentContext.WorkDirectory, ".." )
                )
            );

            workingDirectory = Path.Combine(
                TestContext.CurrentContext.WorkDirectory, // netcoreapp2.2
                "..", // Debug Folder
                "..", // Bin
                "..", // Unit Tests
                "..", // Project Root
                "AssetManager.Web"
            );

            webDll = Path.Combine(
                workingDirectory,
                "bin",
                configuration,
                runTime,
                "AssetManager.Web.dll"
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
            this.readerThread.Start();
            if ( this.applicationStartedEvent.WaitOne( 10 * 1000 ) == false )
            {
                this.Dispose();
                throw new TimeoutException( "Application did not start in 10 seconds, abandon test" );
            }
        }

        public void Dispose()
        {
            this.process?.StandardInput.WriteLine( "\x3" );
            this.process?.StandardInput.Close();
            if ( this.process?.WaitForExit( 5 * 1000 ) == false )
            {
                Console.WriteLine( "Killing process" );
                this.process?.Kill();
            }
            this.process?.Dispose();
            this.readerThread.Join();
            this.process = null;
        }

        private void ReaderThreadEntry()
        {
            string line;
            do
            {
                line = this.process.StandardOutput.ReadLine();
                if ( line != null )
                {
                    Console.WriteLine( line );
                    if ( line.Contains( "Application started" ) )
                    {
                        this.applicationStartedEvent.Set();
                    }
                }
            }
            while ( line != null );
        }
    }
}
