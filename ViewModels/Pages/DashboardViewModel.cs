// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using RiotSharp.Endpoints.MatchEndpoint;
using Sigma.gg.Enums;
using Sigma.gg.Helpers;
using Sigma.gg.Models;
using Sigma.gg.ViewModels.Windows;
using Sigma.gg.Views.Pages;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Wpf.Ui.Controls;
using Wpf.Ui.Dpi;

namespace Sigma.gg.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject
    {
        public ObservableCollection<MatchData> SummonerMatches = new ObservableCollection<MatchData>();   
        
        RiotGamesApi api = new RiotGamesApi();

        public async Task LoadMatches()
        {  
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            List<string> sMatches = new List<string>();
            var result = await api.GetMatches(RiotSharp.Misc.Region.Europe, "cnFwOQeLFjTLgT3h_2AwSrhOaGXwy-1Fb24acmAeGURdb1OipUbCf6GheEwujQKd5uWVu6h25yF3bw", null, null, Globals.apiKey);
            sMatches = result;

            if (sMatches.Count > 0)
            {                
                Globals.summonerMe = "Kot w Dupach";
                foreach (var match in sMatches)
                {
                    var matchData = new MatchData();
                    await matchData.GetMatchDetails(match);
                    SummonerMatches.Add(matchData);
                }
            }
            stopwatch.Stop();
            Debug.WriteLine("Time elapsed: {0}", stopwatch.Elapsed); //3 min
        }

    }
}
