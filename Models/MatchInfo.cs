// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

using CommunityToolkit.Mvvm.ComponentModel.__Internals;
using Newtonsoft.Json;
using RiotSharp.Misc;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sigma.gg.Models;
public class Ban
{
    public int? championId { get; set; }
    public int? pickTurn { get; set; }
}

public class Baron
{
    public bool? first { get; set; }
    public int? kills { get; set; }
}

public class Challenges
{
    [JsonProperty("12AssistStreakCount")]
    
    public int? baronTakedowns { get; set; }
    public int bountyGold { get; set; }
    public int? buffsStolen { get; set; }
    public int? completeSupportQuestInTime { get; set; }
    public int? controlWardsPlaced { get; set; }
    public double? damagePerMinute { get; set; }
    public double? damageTakenOnTeamPercentage { get; set; }
    
    public int? dodgeSkillShotsSmallWindow { get; set; }
    
    public int? dragonTakedowns { get; set; }
    public double? earliestBaron { get; set; }
    public double? earliestDragonTakedown { get; set; }
    public int? earlyLaningPhaseGoldExpAdvantage { get; set; }
    
    
    public int? enemyJungleMonsterKills { get; set; }
    
    public int? epicMonsterSteals { get; set; }
    public int? epicMonsterStolenWithoutSmite { get; set; }
    public int? firstTurretKilled { get; set; }
    
    public double? gameLength { get; set; }
    public int? getTakedownsInAllLanesEarlyJungleAsLaner { get; set; }
    public double? goldPerMinute { get; set; }
    public int? hadOpenNexus { get; set; }
   
    
    public double kda { get; set; }
    
    public double? killParticipation { get; set; }
    
    public int? killsNearEnemyTurret { get; set; }
   
    public int? killsUnderOwnTurret { get; set; }
    
    public int? laneMinionsFirst10Minutes { get; set; }
    public int? laningPhaseGoldExpAdvantage { get; set; }
    
    public double? maxCsAdvantageOnLaneOpponent { get; set; }
   
    public int? maxLevelLeadLaneOpponent { get; set; }
    
    public int? multiKillOneSpell { get; set; }
    public int? multiTurretRiftHeraldCount { get; set; }
    
    public int? perfectGame { get; set; }
    
    public int playedChampSelectPosition { get; set; }
    public int poroExplosions { get; set; }
    public int? quickCleanse { get; set; }
    
   
    public int? skillshotsDodged { get; set; }
    public int? skillshotsHit { get; set; }
    public int? snowballsHit { get; set; }
    public int? soloBaronKills { get; set; }
    public int? soloKills { get; set; }
    public int? stealthWardsPlaced { get; set; }
    public int? survivedSingleDigitHpCount { get; set; }
    
    public int? takedownsBeforeJungleMinionSpawn { get; set; }
    
    public int? takedownsInEnemyFountain { get; set; }
   
    public int? turretPlatesTaken { get; set; }
    public int? turretTakedowns { get; set; }
    public int? turretsTakenWithRiftHerald { get; set; }
    public int? twentyMinionsIn3SecondsCount { get; set; }
   
    public double? visionScorePerMinute { get; set; }
    public int? wardTakedowns { get; set; }
    
    public int? wardsGuarded { get; set; }
   
    public int? soloTurretsLategame { get; set; }
    
}

public class Champion
{
    public bool? first { get; set; }
    public int? kills { get; set; }
}

public class Dragon
{
    public bool? first { get; set; }
    public int? kills { get; set; }
}

public class Horde
{
    public bool? first { get; set; }
    public int? kills { get; set; }
}

public class Info
{
    public long gameCreation { get; set; }
    public int gameDuration { get; set; }
    public long gameEndTimestamp { get; set; }
    public long? gameId { get; set; }
    public string ?gameMode { get; set; }
    public string ?gameName { get; set; }
    public long? gameStartTimestamp { get; set; }
    public string ?gameType { get; set; }
    public string ?gameVersion { get; set; }
    public int? mapId { get; set; }
    public List<Participant> ?participants { get; set; }
    public string ?platformId { get; set; }
    public int? queueId { get; set; }
    public List<Team> ?teams { get; set; }

}

public class Inhibitor
{
    public bool first { get; set; }
    public int kills { get; set; }
}

public class Metadata
{
    public string ?dataVersion { get; set; }
    public string ?matchId { get; set; }
    public List<string> ?participants { get; set; }
}

public class Objectives
{
    public Baron ?baron { get; set; }
    public Champion ?champion { get; set; }
    public Dragon ?dragon { get; set; }
    public Horde ?horde { get; set; }
    public Inhibitor inhibitor { get; set; }
    public RiftHerald ?riftHerald { get; set; }
    public int Buildings => tower.kills + inhibitor.kills; 
    public Tower tower { get; set; }
}

