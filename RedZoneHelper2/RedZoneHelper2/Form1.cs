using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MoreLinq;

namespace RedZoneHelper2
{
    public partial class Form1 : Form
    {
        FRCApi frcApi = new FRCApi();
        TBAApi tbaApi = new TBAApi();
        List<FRCApi.Event> events = new List<FRCApi.Event>();
        List<FRCApi.Event> selectedEvents = new List<FRCApi.Event>();
        List<FIRSTRowMatch> matchesList = new List<FIRSTRowMatch>();
        List<DataGridView> mainScreenDGVs = new List<DataGridView>();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Holder for all pertinent subdivision info.
        /// TODO: add TBA data
        /// </summary>
        [Serializable]
        private class Subdivision
        {
            public FRCApi.Event Event { get; set; }
            public List<FRCApi.MatchResult> MatchResults { get; set; }
            public List<FRCApi.ScheduleMatch> Schedule { get; set; }
            public List<FIRSTRowMatch> MatchResultsView { get; set; }
            public List<SubdivisionScheduleView> ScheduleView { get; set; }

            public Subdivision()
            {
                this.Event = new FRCApi.Event();
                this.MatchResults = new List<FRCApi.MatchResult>();
                this.Schedule = new List<FRCApi.ScheduleMatch>();
            }

            public Subdivision(FRCApi.Event evt, List<FRCApi.MatchResult> matchList)
            {
                this.Event = evt;
                this.MatchResults = matchList;
            }

            public Subdivision(FRCApi.Event evt, List<FRCApi.ScheduleMatch> schedule)
            {
                this.Event = evt;
                this.Schedule = schedule;
                this.ScheduleView = new List<SubdivisionScheduleView>();
                foreach (FRCApi.ScheduleMatch s in this.Schedule)
                {
                    this.ScheduleView.Add(new SubdivisionScheduleView(s));
                }

            }
        }

        [Serializable]
        private class SubdivisionScheduleView
        {
            public string Match { get; set; }
            public DateTime Time { get; set; }
            public string RedAlliance { get; set; }
            public string BlueAlliance { get; set; }
            public double RedZoneScore { get; set; }

            public SubdivisionScheduleView() { }

            public SubdivisionScheduleView(FRCApi.ScheduleMatch schedMatch)
            {
                if (schedMatch.tournamentLevel != null)
                {
                    if (schedMatch.tournamentLevel.ToLower() == "qualification")
                    {
                        this.Match = schedMatch.description.Substring(0, 1) + schedMatch.matchNumber;
                    }
                    else
                    {
                        this.Match = schedMatch.description.Substring(0, 1) + "F" + schedMatch.matchNumber;
                    }
                }
                else
                {
                    this.Match = schedMatch.description.Substring(0, 1) + schedMatch.matchNumber;
                }
                this.Time = schedMatch.startTime;
                //Console.WriteLine(frcMatch.autoStartTime.ToUniversalTime());
                this.RedAlliance = schedMatch.RedAllianceString;
                this.BlueAlliance = schedMatch.BlueAllianceString;
                this.RedZoneScore = 1.0;
            }

        }

        [Serializable]
        private class FIRSTRowMatch
        {
            public string EventName { get; set; }
            public string T { get; set; }  //renamed for easy formatting of the DGV
            [Description("Match index in FIRST Row context.")]
            public int M { get; set; }  //renamed for easy formatting of the DGV
            public int QNum { get; set; }
            public DateTime Time { get; set; }

            public List<FRCApi.ScheduleTeam> Teams { get; set; }

            public string RedAlliance { get; set; }
            public string BlueAlliance { get; set; }
            public double RedZoneScore { get; set; }
            public double DefensesScore { get; set; }
            public double BouldersScore { get; set; }

