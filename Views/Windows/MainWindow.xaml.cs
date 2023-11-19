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
        RiotGamesApi api;
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
            Globals.apiKey = "";
            api = new RiotGamesApi();
            Globals.version = api.GetLatestVersion();

            navigationService.SetNavigationControl(NavigationView);
            snackbarService.SetSnackbarPresenter(SnackbarPresenter);
            contentDialogService.SetContentPresenter(RootContentDialog);

            NavigationView.SetServiceProvider(serviceProvider);
        }

        private void FluentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RegionComboBox.ItemsSource = Enum.GetValues(typeof(RiotSharp.Misc.Region));
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(SummonerNameTextBox.Text) && RegionComboBox.SelectedItem != null)
            {
                Summoner sm = new Summoner();
                
                sm.name = SummonerNameTextBox.Text;
                string reg = RegionComboBox.SelectedItem.ToString();
                sm.region = (RiotSharp.Misc.Region)Enum.Parse(typeof(RiotSharp.Misc.Region), reg);
                //^values taken from user^ 

                var apiSummoner = await api.GetSummonerEntriesByName(sm.name, sm.region);
                sm.puuid = apiSummoner.Puuid;
                sm.id = apiSummoner.Id;
                sm.profileIconId = apiSummoner.ProfileIconId;
                sm.summonerLevel = apiSummoner.Level;
                sm.revisionDate = apiSummoner.RevisionDate;
                sm.accountId = apiSummoner.AccountId;
                //^values taken from api^

                Globals.MeSummoner = sm;
                
            }
        }
    }
}
