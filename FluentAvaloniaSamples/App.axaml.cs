﻿using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using FluentAvaloniaSamples.ViewModels;
using FluentAvaloniaSamples.Views;

namespace FluentAvaloniaSamples
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
    //        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
    //        {
				//desktop.MainWindow = new MainWindow();
    //        }
    //        else
            if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainView
                {
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
