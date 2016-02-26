using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace RedZoneHelper2
{
    class TBAApi
    {
        public const string tbaBaseUrl = "http://www.thebluealliance.com/api/v2";

        public class TBATeam
        {
            public string website { get; set; }
            public string name { get; set; }
            public string locality { get; set; }
            public string region { get; set; }
            public string country { get; set; }
            public string location { get; set; }
            public int? team_number { get; set; }
            public string key { get; set; }
            public string nickname { get; set; }
            public int? rookie_year { get; set; }

            public TBATeam() { }
        }

        public class TBAMatch
        {
            public string comp_level { get; set; }
            public string match_number { get; set; }
            public List<TBAVideo> videos { get; set; }
            public string set_number { get; set; }
            public string key { get; set; }
            public string time { get; set; }
            public TBAScoreBreakDown score_breakdown { get; set; }
            public TBAAlliances alliances { get; set; }
            public string event_key { get; set; }
        }

        public class TBAVideo
        {
            public string type { get; set; }
            public string key { get; set; }

            public TBAVideo() { }
        }

        public class TBAScoreBreakDown
        {
            public TBAAllianceScoreBreakdown blue { get; set; }
            public TBAAllianceScoreBreakdown red { get; set; }
        }

        public class TBAAlliances
        {
            public TBAAlliance blue { get; set; }
            public TBAAlliance red { get; set; }
        }

        public class TBAAllianceScoreBreakdown
        {
            public int? auto { get; set; }
            public int? teleop_goal { get; set; }
            public int? assist { get; set; }
            public int? trussAndCatch { get; set; }
        }

        public class TBAAlliance
        {
            public int? score { get; set; }
            public List<string> teams { get; set; }
        }

        public class TBAEvent
        {
            public string key { get; set; }
            public string website { get; set; }
            public bool? official { get; set; }
            public string end_date { get; set; }
            public string name { get; set; }
            public string short_name { get; set; }
            public string facebook_eid { get; set; }
            public string event_district_string { get; set; }
            public string venue_address { get; set; }
            public int? event_district { get; set; }
            public string location { get; set; }
            public string event_code { get; set; }
            public int? year { get; set; }
            public List<TBAWebcast> webcast { get; set; }
            public List<TBAEventAlliance> alliances { get; set; }
            public string event_type_string { get; set; }
            public string start_date { get; set; }
            public int? event_type { get; set; }
        }

        public class TBAWebcast
        {
            public string type { get; set; }
            public string channel { get; set; }
        }

        public class TBAEventAlliance
        {
            public List<string> declines { get; set; }
            public List<string> picks { get; set; }
        }

        public class TBATeamRecord
        {
            public int? wins;
            public int? losses;
            public int? ties;

            public TBATeamRecord()
            {
                wins = 0;
                losses = 0;
                ties = 0;
            }

            public override string ToString()
            {
                return wins + "-" + losses + "-" + ties;
            }
        }

        public class EventStats
        {
            public Dictionary<string, string> oprs { get; set; }
            public Dictionary<string, string> ccwms { get; set; }
            public Dictionary<string, string> dprs { get; set; }

            public EventStats() { }
        }

        /// <summary>
        /// Returns a TBA Team Model fetched from thebluealliance.com
        /// </summary>
        /// <param name="teamKey">The team to get (ex. "frc254").</param>
        /// <returns>The team model corresponding to the requested team key.</returns>
        public TBATeam getTeam(string teamKey)
        {
            string url = tbaBaseUrl + "/team/" + teamKey;
            try
            {
                string json = getTbaJsonString(url);
                TBATeam team = JsonConvert.DeserializeObject<TBATeam>(json);
                return team;
            }
            catch (Exception ex)
            {
                if (ex.Message == "The remote server returned an error: (404) Not Found.")
                {
                    throw new Exception("Team does not exist in TBA's database.");
                }
                else
                {
                    throw new Exception("An unknown error occurred");
                }
            }
        }

       public class TBAEventAward
        {
            public string event_key { get; set; }
            public int? award_type { get; set; }
            public string name { get; set; }
            public List<TBAAwardRecipient> recipient_list { get; set; }
            public int? year { get; set; }
        }

        public class TBAAwardRecipient
        {
            public int? team_number { get; set; }
            public string awardee { get; set; }
        }

        /// <summary>
        /// Returns a list of TBA Match Models for the given team at the given event.
        /// </summary>
        /// <param name="teamKey">The team to get the matches for (ex. "frc254")</param>
        /// <param name="eventKey">The event to get the matches for (ex. "2014casj")</param>
        /// <returns></returns>
        public List<TBAMatch> getTeamEventMatches(string teamKey, string eventKey)
        {
            string url = tbaBaseUrl + "/team/" + teamKey + "/event/" + eventKey + "/matches";
            string json = getTbaJsonString(url);
            return JsonConvert.DeserializeObject<List<TBAMatch>>(json);
        }

        /// <summary>
        /// Gets a list of events the team is registered for this year.
        /// </summary>
        /// <param name="teamKey">The team to get the events for (ex. "frc254")</param>
        /// <returns>A list of TBAEvents that the team is registered for this year.</returns>
        public List<TBAEvent> getTeamEvents(string teamKey)
        {
            string url = tbaBaseUrl + "/team/" + teamKey + "/events";
            string json = getTbaJsonString(url);
            return JsonConvert.DeserializeObject<List<TBAEvent>>(json);
        }

        /// <summary>
        /// Gets a list of events the team is registered for for a specific year.
        /// </summary>
        /// <param name="teamKey">The team to get the events for (ex. "frc254")</param>
        /// <param name="year">The requested year.</param>
        /// <returns>A list of TBAEvents the team is registered for for that year.</returns>
        public List<TBAEvent> getTeamEvents(string teamKey, string year, bool official = false)
        {
            string url = tbaBaseUrl + "/team/" + teamKey + "/" + year + "/events";
            string json = getTbaJsonString(url);
            List<TBAEvent> allEvents = JsonConvert.DeserializeObject<List<TBAEvent>>(json);
            if (!official)
            {
                return allEvents;
            }
            else
            {
                return allEvents.FindAll(i => i.official == true);
            }
        }

        /// <summary>
        /// Gets all matches for a requested team this year.
        /// </summary>
        /// <param name="teamKey">The team to get the events for (ex. "frc254")</param>
        /// <returns>A list of TBAMatches for that team this year.</returns>
        public List<TBAMatch> getAllTeamMatches(string teamKey)
        {
            List<TBAMatch> matches = new List<TBAMatch>();

            List<TBAEvent> events = getTeamEvents(teamKey);

            foreach (TBAEvent evt in events)
            {
                matches.AddRange(getTeamEventMatches(teamKey, evt.year + evt.event_code));
            }
            return matches;
        }

        /// <summary>
        /// Gets all matches for a requested team for a specific year.
        /// </summary>
        /// <param name="teamKey">The team to get the events for (ex. "frc254")</param>
        /// <param name="year">The requested year.</param>
        /// <returns>A list of TBAMatches for the requested team for the requested year.</returns>
        public List<TBAMatch> getAllTeamMatches(string teamKey, string year)
        {
            List<TBAMatch> matches = new List<TBAMatch>();

            List<TBAEvent> events = getTeamEvents(teamKey, year);

            foreach (TBAEvent evt in events)
            {
                matches.AddRange(getTeamEventMatches(teamKey, evt.year + evt.event_code));
            }
            return matches;
        }

        /// <summary>
        /// Gets a specified event
        /// </summary>
        /// <param name="eventKey">The TBA event key for the event.</param>
        /// <returns>The requested TBAEvent.</returns>
        public TBAEvent getEvent(string eventKey)
        {
            string url = tbaBaseUrl + "/event/" + eventKey;
            string json = getTbaJsonString(url);

            return JsonConvert.DeserializeObject<TBAEvent>(json);
        }


        /// <summary>
        /// Returns a string fetched from the TBA API at the specified url
        /// </summary>
        /// <param name="url">The constructed url.</param>
        /// <returns>A JSON parsable string</returns>
        public string getTbaJsonString(string url)
        {
            WebRequest request = WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            //Add the necessary security headers.
            request.Headers.Add("X-TBA-App-Id", "gamesense:gamesensebot:v01");

            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);

            return reader.ReadToEnd();
        }

        /// <summary>
        /// Calculates win-loss-tie for a given team in a given list of matches.
        /// </summary>
        /// <param name="teamKey">The team in question (ex. "frc254")</param>
        /// <param name="matches">The list of matches the team was in.</param>
        /// <returns>A TBATeamRecord object containing the record of the team.</returns>
        public TBATeamRecord getTeamRecordAtEvent(string teamKey, List<TBAMatch> matches)
        {
            TBATeamRecord record = new TBATeamRecord();

            foreach (TBAMatch match in matches)
            {
                if (match.alliances.blue.score > match.alliances.red.score)
                {
                    if (match.alliances.blue.teams.Contains(teamKey))
                    {
                        record.wins++;
                    }
                    else
                    {
                        record.losses++;
                    }
                }
                else if (match.alliances.red.score > match.alliances.blue.score)
                {
                    if (match.alliances.blue.teams.Contains(teamKey))
                    {
                        record.losses++;
                    }
                    else
                    {
                        record.wins++;
                    }
                }
                else if (match.alliances.blue.score == match.alliances.red.score)
                {
                    record.ties++;
                }
            }

            return record;
        }

        /// <summary>
        /// Gets a list of events on TBA for a specified year
        /// </summary>
        /// <param name="year">The year to get events for.</param>
        /// <returns></returns>
        public List<TBAEvent> getEventsByYear(int year)
        {
            string url = tbaBaseUrl + "/events/" + year.ToString();
            string json = getTbaJsonString(url);
            return JsonConvert.DeserializeObject<List<TBAEvent>>(json);
        }

        /// <summary>
        /// Gets a list of awards given out at the specified event
        /// </summary>
        /// <param name="eventCode"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<TBAEventAward> getEventAwards(string eventCode, int? year)
        {
            string url = tbaBaseUrl + "/event/" + year.ToString() + eventCode + "/awards";
            string json = getTbaJsonString(url);
            return JsonConvert.DeserializeObject<List<TBAEventAward>>(json);
        }

        public EventStats getEventStats(int year, string eventCode)
        {
            string url = tbaBaseUrl + "/event/" + year.ToString() + eventCode + "/stats";
            string json = getTbaJsonString(url);
            return JsonConvert.DeserializeObject<EventStats>(json);
        }
    }
}