public class Participant
{
    public BitmapImage primaryRuneImage { get; set; }
    public BitmapImage secondaryRuneImage { get; set; }
    public Image championIcon { get; set; }
    
    public int assists { get; set; }
    
    public int baronKills { get; set; }
    
    public int bountyLevel { get; set; }
    public Challenges challenges { get; set; }
    public int champExperience { get; set; }
    public int champLevel { get; set; }
    public int championId { get; set; }
    public BitmapImage championImage { get; set; }
    public string championName { get; set; }
    
    public string damageDealtParsed { get; set; }
    public string damageTakenParsed { get; set; }
    public int damageDealtToBuildings { get; set; }
    public int damageDealtToObjectives { get; set; }
    public int damageDealtToTurrets { get; set; }
    public int damageSelfMitigated { get; set; }
   
    public int deaths { get; set; }
    public int detectorWardsPlaced { get; set; }
    public int doubleKills { get; set; }
    public int dragonKills { get; set; }
    
    public bool firstBloodKill { get; set; }
   
    public bool firstTowerKill { get; set; }
    public bool gameEndedInEarlySurrender { get; set; }
    public bool gameEndedInSurrender { get; set; }
    
    public int goldEarned { get; set; }
    public int goldSpent { get; set; }
    
    public string individualPosition { get; set; }
    
    public int item0 { get; set; }
    public int item1 { get; set; }
    public int item2 { get; set; }
    public int item3 { get; set; }
    public int item4 { get; set; }
    public int item5 { get; set; }
    public int item6 { get; set; }
    public List<BitmapImage> itemsImages { get; set; }
    public int itemsPurchased { get; set; }
    public string kda { get; set; }
    public double kdaDouble { get; set; }
    public string kdaString { get; set; }
    public int killingSprees { get; set; }
    public int kills { get; set; }
    public string lane { get; set; }
    public int largestCriticalStrike { get; set; }
    
    public int largestMultiKill { get; set; }
    public int longestTimeSpentLiving { get; set; }
    public int magicDamageDealt { get; set; }
    public int magicDamageDealtToChampions { get; set; }
    public int magicDamageTaken { get; set; }
   
    public int neutralMinionsKilled { get; set; }
    public int nexusKills { get; set; }
    public int nexusLost { get; set; }
    public int nexusTakedowns { get; set; }
    public int objectivesStolen { get; set; }
    
    
    public int participantId { get; set; }
    public int pentaKills { get; set; }
    public Perks perks { get; set; }
    public int physicalDamageDealt { get; set; }
    public int physicalDamageDealtToChampions { get; set; }
    public int physicalDamageTaken { get; set; }
    public int placement { get; set; }
    
    public int playerSubteamId { get; set; }
    public int profileIcon { get; set; }
   
    public string puuid { get; set; }
    public int quadraKills { get; set; }
    public string riotIdName { get; set; }
    public string riotIdTagline { get; set; }
    public string role { get; set; }
    public int sightWardsBoughtInGame { get; set; }
    public int spell1Casts { get; set; }
    public int spell2Casts { get; set; }
    public int spell3Casts { get; set; }
    public int spell4Casts { get; set; }
    public int subteamPlacement { get; set; }
    public int summoner1Casts { get; set; }
    public int summoner1Id { get; set; }
    public BitmapImage summoner1Image { get; set; }
    public int summoner2Casts { get; set; }
    public int summoner2Id { get; set; }
    public BitmapImage summoner2Image { get; set; }
    public string summonerId { get; set; }
    public int summonerLevel { get; set; }
    public string summonerName { get; set; }
    public bool teamEarlySurrendered { get; set; }
    public int teamId { get; set; }
    public string teamPosition { get; set; }
    public int timeCCingOthers { get; set; }
    public int timePlayed { get; set; }
    public int totalAllyJungleMinionsKilled { get; set; }
    public int totalDamageDealt { get; set; }
    public int totalDamageDealtToChampions { get; set; }
    public int totalDamageShieldedOnTeammates { get; set; }
    public int totalDamageTaken { get; set; }
    public int totalEnemyJungleMinionsKilled { get; set; }
    public int totalHeal { get; set; }
    public int totalHealsOnTeammates { get; set; }
    public int totalMinionsKilled { get; set; }
    public int totalTimeCCDealt { get; set; }
    public int totalTimeSpentDead { get; set; }
    public int totalUnitsHealed { get; set; }
    public int tripleKills { get; set; }
    public int trueDamageDealt { get; set; }
    public int trueDamageDealtToChampions { get; set; }
    public int trueDamageTaken { get; set; }
    public int turretKills { get; set; }
    public int turretTakedowns { get; set; }
    public int turretsLost { get; set; }
   
    
    public int visionScore { get; set; }
    public int visionWardsBoughtInGame { get; set; }
    public int wardsKilled { get; set; }
    public int wardsPlaced { get; set; }
    public bool win { get; set; }
    public string killParticipation { get; set; }
    public double killParticipationDouble { get; set; }
    public string rank { get; set; }
    public string csPerMin { get; set; }
    public int totalCs { get; set; }
    public int mvpScore { get; set; }
    public string mvpScoreString { get; set; }
}

