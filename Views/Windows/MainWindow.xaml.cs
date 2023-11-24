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
using System.IO;
using Sigma.gg.ViewModels.Pages;

namespace Sigma.gg.Views.Windows
{
    public partial class MainWindow
    {
        public MainWindowViewModel ViewModel { get; }
        public DashboardViewModel DashViewModel { get; }
        RiotGamesApi api;
        public MainWindow(
            MainWindowViewModel viewModel,
            DashboardViewModel dashboardViewModel,
            INavigationService navigationService,
            IServiceProvider serviceProvider,
            ISnackbarService snackbarService,
            IContentDialogService contentDialogService
        )
        {
            Wpf.Ui.Appearance.Watcher.Watch(this);

            Globals.apiKey = "";

            ViewModel = viewModel;
            DashViewModel = dashboardViewModel;            
            DataContext = this;

            InitializeComponent();            
            api = new RiotGamesApi();
            Globals.version = api.GetLatestVersion();

            navigationService.SetNavigationControl(NavigationView);
            snackbarService.SetSnackbarPresenter(SnackbarPresenter);
            contentDialogService.SetContentPresenter(RootContentDialog);

            NavigationView.SetServiceProvider(serviceProvider);
        }

        private void FluentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RegionComboBox.ItemsSource = Globals.regionsDictionary.Keys;
            RegionComboBox.SelectedItem = RiotSharp.Misc.Region.Eune;
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            await Search();
        }

        private async void SummonerNameTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                await Search();
            }
        }
        private async Task Search()
        {
            if (!string.IsNullOrEmpty(SummonerNameTextBox.Text) && RegionComboBox.SelectedItem != null)
            {
                Summoner sm = await ViewModel.GetActiveSummoner(RegionComboBox.SelectedItem.ToString(), SummonerNameTextBox.Text, api);

                if(Globals.MeSummoner == null)
                {
                    Globals.MeSummoner = sm;
                    DashViewModel.SummonerMe = Globals.MeSummoner;
                }

                if(Globals.MeSummoner.name != sm.name)
                {
                    Globals.MeSummoner = sm;
                    DashViewModel.SummonerMe = Globals.MeSummoner;
                }
            }
        }
    }
}