            public double Red1AvgDefensesDamaged { get; set; }
            public double Red2AvgDefensesDamaged { get; set; }
            public double Red3AvgDefensesDamaged { get; set; }
            public double Blue1AvgDefensesDamaged { get; set; }
            public double Blue2AvgDefensesDamaged { get; set; }
            public double Blue3AvgDefensesDamaged { get; set; }

            public double Red1AvgBoulders { get; set; }
            public double Red2AvgBoulders { get; set; }
            public double Red3AvgBoulders { get; set; }
            public double Blue1AvgBoulders { get; set; }
            public double Blue2AvgBoulders { get; set; }
            public double Blue3AvgBoulders { get; set; }

            public FIRSTRowMatch() { }

            public FIRSTRowMatch(string subdivisionName, FRCApi.MatchResult frcMatch)
            {
                this.EventName = subdivisionName;

                if (frcMatch.tournamentLevel.ToLower() == "qualification")
                {
                    this.T = frcMatch.description.Substring(0, 1);
                }
                else
                {
                    this.T = frcMatch.description.Substring(0, 1);
                }
                this.M = frcMatch.matchNumber;
                this.Time = frcMatch.actualStartTime;
                //Console.WriteLine(frcMatch.autoStartTime.ToUniversalTime());
                this.RedAlliance = frcMatch.RedAllianceString;
                this.BlueAlliance = frcMatch.BlueAllianceString;
                
                this.RedZoneScore = 1.0;

            }

            public FIRSTRowMatch(string eventName, FRCApi.HybridScheduleMatch frcMatch, int queueNum)
            {
                this.EventName = eventName;

                if (frcMatch.tournamentLevel.ToLower() == "qualification")
                {
                    this.T = frcMatch.description.Substring(0, 1);
                }
                else
                {
                    this.T = frcMatch.description.Substring(0, 1);
                }
                this.M = Convert.ToInt16(frcMatch.matchNumber);
                this.QNum = queueNum;
                this.Time = frcMatch.startTime;
                //Console.WriteLine(frcMatch.autoStartTime.ToUniversalTime());
                this.Teams = frcMatch.Teams;
                this.RedAlliance = frcMatch.RedAllianceString;
                this.BlueAlliance = frcMatch.BlueAllianceString;
                this.RedZoneScore = 1.0;
            }

            public void calculateRedZoneScore()
            {
                List<double> blueDefensesDamaged = new List<double>();
                List<double> redDefensesDamaged = new List<double>();
                blueDefensesDamaged.AddRange(new double[] { Blue1AvgDefensesDamaged, Blue2AvgDefensesDamaged, Blue3AvgDefensesDamaged});
                redDefensesDamaged.AddRange(new double[] { Red1AvgDefensesDamaged, Red2AvgDefensesDamaged, Red3AvgDefensesDamaged });
                double avgBlueDefensesDamaged = (blueDefensesDamaged.FindAll(i => i > blueDefensesDamaged.Min()).ToList().Sum()) / 2;
                //double oldAvgBlueDefensesDamaged = (Blue1AvgDefensesDamaged + Blue2AvgDefensesDamaged + Blue3AvgDefensesDamaged) / 3;
                double avgRedDefensesDamaged = (redDefensesDamaged.FindAll(i => i > redDefensesDamaged.Min()).ToList().Sum()) / 2;
                //double avgRedDefensesDamaged = (Red1AvgDefensesDamaged + Red2AvgDefensesDamaged + Red3AvgDefensesDamaged) / 3;

                List<double> blueBoulders = new List<double>();
                List<double> redBoulders = new List<double>();
                blueBoulders.AddRange(new double[] { Blue1AvgBoulders, Blue2AvgBoulders, Blue3AvgBoulders });
                redBoulders.AddRange(new double[] { Red1AvgBoulders, Red2AvgBoulders, Red3AvgBoulders });
                double avgBlueBoulders = (blueBoulders.FindAll(i => i > blueBoulders.Min()).ToList().Sum()) / 2;
                //double oldAvgBlueBoulders = (Blue1AvgBoulders + Blue2AvgBoulders + Blue3AvgBoulders) / 3;
                double avgRedBoulders = (redBoulders.FindAll(i => i > redBoulders.Min()).ToList().Sum()) / 2;
                //double avgRedBoulders = (Red1AvgBoulders + Red2AvgBoulders + Red3AvgBoulders) / 3;

                double expectedDefensesDamaged = avgBlueDefensesDamaged + avgRedDefensesDamaged;
                double expectedDefensesDamagedDifference = Math.Abs(avgBlueDefensesDamaged - avgRedDefensesDamaged);
                                
                double expectedBouldersDifference = Math.Abs(avgBlueBoulders - avgRedBoulders);
                double expectedBoulders = avgBlueBoulders + avgRedBoulders;

                this.DefensesScore = expectedDefensesDamaged - expectedDefensesDamagedDifference / 4 ;
                this.BouldersScore = expectedBoulders - expectedBouldersDifference / 4;

                this.RedZoneScore = DefensesScore + BouldersScore;
                /*
                double matchTotalOPR = RedTotalOPR + BlueTotalOPR;
                double oprDifference = Math.Abs(RedTotalOPR - BlueTotalOPR);

                this.RedZoneScore = matchTotalOPR - oprDifference / 10;
                 * */
            }
        }