public class Perks
{
    public StatPerks statPerks { get; set; }
    public List<Style> styles { get; set; }
}

public class RiftHerald
{
    public bool? first { get; set; }
    public int? kills { get; set; }
}

public class Root
{
    public Metadata ?metadata { get; set; }
    public Info ?info { get; set; }
}

public class Selection
{
    public int perk { get; set; }
    public int var1 { get; set; }
    public int var2 { get; set; }
    public int var3 { get; set; }
}

public class StatPerks
{
    public int defense { get; set; }
    public int flex { get; set; }
    public int offense { get; set; }
}

public class Style
{
    public string?description { get; set; }
    public List<Selection> selections { get; set; }
    public int style { get; set; }
}

public class Team
{
    public List<Ban> ?bans { get; set; }
    public Objectives ?objectives { get; set; }
    public int teamId { get; set; }
    public bool? win { get; set; }
}

public class Tower
{
    public bool first { get; set; }
    public int kills { get; set; }
}

public class Summoner
{
    public string? id { get; set; }
    public string? accountId { get; set; }
    public string? puuid { get; set; }
    public string? name { get; set; }
    public int profileIconId { get; set; }
    public DateTime revisionDate { get; set; }
    public long summonerLevel { get; set; }
    public Ranks soloRank { get; set; }
    public Ranks flexRank { get; set; }
    public RiotSharp.Misc.Region region { get; set; }
    public int LoadedMatches { get; set; }   
    public int Remakes { get; set; }    
    public long LadderRank { get; set; }
    public double LadderTop { get; set; }
    public int TopPlayed { get; set; }
    public int JunglePlayed { get; set; }
    public int MidPlayed { get; set; }
    public int AdcPlayed { get; set; }
    public int SupportPlayed { get; set; }
    public Dictionary<string, int> ChampionsPlayed { get { return _championsPlayed; } set 
        {
            if (_championsPlayed != value)
            {
                _championsPlayed = value;
            }
        } }
    private Dictionary<string, int> _championsPlayed = new Dictionary<string, int>();
    public string iconSource { get; set; }
}
public class Mastery
{
    public string? puuid { get; set; }
    public int? championId { get; set; }
    public int? championLevel { get; set; }
    public int? championPoints { get; set; }
    public object? lastPlayTime { get; set; }
    public int? championPointsSinceLastLevel { get; set; }
    public int? championPointsUntilNextLevel { get; set; }
    public bool? chestGranted { get; set; }
    public int? tokensEarned { get; set; }
    public string? summonerId { get; set; }
}
public class ChampionDetails
{
    public string? version { get; set; }
    public string? id { get; set; }
    public string? key { get; set; }
    public string? name { get; set; }
    public string? title { get; set; }
    public string? blurb { get; set; }
    public Info? info { get; set; }
    public Image? image { get; set; }
    public List<string>? tags { get; set; }
    public string? partype { get; set; }
    public Stats? stats { get; set; }
}
public class Stats
{
 
}
public class QueueDetails
{
    public int? queueId { get; set; }
    public string? map { get; set; }
    public string? description { get; set; }
    public string? notes { get; set; }
}
public class RuneRoot
{
    public int id { get; set; }
    public string key { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public List<Slot> slots { get; set; }
}

public class Rune
{
    public int id { get; set; }
    public string key { get; set; }
    public string icon { get; set; }
    public string name { get; set; }
    public string shortDesc { get; set; }
    public string longDesc { get; set; }
}

public class Slot
{
    public List<Rune> runes { get; set; }
}

public class Ranks
{
    public string leagueId { get; set; }
    public string queueType { get; set; }
    public string queueName { get; set; }
    public string tier { get; set; }
    public string rank { get; set; }
    public string summonerId { get; set; }
    public string summonerName { get; set; }
    public int leaguePoints { get; set; }
    public string leaguePoinstString { get; set; }
    public int wins { get; set; }
    public int losses { get; set; }
    public bool veteran { get; set; }
    public bool inactive { get; set; }
    public bool freshBlood { get; set; }
    public bool hotStreak { get; set; }
    public BitmapImage image { get; set; }
    public string rankName { get; set; }
    public string winRateString { get; set; }

    public string GetRank()
    {
        return tier + " " + rank;
    }
}
public class Item
{
    public string type { get; set; }
    public string version { get; set; }
    public Dictionary<string, string> data { get; set; }
}
public class ImageInfo
{
    public string Full { get; set; }
    public string Sprite { get; set; }
    public string Group { get; set; }
}

public class ChampionInfo
{
    public string Version { get; set; }
    public string Id { get; set; }
    public string Key { get; set; }
    public string Name { get; set; }
    public ImageInfo Image { get; set; }
}


