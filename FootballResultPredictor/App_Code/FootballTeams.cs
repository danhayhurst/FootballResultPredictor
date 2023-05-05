using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FootballResultPredictor.App_Code
{
    public class FootballTeams
    {
        
        public Dictionary<string, int> footballTeams = new Dictionary<string, int>();
        
        public FootballTeams()
        {
            //initialize the list
            footballTeams.Add("Arsenal", 599);
            footballTeams.Add("Aston Villa", 455);
            footballTeams.Add("Bournemouth", 158);
            footballTeams.Add("Brentford", 270);
            footballTeams.Add("Brighton & Hove Albion", 251);
            footballTeams.Add("Chelsea", 775);
            footballTeams.Add("Crystal Palace", 253);
            footballTeams.Add("Everton", 389);
            footballTeams.Add("Fulham", 209);
            footballTeams.Add("Leeds United", 255);
            footballTeams.Add("Leicester City", 409);
            footballTeams.Add("Liverpool", 832);
            footballTeams.Add("Manchester City",950);
            footballTeams.Add("Manchester United", 713);
            footballTeams.Add("Newcastle United", 380);
            footballTeams.Add("Nottingham Forest", 268);
            footballTeams.Add("Southampton", 284);
            footballTeams.Add("Tottenham Hotspur", 617);
            footballTeams.Add("West Ham United", 425);
            footballTeams.Add("Wolverhampton Wanderers", 348);
        }
        
    }
}