        [Serializable]
        private class TeamSeasonStats
        {
            public int team { get; set; }
            public List<double> Oprs { get; set; }
            public List<double> Ccwms { get; set; }
            public List<double> Dprs { get; set; }

            public TeamSeasonStats()
            {
                Oprs = new List<double>();
                Ccwms = new List<double>();
                Dprs = new List<double>();
            }
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            events = HelperDataStructures.ReadObjectFromFile<List<FRCApi.Event>>("events.bin");//frcApi.getEvents(Properties.Settings.Default.selectedYear);//HelperDataStructures.ReadObjectFromFile<List<FRCApi.Event>>("frcEvents.bin");

            //find the desired events
            string eventCodesString = Properties.Settings.Default.eventCodesString;
            string[] selectedEventCodes = eventCodesString.Replace(" ", "").Split(',');

            selectedEvents.Clear();
            foreach (string s in selectedEventCodes)
            {
                selectedEvents.Add(events.Find(i => i.code == s));
            }
            HelperDataStructures.WriteObjectToFile<List<FRCApi.Event>>("selectedEvents.bin", selectedEvents);

            //clear out the matches from the last tiem you clicked go
            matchesList.Clear();

            //Add all the matches for now.
            foreach (FRCApi.Event s in selectedEvents)
            {
                string shortName = s.name.Split('-').Last().Replace("Subdivision", "").Replace("Event", ""); //s.Event.name.Replace("FIRST Championship - ", "").Replace(" Subdivision", "");
                List<FRCApi.HybridScheduleMatch> schedule = frcApi.getHybridSchedule(Properties.Settings.Default.selectedYear, s.code, "qual");
                int redZoneQueueVal = 1;
                foreach (FRCApi.HybridScheduleMatch m in schedule)
                {
                    //DateTime compareDate = m.actualStartTime.Date + Properties.Settings.Default.currentTime.TimeOfDay;
                    if (m.actualStartTime == null)//m.actualStartTime > Properties.Settings.Default.currentTime)
                    {
                        matchesList.Add(new FIRSTRowMatch(shortName, m, redZoneQueueVal));
                        redZoneQueueVal++;
                    }
                }
            }

            matchesList = matchesList.OrderBy(i => i.M).ToList();

            int closestMatch = matchesList.First().M;

            List<double> redZoneScores = new List<double>();

