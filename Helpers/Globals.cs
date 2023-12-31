﻿using LanguageExt.Common;
using LanguageExt.Pipes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sigma.gg.Enums;
using Sigma.gg.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Runtime;
using System.Runtime.Caching;
using RiotSharp.Misc;
using System.Collections;

namespace Sigma.gg.Helpers
{
    public static class Globals
    {
        public static string apiKey { get; set; }
        public static string version { get; set; }
        public static string summonerMe { get; set; }
        public static Summoner MeSummoner { get; set; }
        public static Dictionary<string, BitmapImage> storedImages = new Dictionary<string, BitmapImage>();
        public static Dictionary<Region, string> regionsDictionary = new Dictionary<Region, string>()
        {
            { Region.Br, "Br1" },
            { Region.Eune, "Eun1" },
            { Region.Euw, "Euw1" },
            { Region.Na, "Na1" },
            { Region.Kr, "Kr" },
            { Region.Lan, "La1" },
            { Region.Las, "La2" },
            { Region.Oce, "Oc1" },
            { Region.Ru, "Ru" },
            { Region.Tr, "Tr1" },
            { Region.Jp, "Jp1" }  
        };
        private static readonly ObjectCache memoryCache = MemoryCache.Default;
        public static async Task StoreImages()
        {
            DownloadChampionsImages();
            DownloadItemsImages();
            DownloadSummonerSpellsImages();
            DownloadRunesImages();
        }
        private static async Task DownloadItemsImages()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets\Images\Items");

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                Directory.CreateDirectory(path);
            }
            else
            {
                Directory.CreateDirectory(path);
            }

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"https://ddragon.leagueoflegends.com/cdn/{Globals.version}/data/en_US/item.json");
            List<string> itemIds = new List<string>();

            try
            {
                JObject json = JObject.Parse(response);
                JObject data = json["data"] as JObject;

                if (data != null)
                {
                    foreach (var property in data.Properties())
                    {
                        itemIds.Add(property.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred: {ex.Message}");
            }

            foreach (var itemId in itemIds)
            {
                try
                {
                    var item = await client.GetByteArrayAsync($"https://ddragon.leagueoflegends.com/cdn/{Globals.version}/img/item/{itemId}.png");
                    File.WriteAllBytes(Path.Combine(path, $"I{itemId}.png"), item);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
        private static async Task DownloadSummonerSpellsImages()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets\Images\SummonerSpells");

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                Directory.CreateDirectory(path);
            }
            else
            {
                Directory.CreateDirectory(path);
            }

            HttpClient client = new HttpClient();
            foreach (var item in Enum.GetValues(typeof(SummonerSpellEnum)))
            {
                int spellId = (int)item;
                string summonerSpellName = Enum.GetName(typeof(SummonerSpellEnum), spellId);
                var summonerSpellImage = await client.GetByteArrayAsync($"https://ddragon.leagueoflegends.com/cdn/{Globals.version}/img/spell/{summonerSpellName}.png");
                File.WriteAllBytes(Path.Combine(path, $"S{spellId}.png"), summonerSpellImage);

            }
        }
        private static async Task DownloadChampionsImages()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets\Images\Champions");

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                Directory.CreateDirectory(path);
            }
            else
            {
                Directory.CreateDirectory(path);
            }

            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync($"https://ddragon.leagueoflegends.com/cdn/{Globals.version}/data/en_US/champion.json");
            List<ChampionInfo> champions = ExtractChampionInfo(response);
            foreach (var champion in champions)
            {
                var championImage = await client.GetByteArrayAsync($"http://ddragon.leagueoflegends.com/cdn/{Globals.version}/img/champion/{champion.Image.Full}");
                File.WriteAllBytes(Path.Combine(path, $"C{champion.Key}.png"), championImage);
            }
        }
        static List<ChampionInfo> ExtractChampionInfo(string jsonResponse)
        {
            List<ChampionInfo> champions = new List<ChampionInfo>();

            try
            {
                JObject json = JObject.Parse(jsonResponse);
                JObject data = json["data"] as JObject;

                if (data != null)
                {
                    foreach (var championData in data.Properties())
                    {
                        var champion = championData.Value.ToObject<ChampionInfo>();
                        champions.Add(champion);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return champions;
        }
        private static async Task DownloadRunesImages()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Assets\Images\Runes");

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                Directory.CreateDirectory(path);
            }
            else
            {
                Directory.CreateDirectory(path);
            }

            HttpClient client = new HttpClient();
            var response = client.GetStringAsync($"https://ddragon.leagueoflegends.com/cdn/{Globals.version}/data/en_US/runesReforged.json");
            List<RuneRoot> rune = JsonConvert.DeserializeObject<List<RuneRoot>>(response.Result);

            foreach (var item5 in rune)
            {
                var tempImage = await client.GetByteArrayAsync($"https://ddragon.canisback.com/img/{item5.icon}");
                File.WriteAllBytes(Path.Combine(path, $"R{item5.id}.png"), tempImage);
                foreach (var slot in item5.slots)
                {
                    foreach (var runeItem in slot.runes)
                    {
                        var tempImage2 = await client.GetByteArrayAsync($"https://ddragon.canisback.com/img/{runeItem.icon}");
                        File.WriteAllBytes(Path.Combine(path, $"R{runeItem.id}.png"), tempImage2);
                    }
                }
            }
        }
        public static BitmapImage GetImageFromFile(string path, string fileName)
        {
            if (memoryCache.Contains(fileName) && memoryCache[fileName] is BitmapImage cachedImage)
            {
                return cachedImage;
            }

            string tempPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            string finalPath = Path.Combine(tempPath, fileName);

            var image = new BitmapImage(new Uri(finalPath, UriKind.Absolute))
            {
                DecodePixelHeight = 35,
                DecodePixelWidth = 35,
                CacheOption = BitmapCacheOption.OnLoad
            };

            image.Freeze();
            memoryCache.Set(fileName, image, new CacheItemPolicy
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30) // Ustaw, ile czasu obraz ma pozostać w pamięci
            });

            return image;
        }

    }
}
