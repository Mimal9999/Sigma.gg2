﻿// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Sigma.gg.Helpers;
using Sigma.gg.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Sigma.gg.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject, INotifyPropertyChanged
    {
        public ObservableCollection<MatchData> SummonerMatches = new ObservableCollection<MatchData>();

        private Summoner _summonerMe;

        public Summoner SummonerMe
        {
            get { return _summonerMe; }
            set
            {
                if (_summonerMe != value)
                {
                    _summonerMe = value;
                    OnPropertyChanged(nameof(SummonerMe));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ICollectionView _summonerMatchesView;

        public ICollectionView SummonerMatchesView
        {
            get { return _summonerMatchesView; }
            set
            {
                if (_summonerMatchesView != value)
                {
                    _summonerMatchesView = value;
                    OnPropertyChanged(nameof(SummonerMatchesView));
                }
            }
        }

        public async Task LoadMatches()
        {
            try
            {
                RiotGamesApi api = new RiotGamesApi();
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                List<string> sMatches;
                IList<MatchData> matchDatas = new List<MatchData>();
                var result = await api.GetMatches(RiotSharp.Misc.Region.Europe, Globals.MeSummoner.puuid, Globals.apiKey);
                sMatches = result;

                if (sMatches.Count > 0)
                {
                    Globals.summonerMe = Globals.MeSummoner.name;
                    foreach (var match in sMatches)
                    {
                        var matchData = new MatchData();
                        await matchData.GetMatchDetails(match);
                        SummonerMatches.Add(matchData);
                        matchDatas.Add(matchData);
                    }
                    SummonerMatchesView = CollectionViewSource.GetDefaultView(SummonerMatches);
                }
                stopwatch.Stop();
                Debug.WriteLine("Time elapsed: {0}", stopwatch.Elapsed);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"An error occurred in LoadMatches method : {ex.Message}");
            }            
        }

    }
}