            foreach (FIRSTRowMatch frm in matchesList)
            {
                // we don't care about matches that are really far out, so don't bother calculating stuff for them.
                if (frm.QNum < (closestMatch + 10))
                {
                    frm.Blue1AvgDefensesDamaged = getAverageDefensesDamaged((int)frm.Teams.Find(i => i.station == "Blue1").teamNumber);
                    frm.Blue2AvgDefensesDamaged = getAverageDefensesDamaged((int)frm.Teams.Find(i => i.station == "Blue2").teamNumber);
                    frm.Blue3AvgDefensesDamaged = getAverageDefensesDamaged((int)frm.Teams.Find(i => i.station == "Blue3").teamNumber);

                    frm.Red1AvgDefensesDamaged = getAverageDefensesDamaged((int)frm.Teams.Find(i => i.station == "Red1").teamNumber);
                    frm.Red2AvgDefensesDamaged = getAverageDefensesDamaged((int)frm.Teams.Find(i => i.station == "Red2").teamNumber);
                    frm.Red3AvgDefensesDamaged = getAverageDefensesDamaged((int)frm.Teams.Find(i => i.station == "Red3").teamNumber);

                    frm.Blue1AvgBoulders = getAverageBouldersScored((int)frm.Teams.Find(i => i.station == "Blue1").teamNumber);
                    frm.Blue2AvgBoulders = getAverageBouldersScored((int)frm.Teams.Find(i => i.station == "Blue2").teamNumber);
                    frm.Blue3AvgBoulders = getAverageBouldersScored((int)frm.Teams.Find(i => i.station == "Blue3").teamNumber);

                    frm.Red1AvgBoulders = getAverageBouldersScored((int)frm.Teams.Find(i => i.station == "Red1").teamNumber);
                    frm.Red2AvgBoulders = getAverageBouldersScored((int)frm.Teams.Find(i => i.station == "Red2").teamNumber);
                    frm.Red3AvgBoulders = getAverageBouldersScored((int)frm.Teams.Find(i => i.station == "Red3").teamNumber);

                    //frm.RedMaxOPR = getMaxOPR("red", frm);
                    //frm.BlueMaxOPR = getMaxOPR("blue", frm);
                    //frm.RedTotalOPR = getTotalOPR("red", frm);
                    //frm.BlueTotalOPR = getTotalOPR("blue", frm);
                    frm.calculateRedZoneScore();
                    redZoneScores.Add(frm.RedZoneScore);
                }

            }
            matchesList = matchesList.OrderBy(i => i.QNum).ThenByDescending(i => i.RedZoneScore).ToList();
            currentMatchesDGV.DataSource = matchesList;

            //resize and sort all DGVs by default.
            updateMainDGVs(redZoneScores);

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            /*
            if (!mBusy)
            {
                mBusy = true;
                DateTime dt = dateTimePicker1.Value;
                TimeSpan diff = dt - mPrevDate;
                if (diff.Ticks < 0) dateTimePicker1.Value = mPrevDate.AddMinutes(-1);
                else dateTimePicker1.Value = mPrevDate.AddMinutes(1);
                mBusy = false;
            }
            mPrevDate = dateTimePicker1.Value;
            */
            DateTime time = dateTimePicker1.Value;
            Properties.Settings.Default.currentTime = time;
            Properties.Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dateTimePicker1.Value = DateTime.Now;
            frcApi.loadRequestTimes();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            mainScreenDGVs.Add(currentMatchesDGV);
        }

