using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Windows.Forms.PropertyGridInternal;

namespace CM9394Edit
{
    [Serializable]
    public class CM94Data
    {
        public List<Division> Divisions { get; set; }
    }

    [Serializable]
    public class Division
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Club> Clubs { get; set; }
    }

    [Serializable]
    public class Club
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string StadiumName { get; set; }
        public int StadiumCapacity { get; set; }
        public string PrimaryColorHome { get; set; }
        public string SecondaryColorHome { get; set; }
        public string PrimaryColorAway { get; set; }
        public string SecondaryColorAway { get; set; }
        public Manager Manager { get; set; }
        public string City { get; set; }
        public long Funds { get; set; }
        public List<Player> Players { get; set; }
        public List<ClubSeason> PreviousSeasons { get; set; }
        public ClubSeason CurrentSeason { get; set; }
    }

    [Serializable]
    public class Manager
    {
        public int Id { get; set; }
        public int CurrentSkill { get; set; }
        public int PotentialSkill { get; set; }
        public int Personality { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int Legend { get; set; }
    }

    [Serializable]
    public enum CupStage
    {
        FIRST, SECOND, THIRD, FOURTH, FIFTH, GROUPSTAGE, QUARTERFINALS, SEMIFINALS, FINALS, WON, LOST
    }

    [Serializable]
    public class ClubSeason
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int LeagueLevel { get; set; }
        public CupStage LeagueCup { get; set; }
        public CupStage FaCup { get; set; }
        public CupStage EuropaLeague { get; set; }
        public CupStage ChampionsLegue { get; set; }
        public int WinHome { get; set; }
        public int DrawHome { get; set; }
        public int LossHome { get; set; }
        public int WinAway { get; set; }
        public int DrawAway { get; set; }
        public int LossAway { get; set; }
        public int GoalsForHome { get; set; }
        public int GoalsAgainstHome { get; set; }
        public int GoalsForAway { get; set; }
        public int GoalsAgainstAway { get; set; }
    }

    /*[Serializable]
    public class PlayerPosition
    {
        public bool G { get; set; }
        public bool D { get; set; }
        public bool M { get; set; }
        public bool F { get; set; }
        public bool R { get; set; }
        public bool L { get; set; }
        public bool C { get; set; }
    }*/
   
    
}
