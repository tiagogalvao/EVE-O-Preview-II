using System;
using System.Reflection;
using System.Windows.Forms;
using EveOPreview.Configuration.Implementation;
using EveOPreview.Configuration.Interface;
using EveOPreview.Core.Configuration.Interface;
using EveOPreview.Mediator.Handlers.Configuration;
using EveOPreview.Mediator.Handlers.Services;
using EveOPreview.Mediator.Handlers.Thumbnails;
using EveOPreview.Mediator.Messages.Configuration;
using EveOPreview.Mediator.Messages.Services;
using EveOPreview.Mediator.Messages.Thumbnails;
using EveOPreview.Presenters.Implementation;
using EveOPreview.Services.Implementation;
using EveOPreview.Services.Interface;
using EveOPreview.View.Implementation;
using EveOPreview.View.Interface;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace EveOPreview
{
    public class Program
    {
        private const string MUTEX_NAME = "EVE-O Preview Single Instance Mutex";

        /// <summary>The main entry point for the application.</summary>
        [STAThread]
        static void Main()
        {
            InitializeWinForms();
            ExceptionHandler handler = new ExceptionHandler();
            handler.SetupExceptionHandlers();
            
            var host = CreateHostBuilder([]).Build();
            var app = host.Services.GetRequiredService<MainFormPresenter>();

            app.Run();
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    // Register WPF components
                    services.AddScoped<MainFormPresenter>();

                    // Singleton registration is used for services
                    // Low-level services
                    // services.AddScoped<IApplicationController, ApplicationController>();
                    services.AddSingleton<IWindowManager, WindowManager>();
                    services.AddSingleton<IProcessMonitor, ProcessMonitor>();
                    services.AddScoped<IMainFormView, MainForm>();

                    // MediatR
                    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
                    services.AddScoped<INotificationHandler<ThumbnailActiveSizeUpdated>, ThumbnailActiveSizeUpdatedHandler>();
                    services.AddScoped<INotificationHandler<ThumbnailConfiguredSizeUpdated>, ThumbnailConfiguredSizeUpdatedHandler>();
                    services.AddScoped<INotificationHandler<ThumbnailFrameSettingsUpdated>, ThumbnailFrameSettingsUpdatedHandler>();
                    services.AddScoped<INotificationHandler<ThumbnailListUpdated>, ThumbnailListUpdatedHandler>();
                    services.AddScoped<INotificationHandler<ThumbnailLocationUpdated>, ThumbnailLocationUpdatedHandler>();
                    
                    services.AddScoped<IRequestHandler<SaveConfiguration>, SaveConfigurationHandler>();
                    services.AddScoped<IRequestHandler<StartService>, StartStopServiceHandler>();
                    services.AddScoped<IRequestHandler<StopService>, StartStopServiceHandler>();
                    
                    // Configuration services
                    services.AddScoped<IStorage, ConfigurationStorage>();
                    services.AddScoped<IThumbnailConfiguration, ThumbnailConfiguration>();
                    
                    // Application services
                    services.AddScoped<IThumbnailManager, ThumbnailManager>();
                    services.AddScoped<IThumbnailViewFactory, ThumbnailViewFactory>();
                    services.AddScoped<IThumbnailDescription, ThumbnailDescription>();
                });

        private static void InitializeWinForms()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }
    }
}