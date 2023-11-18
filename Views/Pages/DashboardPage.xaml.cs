// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using Newtonsoft.Json;
using Sigma.gg.ViewModels.Pages;
using System.Net;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Xml.Linq;
using Wpf.Ui.Controls;
using System.Drawing;
using System.Net.Http;
using System.Collections.Generic;
using RiotSharp;
using Sigma.gg.Models;
using Sigma.gg.Views.Windows;
using Wpf.Ui.Dpi;
using RiotSharp.Endpoints.MatchEndpoint;
using System.Diagnostics;
using Sigma.gg.Helpers;
using System.IO;
using System.Collections.ObjectModel;

namespace Sigma.gg.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public static List<QueueDetails> queues;
        public DashboardViewModel ViewModel { get; }
        public Summoner activeSummoner;
        RiotGamesApi api = new RiotGamesApi();

        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await Task.Run(async () => await ViewModel.LoadMatches());
            matchList.ItemsSource = ViewModel.SummonerMatchesView;
            //RefreshScreen();
        }
        

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ImageBrush splash = new ImageBrush();
            splash.ImageSource = new BitmapImage(new Uri($"https://cdn.communitydragon.org/13.20.1/champion/Kindred/splash-art/skin/1", UriKind.Absolute));           
            Application.Current.MainWindow.Background = splash;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            queues = api.GetQueues();          
        }       

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            await RefreshScreen();
        }
        private async Task RefreshMatches() 
        { 
            await ViewModel.LoadMatches();
            await RefreshScreen();
        }

        private async Task RefreshScreen()
        {
            Stopwatch stopWatch = new();
            stopWatch.Start();
            matchList.ItemsSource = null;
            matchList.ItemsSource = ViewModel.SummonerMatches;
            stopWatch.Stop();
            long cnt = stopWatch.ElapsedMilliseconds;
        }
        private void ToggleGrid_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var matchData = button?.DataContext as MatchData;

            if (matchData != null)
            {
                matchData.IsGridVisible = matchData.IsGridVisible == "Visible" ? "Collapsed" : "Visible";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Globals.StoreImages();
        }
    }
}
