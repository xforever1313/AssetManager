//
//          Copyright Seth Hendrick 2019.
// Distributed under the Boost Software License, Version 1.0.
//    (See accompanying file LICENSE_1_0.txt or copy at
//          http://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AssetManager.Web
{
    public class Program
    {
        public static void Main( string[] args )
        {
            CreateWebHostBuilder( args ).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder( string[] args ) =>
            WebHost.CreateDefaultBuilder( args )
                .UseStartup<Startup>();
    }
}
