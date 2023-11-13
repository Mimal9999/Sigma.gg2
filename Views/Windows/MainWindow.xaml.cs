// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Sigma.gg.ViewModels.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System;
using Wpf.Ui.Controls;
using Sigma.gg.Helpers;
using Sigma.gg.Models;

namespace Sigma.gg.Views.Windows
{
    public partial class MainWindow
    {
        public MainWindowViewModel ViewModel { get; }

        public MainWindow(
            MainWindowViewModel viewModel,
            INavigationService navigationService,
            IServiceProvider serviceProvider,
            ISnackbarService snackbarService,
            IContentDialogService contentDialogService
        )
        {
            Wpf.Ui.Appearance.Watcher.Watch(this);

            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
            Globals.apiKey = "RGAPI-6a967526-82e6-4252-91fa-9a317fa104ae";
            RiotGamesApi api = new RiotGamesApi();
            Globals.version = api.GetLatestVersion();

            navigationService.SetNavigationControl(NavigationView);
            snackbarService.SetSnackbarPresenter(SnackbarPresenter);
            contentDialogService.SetContentPresenter(RootContentDialog);

            NavigationView.SetServiceProvider(serviceProvider);
        }

        private void FluentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //ImageBrush splash = new ImageBrush();
            //splash.ImageSource = new BitmapImage(new Uri($"https://cdn.communitydragon.org/13.20.1/champion/Kindred/splash-art/skin/1", UriKind.Absolute));
            //this.Background = splash;
        }
    }
}
