using Newtonsoft.Json;
using RiotSharp;
using RiotSharp.Endpoints.MatchEndpoint;
using RiotSharp.Endpoints.StaticDataEndpoint.Champion;
using RiotSharp.Endpoints.StaticDataEndpoint.Version;
using RiotSharp.Endpoints.SummonerEndpoint;
using RiotSharp.Misc;
using RiotSharp.Misc.Converters;
using Sigma.gg.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Sigma.gg.Models
{
    public class RiotGamesApi
    {
        RiotApi api;
        public RiotGamesApi() 
        {
            api = RiotApi.GetDevelopmentInstance(Globals.apiKey);
        }
        public async Task<string> GetSummonerPuuidByName(string summonerName) 
        {
            try
            {
                var summoner = await api.Summoner.GetSummonerByNameAsync(Region.Eune, summonerName);
                return summoner.Puuid;
            }
            catch (RiotSharpException ex)
            {
                // Handle the exception however you want.
                return null;
            }
        }
        public async Task<string> GetSummonerIdByName(string summonerName)
        {
            try
            {
                var summoner = await api.Summoner.GetSummonerByNameAsync(Region.Eune, summonerName);
                return summoner.Id;
            }
            catch (RiotSharpException ex)
            {
                // Handle the exception however you want.
                return null;
            }
        }
        public async Task<List<RiotSharp.Endpoints.ChampionMasteryEndpoint.ChampionMastery>> GetSummonersBestChamps(string id)
        {
            try
            {
                
                var top = await api.ChampionMastery.GetChampionMasteriesAsync(Region.Eune, id);
                return top;
            }
            catch (RiotSharpException ex)
            {
                // Handle the exception however you want.
                return null;
            }
        }
        public async Task<string> ChampionIdToName(int id, string version)
        {
            try
            {
                var champion = await api.StaticData.Champions.GetAllAsync(version);
                if (champion.Keys.ContainsKey(id))
                {
                    var value = champion.Keys[id];
                    return value;
                }
                else
                {
                    return null;
                }
            }
            catch (RiotSharpException ex)
            {
                // Handle the exception however you want.
                return null;
            }
        }
        public async Task<List<SkinStatic>> GetChampionSkinList(int id, string version)
        {
            try
            {
                var champion = await api.StaticData.Champions.GetAllAsync(version);
                if (champion.Keys.ContainsKey(id))
                {
                    var value = champion.Keys[id];
                    var skins = champion.Champions[value].Skins;
                    return skins;
                }
                else
                {
                    return null;
                }
            }
            catch (RiotSharpException ex)
            {
                // Handle the exception however you want.
                return null;
            }
        }
        public string GetLatestVersion()
        {
            try
            {
                var z = api.StaticData.Versions.GetAllAsync().Result;
                return z[0];
            }
            catch (RiotSharpException ex)
            {
                // Handle the exception however you want.
                return null;
            }
        }

        public async Task<List<string>> GetMatches(Region region ,string puuid, int? start, int? end, string apiKey)
        {
            if(!start.HasValue)
            {
                start = 0;
            }
            if(!end.HasValue)
            {
                end = 10;
            }
            try
            {
                using var client = new HttpClient();
                var result = await client.GetStringAsync($"https://{region}.api.riotgames.com/lol/match/v5/matches/by-puuid/{puuid}/ids?start={start}&count={end}&api_key={apiKey}");
                List<string> matches = JsonConvert.DeserializeObject<List<string>>(result);
                return matches;
            }
            catch (RiotSharpException ex)
            {
                // Handle the exception however you want.
                return null;
            }
        }

        public async Task<Root> GetMatchDetails(Region region, string matchId, string apiKey)
        {
            try
            {
                using var client = new HttpClient();
                var result = await client.GetStringAsync($"https://{region}.api.riotgames.com/lol/match/v5/matches/{matchId}?api_key={apiKey}");
                Root matchDetails = JsonConvert.DeserializeObject<Root>(result);
                return matchDetails;
            }
            catch (RiotSharpException ex)
            {
                // Handle the exception however you want.
                return null;
            }
        }
        public List<QueueDetails> GetQueues()
        {
            try
            { 
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                using var client = new HttpClient();
                var result = client.GetStringAsync($"https://static.developer.riotgames.com/docs/lol/queues.json");
                List<QueueDetails> queueDetails = JsonConvert.DeserializeObject<List<QueueDetails>>(result.Result);
                stopwatch.Stop();
                Debug.WriteLine("GetQueues method took: {0}", stopwatch.Elapsed);
                return queueDetails;
            }
            catch (RiotSharpException ex)
            {
                // Handle the exception however you want.
                return null;
            }
        }
    }
}
