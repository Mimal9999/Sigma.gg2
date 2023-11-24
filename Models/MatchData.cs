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
using System.Globalization;
using System.Windows.Media;
using RiotSharp.Endpoints.ChampionEndpoint;

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
        public string kda { get; set; }
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
            queueName = GetQueueName(match.info.queueId);
            queueMap = GetQueueMap(match.info.queueId);
            gameDuration = ParseGameDuration(match.info.gameDuration);
            gameEnd = match.info.gameEndTimestamp;
            gameEndString = TimeAgoFromUnixTimestamp(gameEnd);
            SetTeams();
            SetParticipantInfo(match);
            await SetRanks();          
            MVPScore();                     
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
                Debug.WriteLine($"An error occurred in GetMatch method : {ex.Message}");
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
            var temp = teamParticipants.Sum(p => p.kills);
            return temp;
        }
        /// <summary>
        /// Calculates and sets the kill participation (P/Kill) for each participant in the match data.
        /// It computes the total kills of each team, and then calculates the kill participation
        /// of each participant by considering their kills and assists relative to their team's total kills.
        /// The result is stored in the "killParticipation" property for each participant.
        /// </summary>
        private void SetPKill(Participant p)
        {            
            int team1Kills = CountTotalTeamKills(1);
            int team2Kills = CountTotalTeamKills(2);
            
            var teamKills = p.teamId == 100 ? team1Kills : team2Kills;
            double killP = ((double)p.kills + (double)p.assists) / teamKills;
            p.killParticipation = "P/Kill " + ((int)(killP * 100)).ToString() + "%";
            p.killParticipationDouble = killP;                        
        }
        /// <summary>
        /// Asynchronously retrieves and sets the ranks for each participant in the match data.
        /// It sends requests to the Riot Games API to obtain the summoners' rank information.
        /// If a participant has no ranked data (result is "[]"), they are marked as "Unranked".
        /// The average tier of all participants' ranks is computed and stored in the instance's "avgRank" property.
        /// </summary>
        private async Task SetRanks()
        {
            SemaphoreSlim semaphore = new SemaphoreSlim(20);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            HttpClient client = new HttpClient();            
            var tasks = participants.Select(async p =>
            {
                await semaphore.WaitAsync();

                if (p.summonerName == Globals.MeSummoner.name)
                {
                    if (Globals.MeSummoner.flexRank != null || Globals.MeSummoner.soloRank != null)
                    {
                        List<Ranks> ranks = new List<Ranks>()
                        {
                            Globals.MeSummoner.flexRank,
                            Globals.MeSummoner.soloRank
                        };
                        p.rank = GetHigherRank(ranks);
                    }
                    else
                    {
                        p.rank = "Unranked 0";
                    }
                }
                else
                {
                    try
                    {
                        var response = await client.GetStringAsync($"https://eun1.api.riotgames.com/lol/league/v4/entries/by-summoner/{p.summonerId}?api_key={Globals.apiKey}");

                        if (response == "[]")
                        {
                            p.rank = "Unranked 0";
                        }
                        else
                        {
                            try
                            {
                                List<Ranks> rank = JsonConvert.DeserializeObject<List<Ranks>>(response);
                                p.rank = GetHigherRank(rank);
                            }
                            catch (Exception)
                            {
                                p.rank = "Unranked 0";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"An error occurred in SetRanks method : {ex.Message}");
                    }                    
                    finally
                    {
                        semaphore.Release();
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
        private void SetCsPerMin(int duration, Participant p)
        {           
            double gameDuration = duration / 60.0;            
            p.csPerMin = (p.totalCs / gameDuration).ToString("0.0") + "/m";            
        }
        /// <summary>
        /// Recalculates the total creep score (CS) for each participant.
        /// It adds the total minion kills and neutral minion kills to determine the new total CS value.
        /// </summary>
        private void RecalculateCS(Participant p)
        {
            p.totalCs = p.totalMinionsKilled + p.neutralMinionsKilled;
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
            
            var participantWithHighestKDA = participants.OrderByDescending(p => p.kdaDouble);
            for (int i = participantWithHighestKDA.Count() - 1; i >= 0; i--)
            {
                participantWithHighestKDA.ElementAt(9 - i).mvpScore += i;
            }
            var participantWithHighestKillParticipation = participants.OrderByDescending(p => p.killParticipationDouble);
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
                    //bestParticipant.ElementAt(9-i).winColor = winBrush;
                }
                else
                {
                    bestParticipantsTeam2.Add(bestParticipant.ElementAt(9 - i));
                    //bestParticipant.ElementAt(9-i).winColor = loseBrush;
                }
            }
            bestParticipantsTeam1.ElementAt(0).mvpScoreString = "MVP";
            //bestParticipantsTeam1.ElementAt(0).mvpColor = mvpBrush;
            bestParticipantsTeam2.ElementAt(0).mvpScoreString = "ACE";
            //bestParticipantsTeam2.ElementAt(0).mvpColor = aceBrush;

            for (int i = bestParticipant.Count() - 1; i >= 0; i--)
            {
                if (bestParticipant.ElementAt(9 - i).mvpScoreString == null)
                {
                    if (9 - i + 2 == 3)
                    {
                        bestParticipant.ElementAt(9 - i).mvpScoreString = (9 - i + 2).ToString() + "rd";
                        //bestParticipant.ElementAt(9 - i).mvpColor = otherBrush;
                    }
                    else
                    {
                        bestParticipant.ElementAt(9 - i).mvpScoreString = (9 - i + 2).ToString() + "th";
                        //bestParticipant.ElementAt(9 - i).mvpColor = otherBrush;
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
                if (Globals.MeSummoner.ChampionsPlayed.TryGetValue(me.championName, out int count))
                {
                    // Jeśli tak, zwiększ liczbę wystąpień o 1
                    Globals.MeSummoner.ChampionsPlayed[me.championName] = count + 1;
                }
                else
                {
                    // Jeśli nie, dodaj nową postać z liczbą wystąpień równą 1
                    Globals.MeSummoner.ChampionsPlayed[me.championName] = 1;
                }
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
            //var avgTier = participants.Sum(p => (int)Enum.Parse(typeof(RanksEnum), p.rank.Replace(" ", "_"))) / 10;
            //return ((RanksEnum)avgTier).ToString().Replace("_", " ");
            var sumOfRanks = participants
        .Sum(p => (int)Enum.Parse(typeof(RanksEnum), (p.rank ?? "Unranked 0").Replace(" ", "_")));

            var avgTier = sumOfRanks / 10;

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
        private void SetRunesImages(Participant p)
        {
            try
            {
                string filename1 = "R" + p.perks.styles[0].selections[0].perk.ToString() + ".png";
                string filename2 = "R" + p.perks.styles[1].style.ToString() + ".png";
                p.primaryRuneImage = Globals.GetImageFromFile(@"Assets\Images\Runes", filename1);
                p.secondaryRuneImage = Globals.GetImageFromFile(@"Assets\Images\Runes", filename2);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred in SetRunesImages method : {ex.Message}");
            }                  
        }
        private void SetItemsImages(Participant p)
        {
            try
            {
                List<BitmapImage> itemsImages = new List<BitmapImage>
            {
                p.item0 != 0 ? Globals.GetImageFromFile(@"Assets\Images\Items", "I" + p.item0.ToString() + ".png") : null,
                p.item1 != 0 ? Globals.GetImageFromFile(@"Assets\Images\Items", "I" + p.item1.ToString() + ".png") : null,
                p.item2 != 0 ? Globals.GetImageFromFile(@"Assets\Images\Items", "I" + p.item2.ToString() + ".png") : null,
                p.item3 != 0 ? Globals.GetImageFromFile(@"Assets\Images\Items", "I" + p.item3.ToString() + ".png") : null,
                p.item4 != 0 ? Globals.GetImageFromFile(@"Assets\Images\Items", "I" + p.item4.ToString() + ".png") : null,
                p.item5 != 0 ? Globals.GetImageFromFile(@"Assets\Images\Items", "I" + p.item5.ToString() + ".png") : null,
                p.item6 != 0 ? Globals.GetImageFromFile(@"Assets\Images\Items", "I" + p.item6.ToString() + ".png") : null
            };
                p.itemsImages = itemsImages;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred in SetItemsImages method : {ex.Message}");
            }                
        }
        private void SetChampionIcons(Participant p)
        {
            try
            {
                string filename = "C" + p.championId.ToString() + ".png";
                p.championImage = Globals.GetImageFromFile(@"Assets\Images\Champions", filename);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"An error occurred in SetChampionIcons method : {ex.Message}");
            }          
        }
        private void SetSummonerSpellsIcons(Participant p)
        {
            try
            {
                string filename1 = "S" + p.summoner1Id + ".png";
                string filename2 = "S" + p.summoner2Id + ".png";
                p.summoner1Image = Globals.GetImageFromFile(@"Assets\Images\SummonerSpells", filename1);
                p.summoner2Image = Globals.GetImageFromFile(@"Assets\Images\SummonerSpells", filename2);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred in SetSummonerSpellsIcons method : {ex.Message}");
            }         
        }
        private void SetParsedDamage(Participant p)
        {           
            p.damageDealtParsed = $"D: {p.totalDamageDealtToChampions}";
            p.damageTakenParsed = $"T: {p.totalDamageTaken}";           
        }
        private void SetKda(Participant p)
        {            
            p.kda = $"{p.kills}/{p.deaths}/{p.assists}";
            if(p.deaths == 0)
            {
                p.kdaDouble = 1000;
                p.kdaString = "Perfect KDA";
            }
            else
            {
                double kda = (double)(p.kills + p.assists) / p.deaths;
                p.kdaString = kda.ToString("0.00", CultureInfo.GetCultureInfo("en-GB")) + " KDA";
                p.kdaDouble = kda;
            }                                  
        }
        private void SetParticipantInfo(Root match)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (var p in participants)
            {
                SetKda(p);
                SetParsedDamage(p);
                SetSummonerSpellsIcons(p);
                SetChampionIcons(p);
                SetItemsImages(p);
                SetRunesImages(p);
                RecalculateCS(p);
                SetCsPerMin(match.info.gameDuration, p);
                SetPKill(p);
            }
            stopwatch.Stop();
            Debug.WriteLine($"SetParticipantInfo method took: {stopwatch.Elapsed}");
        }
    }   
}