        private void updateMainDGVs(List<double> redZoneScores)
        {
            foreach (DataGridView d in mainScreenDGVs)
            {
                d.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                d.AutoResizeColumns();

                foreach (DataGridViewColumn column in d.Columns)
                {
                    if (column.Name.Contains("Avg") || column.Name.Contains("Score"))
                    {
                        column.DefaultCellStyle.Format = "n2";
                        column.ValueType = typeof(double);
                    }
                    d.Columns[column.Name].SortMode = DataGridViewColumnSortMode.Automatic;
                }

                double maxRZS = redZoneScores.Max();
                double minRZS = redZoneScores.Min();
                double rzsRange = maxRZS - minRZS;
                float maxColorCorrectionFactor = 1.0f;

                foreach (DataGridViewRow row in d.Rows)
                {
                    double rzs = Convert.ToDouble(row.Cells["RedZoneScore"].Value);
                    if (rzs > 1)
                    {
                        float correctionFactor = 0f;
                        correctionFactor = (float)((maxRZS - rzs) / rzsRange) * maxColorCorrectionFactor;
                        row.DefaultCellStyle.BackColor = HelperDataStructures.ligtenColor(Color.LightBlue, correctionFactor);
                    }
                }
                //d.Sort(d.Columns["RedZoneScore"], ListSortDirection.Descending);
            }
        }

        private void updateOPRDataButton_Click(object sender, EventArgs e)
        {
            int year = Properties.Settings.Default.selectedYear;
            List<TBAApi.TBAEvent> tbaEvents = tbaApi.getEventsByYear(year);
            List<HelperDataStructures.TeamEventStats> allStats = new List<HelperDataStructures.TeamEventStats>();

            allStats.Clear();

            foreach (TBAApi.TBAEvent evt in tbaEvents)
            {
                if (Convert.ToBoolean(evt.official))
                {
                    TBAApi.EventStats stats = tbaApi.getEventStats(year, evt.event_code);

                    if (stats != null)
                    {
                        foreach (KeyValuePair<string, string> entry in stats.oprs)
                        {
                            HelperDataStructures.TeamEventStats teamStat = new HelperDataStructures.TeamEventStats(Convert.ToInt16(entry.Key), Convert.ToDateTime(evt.end_date),
                                Convert.ToDouble(entry.Value), Convert.ToDouble(stats.dprs[entry.Key]), Convert.ToDouble(stats.ccwms[entry.Key]));

                            allStats.Add(teamStat);
                            // do something with entry.Value or entry.Key
                        }
                    }
                }
            }

            //when you're done, write it to a file so we don't have to do this every time
            HelperDataStructures.WriteObjectToFile<List<HelperDataStructures.TeamEventStats>>("allStats.bin", allStats);
        }

        /// <summary>
        /// Gets the maximum OPR among all teams at all events on the given alliance
        /// </summary>
        /// <param name="alliance"> either "red" or "blue"</param>
        /// <param name="match">a SubdivisionBestMatch</param>
        /// <returns>a double representing the maximum OPR</returns>
        private double getMaxOPR(string alliance, FIRSTRowMatch match)
        {
            List<HelperDataStructures.TeamEventStats> allStats = HelperDataStructures.ReadObjectFromFile<List<HelperDataStructures.TeamEventStats>>("allStats.bin");

            List<HelperDataStructures.TeamEventStats> allianceStats = new List<HelperDataStructures.TeamEventStats>();

            foreach (FRCApi.ScheduleTeam s in match.Teams)
            {
                List<HelperDataStructures.TeamEventStats> stats = allStats.FindAll(i => i.Team == s.teamNumber);
                if (s.station.ToLower().Contains(alliance.ToLower()))
                {
                    allianceStats.AddRange(stats);
                }
            }

            return allianceStats.MaxBy(x => x.Opr).Opr;
        }

