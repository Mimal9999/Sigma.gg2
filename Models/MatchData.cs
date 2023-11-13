using Newtonsoft.Json;
using RiotSharp;
using Sigma.gg.Helpers;
using Sigma.gg.Views.Pages;
using System.Drawing;
using System.Net.Http;
using Sigma.gg.Enums;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Diagnostics;

namespace Sigma.gg.Models
{
    public class MatchData : INotifyPropertyChanged
    {
        public string queueName { get; set; }
        public string queueMap { get; set; }
        public string gameDuration { get; set; }
        public string victory { get; set; }
        public string championName { get; set; }
        public int championId { get; set; }
        public int spell1 { get; set; }
        public int spell2 { get; set; }
        public int primaryRune { get; set; }
        public int secondaryRune { get; set; }
        public string killP { get; set; }
        public string cs { get; set; }
        public string rank { get; set; }
        public string avgRank { get; set; }
        public List<Participant> participants { get; set; }
        public List<Participant> participantsTeam1 { get; set; }
        public List<Participant> participantsTeam2 { get; set; }
        public List<Image> participantsChampionIcons { get; set; }
        public int trinket { get; set; }
        public List<int> itemsIcons { get; set; }
        public int kills { get; set; }
        public int deaths { get; set; }
        public int assists { get; set; }
        public string parsedKda { get; set; }
        public double kda { get; set; }
        public int level { get; set; }
        public string controlWards { get; set; }
        public Participant me { get; set; }
        public string multikill { get; set; }
        public long gameEnd { get; set; }
        public string gameEndString { get; set; }

        private string isGridVisible;

