using System;
using Avalonia;
using Avalonia.Logging.Serilog;
using AssetManager.Gui.ViewModels;
using AssetManager.Gui.Views;
using AssetManager.Api;

namespace AssetManager.Gui
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main( string[] args ) => BuildAvaloniaApp().Start( AppMain, args );

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToDebug();

        // Your application's entry point. Here you can initialize your MVVM framework, DI
        // container, etc.
        private static void AppMain( Application app, string[] args )
        {
            IAssetManagerApi api;
            try
            {
                throw new Exception( "lol" );
                api = AssetManagerApiFactory.CreateApiFromDefaultConfigFile();
            }
            catch ( Exception e )
            {
                MessageBoxModel model = new MessageBoxModel
                {
                    Title = "Error when loading database",
                    Message = e.Message,
                    Type = MessageBoxModel.MessageBoxType.Error
                };

                MessageBox box = new MessageBox( model );
                app.Run( box );
                return;
            }

            var window = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };

            app.Run( window );
        }
    }
}
