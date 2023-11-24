// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Sigma.gg.Helpers;
using Sigma.gg.Models;
using Sigma.gg.ViewModels.Pages;
using Sigma.gg.Views.Windows;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Dpi;

namespace Sigma.gg.ViewModels.Windows
{
    public partial class MainWindowViewModel : ObservableObject
    {

        public MainWindowViewModel()
        {

        }

        [ObservableProperty]
        private string _applicationTitle = "Sigma.gg";

        [ObservableProperty]
        private ObservableCollection<object> _menuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Dashboard",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                TargetPageType = typeof(Views.Pages.DashboardPage)
            },
            new NavigationViewItem()
            {
                Content = "Data",
                Icon = new SymbolIcon { Symbol = SymbolRegular.DataHistogram24 },
                TargetPageType = typeof(Views.Pages.DataPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<object> _footerMenuItems = new()
        {
            new NavigationViewItem()
            {
                Content = "Settings",
                Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                TargetPageType = typeof(Views.Pages.SettingsPage)
            }
        };

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = new()
        {
            new MenuItem { Header = "Home", Tag = "tray_home" }
        };

        public async Task<Summoner> GetActiveSummoner(string region, string summonerName, RiotGamesApi api)
        {
            Summoner sm = new Summoner();

            #region User Values
            sm.name = summonerName;
            string reg = region;
            sm.region = (RiotSharp.Misc.Region)Enum.Parse(typeof(RiotSharp.Misc.Region), reg);
            #endregion

            #region API Values
            string tempRegion = Globals.regionsDictionary[sm.region];
            var apiSummoner = await api.GetSummonerEntriesByName(sm.name, sm.region);
            Task.WaitAll();
            sm.puuid = apiSummoner.Puuid;
            sm.id = apiSummoner.Id;
            var ranks = await api.GetSummonerRanks(sm.id, tempRegion);
            Task.WaitAll();
            sm.profileIconId = apiSummoner.ProfileIconId;
            sm.iconSource = $"https://ddragon.leagueoflegends.com/cdn/{Globals.version}/img/profileicon/{sm.profileIconId}.png";
            sm.summonerLevel = apiSummoner.Level;
            sm.revisionDate = apiSummoner.RevisionDate;
            sm.accountId = apiSummoner.AccountId;
            if(ranks != null)
            {
                if (ranks.Exists(x => x.queueType == "RANKED_FLEX_SR"))
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets\Images\Emblems");
                    sm.flexRank = ranks.Find(x => x.queueType == "RANKED_FLEX_SR");
                    sm.flexRank.queueName = "Ranked Flex";
                    int flexWr = (int)((double)sm.flexRank.wins / (double)(sm.flexRank.wins + (double)sm.flexRank.losses) * 100);
                    sm.flexRank.winRateString = "WR: " + flexWr.ToString() + "%";
                    sm.flexRank.image = Globals.GetImageFromFile(path, $"emblem-{sm.flexRank.tier}.png");
                    sm.flexRank.rankName = sm.flexRank.tier + " " + sm.flexRank.rank;
                    sm.flexRank.leaguePoinstString = sm.flexRank.leaguePoints.ToString() + " LP";
                }
                if (ranks.Exists(x => x.queueType == "RANKED_SOLO_5x5"))
                {
                    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets\Images\Emblems");
                    sm.soloRank = ranks.Find(x => x.queueType == "RANKED_SOLO_5x5");
                    sm.soloRank.queueName = "Ranked Solo/Duo";
                    int soloWr = (int)((double)sm.soloRank.wins / ((double)sm.soloRank.wins + (double)sm.soloRank.losses) * 100);
                    sm.soloRank.winRateString = "WR: " + soloWr.ToString() + "%";
                    sm.soloRank.image = Globals.GetImageFromFile(path, $"emblem-{sm.soloRank.tier}.png");
                    sm.soloRank.rankName = sm.soloRank.tier + " " + sm.soloRank.rank;
                    sm.soloRank.leaguePoinstString = sm.soloRank.leaguePoints.ToString() + " LP";
                }
            }      
            #endregion
            return sm;
        }
    }
}