        public string IsGridVisible
        {
            get { return isGridVisible; }
            set
            {
                if (isGridVisible != value)
                {
                    isGridVisible = value;
                    OnPropertyChanged(nameof(IsGridVisible));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public async Task GetMatchDetails(string matchId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Root match = await GetMatch(RiotSharp.Misc.Region.Europe, matchId);
            await GetMatchDetails(match);
            isGridVisible = "Collapsed";
            stopwatch.Stop();
            Debug.WriteLine($"Loading 1 match took: {stopwatch.Elapsed}");
        }

        private async Task GetMatchDetails(Root match)
        {
            participants = match.info.participants;
            RecalculateCS();
            queueName = GetQueueName(match.info.queueId);
            queueMap = GetQueueMap(match.info.queueId);
            gameDuration = ParseGameDuration(match.info.gameDuration);
            gameEnd = match.info.gameEndTimestamp;
            gameEndString = TimeAgoFromUnixTimestamp(gameEnd);
            SetTeams();
            SetPKill();
            await SetRanks();
            SetCsPerMin(match.info.gameDuration);
            //victory = match.info.
            MVPScore();
            SetRunesImages();
            SetItemsImages();
            SetChampionIcons();
            SetSummonerSpellsIcons();
            SetSefl();
        }
        /// <summary>
        /// Retrieves the map associated with a game queue based on its ID.
        /// </summary>
        /// <param name="queueId">The ID of the game queue to retrieve the map for.</param>
        /// <returns>The map associated with the game queue if found, or null if not found.</returns>
        private string GetQueueMap(int? queueId)
        {
            QueueDetails foundQueue = DashboardPage.queues.Find(q => q.queueId == queueId);
            if (foundQueue != null)
            {
                return foundQueue.map;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Retrieves the name of a game queue based on its ID.
        /// </summary>
        /// <param name="queueId">The ID of the game queue to retrieve the name for.</param>
        /// <returns>The name of the game queue if found, or null if not found.</returns>
        private string GetQueueName(int? queueId)
        {
            QueueDetails foundQueue = DashboardPage.queues.Find(q => q.queueId == queueId);
            if (foundQueue != null)
            {
                string temp = foundQueue.description.Replace("games", "");
                return temp;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Asynchronously retrieves match data from the Riot Games API for a specific region and match ID.
        /// </summary>
        /// <param name="region">The region for which to fetch the match data.</param>
        /// <param name="matchId">The ID of the match to retrieve data for.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation. It returns a <see cref="Root"/> object containing match data or null on failure.</returns>
        private async Task<Root> GetMatch(RiotSharp.Misc.Region region, string matchId)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                using var client = new HttpClient();
                string xxx = $"https://{region}.api.riotgames.com/lol/match/v5/matches/{matchId}?{Globals.apiKey}";
                var result = await client.GetStringAsync($"https://{region}.api.riotgames.com/lol/match/v5/matches/{matchId}?api_key={Globals.apiKey}");
                Root match = JsonConvert.DeserializeObject<Root>(result);
                stopwatch.Stop();
                Debug.WriteLine($"GetMatch method took: {stopwatch.Elapsed}");
                return match;
            }
            catch (RiotSharpException ex)
            {
                // Handle the exception however you want.
                return null;
            }
        }
        /// <summary>
        /// Sets the participating teams in the match, dividing participants into two separate lists for each team.
        /// The lists participantsTeam1 and participantsTeam2 are updated accordingly.
        /// </summary>
        private void SetTeams()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            this.participantsTeam1 = participants.Take(5).ToList();
            this.participantsTeam2 = participants.Skip(5).Take(5).ToList();
            stopwatch.Stop();
            Debug.WriteLine($"SetTeams method took: {stopwatch.Elapsed}");

        }
        /// <summary>
        /// Counts and returns the total number of kills for a specific team in the match.
        /// </summary>
        /// <param name="team">The team identifier (1 or 2) for which to count kills.</param>
        /// <returns>The total number of kills for the specified team.</returns>
        private int CountTotalTeamKills(int team)
        {
            var teamParticipants = team == 1 ? participantsTeam1 : participantsTeam2;
            return teamParticipants.Sum(p => p.kills);
        }
        /// <summary>
        /// Calculates and sets the kill participation (P/Kill) for each participant in the match data.
        /// It computes the total kills of each team, and then calculates the kill participation
        /// of each participant by considering their kills and assists relative to their team's total kills.
        /// The result is stored in the "killParticipation" property for each participant.
        /// </summary>
        private void SetPKill()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int team1Kills = CountTotalTeamKills(1);
            int team2Kills = CountTotalTeamKills(2);

            foreach (Participant p in participantsTeam1.Concat(participantsTeam2))
            {
                var teamKills = p.teamId == 1 ? team1Kills : team2Kills;
                double killP = ((double)p.kills + (double)p.assists) / teamKills;
                p.killParticipation = "P/Kill " + ((int)(killP * 100)).ToString() + "%";
            }
            stopwatch.Stop();
            Debug.WriteLine($"SetPKill method took: {stopwatch.Elapsed}");
        }
        /// <summary>
        /// Asynchronously retrieves and sets the ranks for each participant in the match data.
        /// It sends requests to the Riot Games API to obtain the summoners' rank information.
        /// If a participant has no ranked data (result is "[]"), they are marked as "Unranked".
        /// The average tier of all participants' ranks is computed and stored in the instance's "avgRank" property.
        /// </summary>
        private async Task SetRanks()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            HttpClient client = new HttpClient();
            var tasks = participants.Select(async p =>
            {
                var response = await client.GetAsync($"https://eun1.api.riotgames.com/lol/league/v4/entries/by-summoner/{p.summonerId}?api_key={Globals.apiKey}");
                var result = await response.Content.ReadAsStringAsync();

                if (result == "[]")
                {
                    p.rank = "Unranked 0";
                }
                else
                {
                    try
                    {
                        List<Ranks> rank = JsonConvert.DeserializeObject<List<Ranks>>(result);
                        p.rank = GetHigherRank(rank);
                    }
                    catch (Exception)
                    {
                        /*Ranks rank = JsonConvert.DeserializeObject<Ranks>(result);
                        List<Ranks> rnk = new List<Ranks>
                        {
                            rank
                        };*/
                        p.rank = "Unranked 0"; //do naprawienia
                    }
                }
            });

            await Task.WhenAll(tasks);

            this.avgRank = GetAvgTier();
            stopwatch.Stop();
            Debug.WriteLine($"SetRanks method took: {stopwatch.Elapsed}");
        }
        /// <summary>
        /// Retrieves the highest rank from a list of ranks.
        /// </summary>
        /// <param name="ranks">The list of ranks to search.</param>
        /// <returns>The highest rank as a formatted string or null if the list is empty.</returns>
        public static string GetHigherRank(List<Ranks> ranks)
        {
            if (ranks == null || ranks.Count == 0)
            {
                return null;
            }

            if (ranks.Count == 1)
            {
                return ranks[0].tier + " " + ranks[0].rank;
            }

            var topRank = ranks.MaxBy(r => CalculateRankValue(r));

            return topRank.tier + " " + topRank.rank;
        }
        /// <summary>
        /// Calculates a numeric value for a rank.
        /// </summary>
        /// <param name="rank">The rank to calculate the value for.</param>
        /// <returns>The calculated numeric value of the rank.</returns>
        private static int CalculateRankValue(Ranks rank)
        {
            string rankValue = $"{rank.tier}_{rank.rank}";
            if (Enum.TryParse(typeof(RanksEnum), rankValue, out object result))
            {
                return (int)result;
            }

            return 0; // Default value for an invalid rank.
        }
        /// <summary>
        /// Parses the match duration in seconds and formats it into a readable duration.
        /// It converts the duration to minutes and seconds and provides the formatted string.
        /// </summary>
        /// <param name="duration">The match duration in seconds.</param>
        /// <returns>The formatted match duration string (e.g., "15m 30s").</returns>
        private string ParseGameDuration(int duration)
        {
            int minutes = duration / 60;
            int seconds = duration % 60;

            return $"{minutes}m {seconds}s";
        }
        /// <summary>
        /// Calculates and assigns the CS per minute (CS/min) for each participant.
        /// It divides the total CS by the match duration in minutes to obtain CS per minute.
        /// The result is then formatted into a string and assigned to the participant.
        /// </summary>
        /// <param name="duration">The match duration in seconds.</param>
        private void SetCsPerMin(int duration)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            double gameDuration = duration / 60.0;
            foreach (Participant p in participants)
            {
                p.csPerMin = (p.totalCs / gameDuration).ToString("0.0") + "/m";
            }
            stopwatch.Stop();
            Debug.WriteLine($"SetCsPerMin method took: {stopwatch.Elapsed}");
        }
        /// <summary>
        /// Recalculates the total creep score (CS) for each participant.
        /// It adds the total minion kills and neutral minion kills to determine the new total CS value.
        /// </summary>
        private void RecalculateCS()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (Participant p in participants)
            {
                p.totalCs = p.totalMinionsKilled + p.neutralMinionsKilled;
            }
            stopwatch.Stop();
            Debug.WriteLine($"RecalculateCS method took: {stopwatch.Elapsed}");
        }
        /// <summary>
        /// Calculates and assigns a score to each participant in the match based on multiple performance factors.
        /// It evaluates performance in terms of KDA ratio, kill participation, total damage dealt to champions,
        /// gold earned, vision score, total creep score, and overall score. It then designates an MVP or ACE player for each team.
        /// </summary>
        private void MVPScore()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            if (participants[0].challenges != null)
            {
                var participantWithHighestKDA = participants.OrderByDescending(p => p.challenges.kda);
                for (int i = participantWithHighestKDA.Count() - 1; i >= 0; i--)
                {
                    participantWithHighestKDA.ElementAt(9 - i).mvpScore += i;
                }
                var participantWithHighestKillParticipation = participants.OrderByDescending(p => p.challenges.killParticipation);
                for (int i = participantWithHighestKillParticipation.Count() - 1; i >= 0; i--)
                {
                    participantWithHighestKillParticipation.ElementAt(9 - i).mvpScore += i;
                }
                var participantWithHighestDamage = participants.OrderByDescending(p => p.totalDamageDealtToChampions);
                for (int i = participantWithHighestDamage.Count() - 1; i >= 0; i--)
                {
                    participantWithHighestDamage.ElementAt(9 - i).mvpScore += i;
                }
                var participantWithHighestGold = participants.OrderByDescending(p => p.goldEarned);
                for (int i = participantWithHighestGold.Count() - 1; i >= 0; i--)
                {
                    participantWithHighestGold.ElementAt(9 - i).mvpScore += i;
                }
                var participantWithHighestVisionScore = participants.OrderByDescending(p => p.visionScore);
                for (int i = participantWithHighestVisionScore.Count() - 1; i >= 0; i--)
                {
                    participantWithHighestVisionScore.ElementAt(9 - i).mvpScore += i;
                }
                var participantWithHighestTotalCs = participants.OrderByDescending(p => p.totalCs);
                for (int i = participantWithHighestTotalCs.Count() - 1; i >= 0; i--)
                {
                    participantWithHighestTotalCs.ElementAt(9 - i).mvpScore += i;
                }
                var bestParticipant = participants.OrderByDescending(p => p.mvpScore);
                List<Participant> bestParticipantsTeam1 = new List<Participant>();
                List<Participant> bestParticipantsTeam2 = new List<Participant>();
                for (int i = bestParticipant.Count() - 1; i >= 0; i--)
                {
                    if (bestParticipant.ElementAt(9 - i).win == true)
                    {
                        bestParticipantsTeam1.Add(bestParticipant.ElementAt(9 - i));
                    }
                    else
                    {
                        bestParticipantsTeam2.Add(bestParticipant.ElementAt(9 - i));
                    }
                }
                bestParticipantsTeam1.ElementAt(0).mvpScoreString = "MVP";
                bestParticipantsTeam2.ElementAt(0).mvpScoreString = "ACE";

                for (int i = bestParticipant.Count() - 1; i >= 0; i--)
                {
                    if (bestParticipant.ElementAt(9 - i).mvpScoreString == null)
                    {
                        bestParticipant.ElementAt(9 - i).mvpScoreString = (9 - i + 2).ToString();
                    }
                }
            }
            stopwatch.Stop();
            Debug.WriteLine($"MVPScore method took: {stopwatch.Elapsed}");
            
        }
        /// <summary>
        /// Retrieves and sets personal statistics for the current user in the match data.
        /// </summary>
        private void SetSefl()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Participant me = participants.Find(p => p.summonerName == Globals.summonerMe);
            if (me != null)
            {
                kills = me.kills;
                deaths = me.deaths;
                assists = me.assists;
                if (me.challenges != null)
                {
                    parsedKda = me.challenges.kda.ToString("0.0");
                    kda = me.challenges.kda;
                }
                level = me.champLevel;
                championName = me.championName;
                championId = me.championId;
                spell1 = me.summoner1Id;
                spell2 = me.summoner2Id;
                primaryRune = me.perks.styles[0].selections[0].perk;
                secondaryRune = me.perks.styles[1].style;
                trinket = me.item6;
                itemsIcons = new List<int>
                {
                    me.item0,
                    me.item1,
                    me.item2,
                    me.item3,
                    me.item4,
                    me.item5
                };
                victory = me.win ? "Victory" : "Defeat";
                killP = me.killParticipation;
                cs = $"{me.totalCs} {me.csPerMin}";
                controlWards = $"Control Wards: {me.sightWardsBoughtInGame}";
                this.me = me;
                multikill = GetLargestMultikill();
            }
            stopwatch.Stop();
            Debug.WriteLine($"SetSelf method took: {stopwatch.Elapsed}");
        }
        /// <summary>
        /// Calculates and returns the average rank tier of all participants in the match.
        /// It computes the average tier value based on the ranks of the participants and converts it to a human-readable format.
        /// </summary>
        /// <returns>The average rank tier in a human-readable format.</returns>
        private string GetAvgTier()
        {
            var avgTier = participants.Sum(p => (int)Enum.Parse(typeof(RanksEnum), p.rank.Replace(" ", "_"))) / 10;
            return ((RanksEnum)avgTier).ToString().Replace("_", " ");
        }
        /// <summary>
        /// Determines and returns the highest multikill achievement for the current user in the match.
        /// </summary>
        /// <returns>The highest multikill achievement for the current user.</returns>
        private string GetLargestMultikill()
        {
            if (me.pentaKills > 0)
            {
                return "Penta Kill";
            }
            else if (me.quadraKills > 0)
            {
                return "Quadra Kill";
            }
            else if (me.tripleKills > 0)
            {
                return "Triple Kill";
            }
            else if (me.doubleKills > 0)
            {
                return "Double Kill";
            }
            else
            {
                return "No Multikill";
            }
        }
        public static string TimeAgoFromUnixTimestamp(long unixTimestampMilliseconds)
        {
            long unixTimestampSeconds = unixTimestampMilliseconds / 1000;
            DateTime timestamp = DateTimeOffset.FromUnixTimeSeconds(unixTimestampSeconds).UtcDateTime;
            TimeSpan timeDifference = DateTime.UtcNow - timestamp;

            if (timeDifference.TotalDays < 1)
            {
                if (timeDifference.TotalHours < 1)
                {
                    int minutesAgo = (int)timeDifference.TotalMinutes;
                    return $"{minutesAgo} {(minutesAgo == 1 ? "minute" : "minutes")} ago";
                }
                else
                {
                    int hoursAgo = (int)timeDifference.TotalHours;
                    return $"{hoursAgo} {(hoursAgo == 1 ? "hour" : "hours")} ago";
                }
            }
            else
            {
                int daysAgo = (int)timeDifference.TotalDays;
                return $"{daysAgo} {(daysAgo == 1 ? "day" : "days")} ago";
            }
        }
        private void SetRunesImages()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var p in participants)
            {
                string filename1 = p.perks.styles[0].selections[0].perk.ToString() + ".png";
                string filename2 = p.perks.styles[1].style.ToString() + ".png";
                p.primaryRuneImage = Globals.GetImageFromFile(@"Assets\Images\Runes", filename1);
                p.secondaryRuneImage = Globals.GetImageFromFile(@"Assets\Images\Runes", filename2);
            }
            stopwatch.Stop();
            Debug.WriteLine($"SetRunesImages method took: {stopwatch.Elapsed}");
        }
        private void SetItemsImages()
        {
            /*var tasks = participants.Select(async p =>
            {
                var itemsImages = new List<BitmapImage>();

                for (int i = 0; i <= 6; i++)
                {
                    string propertyName = $"item{i}";
                    int itemValue = (int)p.GetType().GetProperty(propertyName).GetValue(p, null);

                    if (Globals.itemImageCache.ContainsKey(itemValue))
                    {
                        itemsImages.Add(Globals.itemImageCache[itemValue]);
                    }
                    else
                    {
                        BitmapImage itemImage = await Globals.GetItemImage(itemValue);
                        itemsImages.Add(itemImage);
                    }
                }

                p.itemsImages = itemsImages;
            });

            await Task.WhenAll(tasks);*/
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var p in participants)
            {
                List<BitmapImage> itemsImages = new List<BitmapImage>
                {
                    p.item0 != 0 ? Globals.GetImageFromFile(@"Assets\Images\Items", p.item0.ToString() + ".png") : null,
                    p.item1 != 0 ? Globals.GetImageFromFile(@"Assets\Images\Items", p.item1.ToString() + ".png") : null,
                    p.item2 != 0 ? Globals.GetImageFromFile(@"Assets\Images\Items", p.item2.ToString() + ".png") : null,
                    p.item3 != 0 ? Globals.GetImageFromFile(@"Assets\Images\Items", p.item3.ToString() + ".png") : null,
                    p.item4 != 0 ? Globals.GetImageFromFile(@"Assets\Images\Items", p.item4.ToString() + ".png") : null,
                    p.item5 != 0 ? Globals.GetImageFromFile(@"Assets\Images\Items", p.item5.ToString() + ".png") : null,
                    p.item6 != 0 ? Globals.GetImageFromFile(@"Assets\Images\Items", p.item6.ToString() + ".png") : null
                };
                p.itemsImages = itemsImages;
            }
            stopwatch.Stop();
            Debug.WriteLine($"SetItemsImage method took: {stopwatch.Elapsed}");
        }
        private void SetChampionIcons()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var p in participants)
            {
                string filename = p.championId.ToString() + ".png";
                p.championImage = Globals.GetImageFromFile(@"Assets\Images\Champions", filename);
            }
            stopwatch.Stop();
            Debug.WriteLine($"SetChampionsIcons method took: {stopwatch.Elapsed}");
        }
        private void SetSummonerSpellsIcons()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var p in participants)
            {
                string filename1 = p.summoner1Id + ".png";
                string filename2 = p.summoner2Id + ".png";
                p.summoner1Image = Globals.GetImageFromFile(@"Assets\Images\SummonerSpells", filename1);
                p.summoner2Image = Globals.GetImageFromFile(@"Assets\Images\SummonerSpells", filename2);
            }
            stopwatch.Stop();
            Debug.WriteLine($"SetSummonerSpellsIcons method took: {stopwatch.Elapsed}");
        }
    }   
}