        private double getAverageDefensesDamaged(int teamNumber)
        {
            List<HelperDataStructures.MatchData2016> seasonData = HelperDataStructures.ReadObjectFromFile<List<HelperDataStructures.MatchData2016>>("seasonMatchData.bin");
            List<HelperDataStructures.MatchData2016> teamSeasonData = seasonData.FindAll(i => i.result.teams.Find(t => t.teamNumber == teamNumber) != null);

            double defensesDamaged = 0;
            double totalMatches = 0;
            foreach (HelperDataStructures.MatchData2016 m in teamSeasonData)
            {
                FRCApi.AllianceScoreDetails2016 asd;
                if (m.result.teams.Find(i => i.teamNumber == teamNumber).station.ToLower().Contains("red"))
                {
                    asd = m.details.alliances.Find(a => a.alliance.ToLower().Contains("red"));
                }
                else
                {
                    asd = m.details.alliances.Find(a => a.alliance.ToLower().Contains("blue"));
                }

                defensesDamaged += (asd.position1crossings == 2) ? 1 : 0;
                defensesDamaged += (asd.position2crossings == 2) ? 1 : 0;
                defensesDamaged += (asd.position3crossings == 2) ? 1 : 0;
                defensesDamaged += (asd.position4crossings == 2) ? 1 : 0;
                defensesDamaged += (asd.position5crossings == 2) ? 1 : 0;
                totalMatches += 1;
            }

            double avg = defensesDamaged / totalMatches;

            return avg;
        }

        private double getAverageBouldersScored(int teamNumber)
        {
            List<HelperDataStructures.MatchData2016> seasonData = HelperDataStructures.ReadObjectFromFile<List<HelperDataStructures.MatchData2016>>("seasonMatchData.bin");
            List<HelperDataStructures.MatchData2016> teamSeasonData = seasonData.FindAll(i => i.result.teams.Find(t => t.teamNumber == teamNumber) != null);

            double bouldersScored = 0;
            double totalMatches = 0;
            foreach (HelperDataStructures.MatchData2016 m in teamSeasonData)
            {
                FRCApi.AllianceScoreDetails2016 asd;
                if (m.result.teams.Find(i => i.teamNumber == teamNumber).station.ToLower().Contains("red"))
                {
                    asd = m.details.alliances.Find(a => a.alliance.ToLower().Contains("red"));
                }
                else
                {
                    asd = m.details.alliances.Find(a => a.alliance.ToLower().Contains("blue"));
                }

                bouldersScored += asd.autoBouldersHigh;
                bouldersScored += asd.autoBouldersLow;
                bouldersScored += asd.teleopBouldersHigh;
                bouldersScored += asd.teleopBouldersLow;
                totalMatches += 1;
            }

            double avg = bouldersScored / totalMatches;

            return avg;
        }

        /// <summary>
        /// Gets the maximum OPR among all teams at all events on the given alliance
        /// </summary>
        /// <param name="alliance"> either "red" or "blue"</param>
        /// <param name="match">a SubdivisionBestMatch</param>
        /// <returns>a double representing the maximum OPR</returns>
        private double getTotalOPR(string alliance, FIRSTRowMatch match)
        {
            List<HelperDataStructures.TeamEventStats> allStats = HelperDataStructures.ReadObjectFromFile<List<HelperDataStructures.TeamEventStats>>("allStats.bin");

            List<HelperDataStructures.TeamEventStats> allianceStats = new List<HelperDataStructures.TeamEventStats>();

            foreach (FRCApi.ScheduleTeam s in match.Teams)
            {
                List<HelperDataStructures.TeamEventStats> stats = allStats.FindAll(i => i.Team == s.teamNumber);
                if (s.station.ToLower().Contains(alliance.ToLower()))
                {
                    allianceStats.Add(stats.MaxBy(x => x.Opr));
                }
            }

            return allianceStats.Sum(i => i.Opr);
        }

        private void getFullRankingsButton_Click(object sender, EventArgs e)
        {
            int year = Properties.Settings.Default.selectedYear;
            if (year == 2016)
            {
                List<FRCApi.TeamRanking2016> ranks = frcApi.getEventRankings<FRCApi.TeamRanking2016>(year, "manda");
            }
            else if (year == 2015)
            {
                List<FRCApi.TeamRanking2015> ranks = frcApi.getEventRankings<FRCApi.TeamRanking2015>(year, "manda");
            }
            else
            {
                MessageBox.Show("Invalid year.");
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            frcApi.saveRequestTimes();
        }
    }
}
