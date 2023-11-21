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
            RegionComboBox.ItemsSource = Globals.regionsDictionary.Keys;
            RegionComboBox.SelectedItem = RiotSharp.Misc.Region.Eune;
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(SummonerNameTextBox.Text) && RegionComboBox.SelectedItem != null)
            {
                Summoner sm = new Summoner();

                #region User Values
                sm.name = SummonerNameTextBox.Text;
                string reg = RegionComboBox.SelectedItem.ToString();
                sm.region = (RiotSharp.Misc.Region)Enum.Parse(typeof(RiotSharp.Misc.Region), reg);
                #endregion

                #region API Values
                string tempRegion = Globals.regionsDictionary[sm.region];
                var apiSummoner = await api.GetSummonerEntriesByName(sm.name, sm.region);
                sm.puuid = apiSummoner.Puuid;
                sm.id = apiSummoner.Id;
                var ranks = await api.GetSummonerRanks(sm.id, tempRegion);
                sm.profileIconId = apiSummoner.ProfileIconId;
                sm.summonerLevel = apiSummoner.Level;
                sm.revisionDate = apiSummoner.RevisionDate;
                sm.accountId = apiSummoner.AccountId;
                sm.soloRank = ranks.Count > 0 ? ranks[0] : null;
                if (ranks.Exists(x => x.queueType == "RANKED_FLEX_SR"))
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets\Images\Emblems");
                    sm.flexRank = ranks.Find(x => x.queueType == "RANKED_FLEX_SR");
                    sm.FlexWr = (int)((double)sm.flexRank.wins / (double)(sm.flexRank.wins + (double)sm.flexRank.losses) * 100);
                    sm.flexRank.image = Globals.GetImageFromFile(path, $"emblem-{sm.flexRank.tier}.png");
                }
                if (ranks.Exists(x => x.queueType == "RANKED_SOLO_5x5"))
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets\Images\Emblems");
                    sm.soloRank = ranks.Find(x => x.queueType == "RANKED_SOLO_5x5");
                    sm.SoloWr = (int)((double)sm.soloRank.wins / ((double)sm.soloRank.wins + (double)sm.soloRank.losses) * 100);
                    sm.soloRank.image = Globals.GetImageFromFile(path, $"emblem-{sm.soloRank.tier}.png");
                }               
                #endregion

                Globals.MeSummoner = sm;
                
                //DashboardViewModel. nie wiem jak to zrobić
                
            }
        }
    }
}
